using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Windy
{
    public static class RxExtension
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext = null, Action<Exception> onError = null, Action onCompleted = null)
        {
            var subscriber = new ObserverbleSubscriber<T>(onNext, onError, onCompleted);
            return observable.Subscribe(subscriber);
        }
    }
}
