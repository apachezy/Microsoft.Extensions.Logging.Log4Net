using log4net.Core;
using Microsoft.Extensions.Logging.Log4Net.Entities;

namespace Microsoft.Extensions.Logging.Log4Net
{
    /// <summary>
    /// Represents a factory that creates the log4net <see cref="log4net.Core.LoggingEvent"/> from a <see cref="MessageCandidate{TState}"/>.
    /// </summary>
    public interface ILog4NetLoggingEventFactory
    {
        /// <summary>
        /// Create the <see cref="log4net.Core.LoggingEvent"/>.
        /// </summary>
        /// <typeparam name="TState">
        /// The type of the state object used to format the log message.
        /// </typeparam>
        /// <param name="messageCandidate">
        /// The message information that should be logged.
        /// </param>
        /// <param name="logger">
        /// The logger the event is created for.
        /// </param>
        /// <param name="options">
        /// The options of the log4net logging provider.
        /// </param>
        /// <param name="scopeProvider">
        /// The <see cref="IExternalScopeProvider"/> used to access the current logging scopes.
        /// <para/>
        /// This provider enables integration with the <c>Microsoft.Extensions.Logging</c> scope system
        /// (for example, values created via <c>ILogger.BeginScope</c>).
        /// <para/>
        /// Implementations may enumerate the active scopes to enrich the resulting
        /// <see cref="log4net.Core.LoggingEvent"/> with contextual data, such as
        /// logical operation information or structured properties.
        /// </param>
        /// <returns>
        /// A <see cref="log4net.Core.LoggingEvent"/> that is ready to be logged with the provided logger,
        /// or <c>null</c> if the candidate should be dropped.
        /// </returns>
        LoggingEvent CreateLoggingEvent<TState>(
            in MessageCandidate<TState> messageCandidate,
            log4net.Core.ILogger logger,
            Log4NetProviderOptions options,
            IExternalScopeProvider scopeProvider);
    }
}