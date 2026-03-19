using System;
using log4net;

namespace Microsoft.Extensions.Logging.Log4Net.Extensions
{
    /// <summary>
    /// Provides extension methods for writing log entries at the <c>Critical</c> and <c>Trace</c> levels.
    /// </summary>
    /// <remarks>
    /// These methods delegate to the underlying log4net logging infrastructure and require an instance
    /// of <see cref="ILog"/>.
    /// <para/>
    /// The <c>Critical</c> level is typically used for severe errors that may cause application failure,
    /// while the <c>Trace</c> level is intended for highly detailed diagnostic information.
    /// <para/>
    /// Internally, these methods pass a fixed declaring type to log4net in order to correctly establish
    /// the logging stack boundary, ensuring accurate caller information in log output.
    /// </remarks>
    public static class LogExtensions
    {
        private static readonly Type _declaringType = typeof(LogExtensions);

        /// <summary>
        /// Writes a log entry at the <c>Critical</c> level.
        /// </summary>
        /// <param name="log">The <see cref="ILog"/> instance used to write the log entry.</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception associated with the log entry, if any.</param>
        public static void Critical(this ILog log, object message, Exception exception)
        {
            log.Logger.Log(_declaringType, log4net.Core.Level.Critical, message, exception);
        }

        /// <summary>
        /// Writes a log entry at the <c>Trace</c> level.
        /// </summary>
        /// <param name="log">The <see cref="ILog"/> instance used to write the log entry.</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception associated with the log entry, if any.</param>
        public static void Trace(this ILog log, object message, Exception exception)
        {
            log.Logger.Log(_declaringType, log4net.Core.Level.Trace, message, exception);
        }
    }
}