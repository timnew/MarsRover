using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Windy
{
    public sealed class ObserverbleSubscriber<T> : IObserver<T>
    {
        public ObserverbleSubscriber(Action<T> onNext = null, Action<Exception> onError = null, Action onCompleted = null)
        {
            this.onNext = onNext ?? this.onNext;
            this.onError = onError ?? this.onError;
            this.onCompleted = onCompleted ?? this.onCompleted;
        }

        #region IObserver<T> Members

        private Action onCompleted = delegate { };
        public void OnCompleted()
        {
            onCompleted();
        }

        private Action<Exception> onError = delegate { };
        public void OnError(Exception error)
        {
            onError(error);
        }

        private Action<T> onNext = delegate { };
        public void OnNext(T value)
        {
            onNext(value);
        }

        #endregion
    }
}
