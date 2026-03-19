using System;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging.Log4Net.Entities;

namespace Microsoft.Extensions.Logging.Log4Net
{
    /// <summary>
    /// The log4net logger class.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private readonly IExternalScopeProvider _externalScopeProvider;

        /// <summary>
        /// The log.
        /// </summary>
        private readonly log4net.Core.ILogger _logger;

        /// <summary>
        /// The provider options.
        /// </summary>
        private readonly Log4NetProviderOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        /// <param name="options">
        /// The <see cref="Log4NetProviderOptions"/> used to configure the log4net logger,
        /// including repository and logger name settings.
        /// </param>
        /// <param name="externalScopeProvider">
        /// The <see cref="IExternalScopeProvider"/> used to access the current logging scopes.
        /// <para/>
        /// This provider enables integration with the <c>Microsoft.Extensions.Logging</c> scope system
        /// (for example, values created via <c>ILogger.BeginScope</c>), allowing scope data to be
        /// included in log4net log events.
        /// </param>
        public Log4NetLogger(Log4NetProviderOptions options, IExternalScopeProvider externalScopeProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _externalScopeProvider = externalScopeProvider ?? throw new ArgumentNullException(nameof(externalScopeProvider));
            _logger = LogManager.GetLogger(options.LoggerRepository, options.Name).Logger;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => _logger.Name;

        /// <summary>
        /// A get-only property for accessing the <see cref="Log4NetProviderOptions"/>
        /// within the instance.
        /// </summary>
        internal Log4NetProviderOptions Options => _options;


        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>
        /// An IDisposable that ends the logical operation scope on dispose.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return _externalScopeProvider.Push(state);
        }

        /// <summary>
        /// Determines whether the logging level is enabled.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The <see cref="bool"/> value indicating whether the logging level is enabled.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="logLevel"/> is outside allowed range.</exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            Level translatedLogLevel = _options.LogLevelTranslator.TranslateLogLevel(logLevel, Options);
            if (translatedLogLevel != null)
            {
                return _logger.IsEnabledFor(translatedLogLevel);
            }

            if (logLevel == LogLevel.None)
            {
                return false;
            }

            throw new ArgumentOutOfRangeException(nameof(logLevel));
        }

        /// <summary>
        /// Logs an exception into the log.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="eventId">The event Id.</param>
        /// <param name="state">The state.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatter">The formatter.</param>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="formatter"/> is null.</exception>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            EnsureValidFormatter(formatter);

            var candidate = new MessageCandidate<TState>(logLevel, eventId, state, exception, formatter);

            var loggingEvent = _options.LoggingEventFactory.CreateLoggingEvent(in candidate, _logger, _options, _externalScopeProvider);

            if (loggingEvent == null)
                return;

            _logger.Log(loggingEvent);
        }

        private static void EnsureValidFormatter<TState>(Func<TState, Exception, string> formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
        }
    }
}