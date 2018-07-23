using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Windy
{
    public sealed class SubscribeSession<T> : IDisposable
    {
        public SubscribeSession(IObserver<T> observer, Action<IObserver<T>> subscribe = null, Action<IObserver<T>> unsubscribe = null)
        {
            Contract.Requires(observer != null);

            this.observer = observer;
            this.onSubscribe = subscribe ?? this.onSubscribe;
            this.onUnsubscribe = unsubscribe ?? this.onUnsubscribe;

            onSubscribe(observer);
        }

        private IObserver<T> observer;
        private Action<IObserver<T>> onSubscribe = delegate { };
        private Action<IObserver<T>> onUnsubscribe = delegate { };

        public void Dispose()
        {
            onUnsubscribe(observer);
        }
    }
}