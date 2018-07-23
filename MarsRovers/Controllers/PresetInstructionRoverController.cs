using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windy;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class PresetInstructionRoverController : IRoverController
    {
        #region Constructors

        public PresetInstructionRoverController(params IRoverAction[] actions)
            : this(actions as IEnumerable<IRoverAction>)
        {
            Contract.Requires(actions != null);
        }
        public PresetInstructionRoverController(IEnumerable<IRoverAction> actions)
        {
            Contract.Requires(actions != null);
            this.Instructions = new List<IRoverAction>(actions);
        }

        #endregion

        protected List<IRoverAction> Instructions;

        #region Timer Related

        private IDisposable timerSession;
        public bool IsTimerAttached { get { return timerSession != null; } }
        public void AttachTimer(IObservable<TimeSlice> timer)
        {
            if (timerSession != null)
            {
                timerSession.Dispose();
                timerSession = null;
            }

            if (timer != null)
            {
                timerSession = timer.Subscribe(this);
            }
        }

        #endregion

        #region IObservable<IRoverAction> Members

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "Contract.Result<T> will be rewriten after compiling")]
        public IDisposable Subscribe(IObserver<IRoverAction> observer)
        {
            return
                new SubscribeSession<IRoverAction>(
                    observer,
                    (c) =>
                    {
                        this.DispatchInstruction += c.OnNext;
                        this.DispatchError += c.OnError;
                        this.DispatchCompleted += c.OnCompleted;
                    },
                    (c) =>
                    {
                        this.DispatchInstruction -= c.OnNext;
                        this.DispatchError -= c.OnError;
                        this.DispatchCompleted -= c.OnCompleted;
                    });
        }

        protected Action DispatchCompleted = delegate { };
        protected Action<Exception> DispatchError = delegate { };
        protected Action<IRoverAction> DispatchInstruction = delegate { };

        #endregion

        #region IObserver<int> Members


        public void OnCompleted()
        {
            DispatchCompleted(); // To call dispatchCompleted here rather than in OnNext while value == Instructions.Count ensures all the rovers report their positions and orientations at the same time;
        }

        public void OnError(Exception error)
        {
            DispatchError(error); // Relay the error to the rover.
        }

        public void OnNext(TimeSlice value)
        {
            if (value.Index < this.Instructions.Count - 1)
            {
                value.ReportNeedMoreTime();
            }

            if (value.Index < 0) // HACK add this to remove the unproven require conctract warning
                throw new ArgumentException("Invalid Time Slice");

            if (value.Index < this.Instructions.Count)
            {
                DispatchInstruction(Instructions[value.Index]);
            }
        }

        #endregion
    }
}
