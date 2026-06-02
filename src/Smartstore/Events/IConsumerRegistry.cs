namespace Smartstore.Events;

/// <summary>
/// A registry for fast <see cref="ConsumerDescriptor"/> lookup.
/// </summary>
public interface IConsumerRegistry
{
    ConsumerDescriptor[] GetConsumers(object message);
}