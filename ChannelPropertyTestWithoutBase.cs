using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Channel.Tester
{
    /// <summary>
    /// Samll test to see avalible nodes when only Channel{T} is inherited
    /// </summary>
    [ProcessNode(HasStateOutput = true)] // Process  for convinience testing
    public class ChannelPropertyTestWithoutBase : Channel<string> { }
}
