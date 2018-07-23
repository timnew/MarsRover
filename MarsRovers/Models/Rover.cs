using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windy;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class Rover : INotifyPropertyChanged, IObserver<IRoverAction>
    {
        internal Rover(Plateau plateau, Point postion, Orientation orientation)
        {
            this.Plateau = plateau;
            this.Orientation = orientation;
            this.Position = postion;
        }

        #region Properties

        public Plateau Plateau { get; private set; }

        private IDisposable controllerSession;
        public bool IsControllerAttached { get { return controllerSession != null; } }
        public void AttachContoller(IRoverController controller)
        {
            if (controllerSession != null)
            {
                controllerSession.Dispose();
                controllerSession = null;
            }

            if (controller != null)
            {
                controllerSession = controller.Subscribe(this);
            }
        }

        #region Notify Property Orientation
        private Orientation orientation;
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation == value)
                    return;

                orientation = value;
                Notify("Orientation");
            }
        }
        #endregion

        #region Notify Property Position
        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                if (position == value)
                    return;

                position = value;
                Notify("Position");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion

        #region IObserver<IRoverAction> Explicit Members

        void IObserver<IRoverAction>.OnNext(IRoverAction value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.OnNext(value);
        }

        void IObserver<IRoverAction>.OnError(Exception error)
        {
            this.OnError(error);
        }

        void IObserver<IRoverAction>.OnCompleted()
        {
            this.OnCompleted();
        }

        #endregion

        #region IObserver<IRoverAction> Implementations

        protected virtual void OnNext(IRoverAction action)
        {
            Contract.Requires(action != null);

            action.ApplyTo(this);
        }

        protected virtual void OnError(Exception error)
        {
            //TODO Log Error
        }

        protected virtual void OnCompleted()
        {
            //TODO Log Complete
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return "{0} {1} {2}".ApplyFormat(Position.X, Position.Y, Orientation.ToShortName()) ?? base.ToString();// HACK to satisfy the code contract
        }

        #endregion
    }

}