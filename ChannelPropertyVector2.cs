using System.Reactive.Disposables;
using Stride.Core.Mathematics;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Channel.Tester
{
    [ProcessNode(HasStateOutput = true)] // Just for convinience testing
    public class ChannelPropertyVector2 : ChannelBase<Vector2>, IDisposable
    {
        public IChannel<float> XChannel { get; } = ChannelHelpers.CreateChannelOfType<float>();
        public IChannel<float> YChannel { get; } = ChannelHelpers.CreateChannelOfType<float>();

        private readonly CompositeDisposable _subscriptions = new();

        public ChannelPropertyVector2(Vector2 initialValue)
        {
            this.Value = initialValue;

            // also possible to access:
            // this.value
            // this.subject

            // Subscribe to changes in the X and Y channels to update the Vector2 value
            _subscriptions.Add(
                XChannel.Subscribe(x =>
                {
                    this.EnsureValue(this.Value with { X = x });
                })
            );
            _subscriptions.Add(
                YChannel.Subscribe(y =>
                {
                    this.EnsureValue(this.Value with { Y = y });
                })
            );

            _subscriptions.Add(
                this.Subscribe(vec =>
                {
                    XChannel.EnsureValue(vec.X);
                    YChannel.EnsureValue(vec.Y);
                })
            );
        }
    }
}
