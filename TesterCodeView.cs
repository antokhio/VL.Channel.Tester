using System.Reactive.Linq;
using System.Reactive.Subjects;
using Stride.Core.Mathematics;
using VL.Lib.Reactive;

namespace VL.Channel.Tester
{
    // Internal so it's not leaking to vl
    internal class TesterCodeView
    {
        private ChannelPropertyVector2 _vector2Property = new ChannelPropertyVector2(default);
        private ChannelPropertyTestWithoutBase _stringProperty =
            new ChannelPropertyTestWithoutBase();
        public IChannel<Vector2> Vector2Property => _vector2Property;

        public IChannel<string> StringProperty => _stringProperty;

        public TesterCodeView()
        {
            _vector2Property.Subscribe(x => Console.WriteLine("Vector2 updated: " + x));

            // _stringProperty.Subscribe // subscribe is in accessible

            var vector2 = _vector2Property.Value; // Value is accessible
            var str = _stringProperty.Value; // Value is accessible and not object, howeve vvvv sees Value as Object

            // _vector2Property.SetValue // no set value
            // _stringProperty.SetValue // no set value

            _vector2Property.OnNext(new Vector2(1, 2)); // OnNext is accessible
            // _stringProperty.OnNext // no OnNext

            _vector2Property.SetValueAndAuthor(new Vector2(3, 4), "Tester"); // SetValueAndAuthor is accessible
            _stringProperty.SetValueAndAuthor("test", "Tester"); // SetValueAndAuthor is accessible

            Observable
                .Merge(
                    // only accessible with type arguments
                    _stringProperty.Select<string, object>(x => (object)x),
                    _vector2Property.Select<Vector2, object>(x => (object)x)
                )
                .Subscribe(x => Console.WriteLine(x.ToString()));

            var s1 = new Subject<string>();
            var s2 = new Subject<Vector2>();
            // no type arguiment here hmm
            var s3 = Observable.Merge(s1.Select(x => (object)x), s2.Select(x => (object)x));
        }
    }
}
