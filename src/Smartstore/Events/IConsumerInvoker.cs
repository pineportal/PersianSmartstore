namespace Smartstore.Events;

/// <summary>
/// Responsible for invoking event message handler methods.
/// </summary>
public interface IConsumerInvoker
{
    /// <summary>
    /// Invokes the specified consumer to process the provided event message.
    /// </summary>
    /// <typeparam name="TMessage">The type of event message to be consumed. Must implement the IEventMessage interface.</typeparam>
    /// <param name="descriptor">The descriptor that provides metadata and configuration for the consumer.</param>
    /// <param name="consumer">The consumer instance that will handle the event message.</param>
    /// <param name="envelope">The context containing the event message and related metadata to be processed.</param>
    /// <param name="cancelToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous invocation operation.</returns>
    Task InvokeAsync<TMessage>(
        ConsumerDescriptor descriptor,
        IConsumer consumer,
        IConsumeContext<TMessage> envelope,
        CancellationToken cancelToken = default) where TMessage : IEventMessage;
}