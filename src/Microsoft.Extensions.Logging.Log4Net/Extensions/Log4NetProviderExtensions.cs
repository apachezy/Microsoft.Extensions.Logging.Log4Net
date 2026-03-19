using System;

namespace Microsoft.Extensions.Logging.Log4Net.Extensions
{
    /// <summary>
    /// Log4Net provider extensions.
    /// </summary>
    public static class Log4NetProviderExtensions
    {
        /// <summary>
        /// Creates a logger with the name of the given <see cref="TLogger"/> type.
        /// </summary>
        /// <typeparam name="TLogger">The type of the class to be used as name of the logger.</typeparam>
        /// <param name="provider">An ILoggerProvider instance.</param>
        /// <returns>An instance of the <see cref="ILogger"/>.</returns>
        public static ILogger CreateLog4Logger<TLogger>(this ILoggerProvider provider) where TLogger : class
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (!provider.GetType().IsAssignableFrom(typeof(Log4NetProvider)))
            {
                throw new ArgumentOutOfRangeException(nameof(provider), "The ILoggerProvider should be of type Log4NetProvider.");
            }

            var type = typeof(TLogger);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = type.Name;
            }

            return provider.CreateLogger(name);
        }
    }
}