using System.Reactive;
using VL.Lib.Reactive;

namespace VL.Channel.Tester
{
    public abstract class ChannelBase<T> : Channel<T>, IChannel<T>
    {
        // Expose EnsureValue as a convenient instance method
        public void EnsureValue(T? value, bool force = false, string? author = default)
        {
            ChannelHelpers.EnsureValue(this, value, force, author);
        }

        // Expose OnNext with author support
        public void OnNext(T? value, string? author = default)
        {
            base.SetValueAndAuthor(value, author);
        }

        // Helper for simple subscription
        public IDisposable Subscribe(Action<T?> onNext)
        {
            return ((IObservable<T?>)this).Subscribe(Observer.Create(onNext));
        }
    }
}
