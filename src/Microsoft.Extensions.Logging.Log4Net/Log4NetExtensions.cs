using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Logging.Log4Net
{
    /// <summary>
    /// The log4net extensions class.
    /// </summary>
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Adds the log4net.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <returns>The <see cref="ILoggerFactory"/> with added Log4Net provider</returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            return factory.AddLog4Net(new Log4NetProviderOptions());
        }

        /// <summary>
        /// Adds the log4net.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="log4NetConfigFile">The log4net Config File.</param>
        /// <returns>The <see cref="ILoggerFactory"/> after adding the log4net provider.</returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile)
        {
            return factory.AddLog4Net(log4NetConfigFile, false);
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="log4NetConfigFile">The log4 net configuration file.</param>
        /// <param name="watch">if set to <c>true</c> [watch].</param>
        /// <returns>The <see cref="ILoggerFactory"/> after adding the log4net provider.</returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile, bool watch)
        {
            return factory.AddLog4Net(new Log4NetProviderOptions(log4NetConfigFile, watch));
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="factory">The logger factory.</param>
        /// <param name="options">The options for log4net provider.</param>
        /// <returns>The <see cref="ILoggerFactory"/> after adding the log4net provider.</returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, Log4NetProviderOptions options)
        {
            factory.AddProvider(new Log4NetProvider(options));
            return factory;
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="builder">The logging builder instance.</param>
        /// <returns>The <see ref="ILoggingBuilder" /> passed as parameter with the new provider registered.</returns>
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
        {
            var options = new Log4NetProviderOptions();
            return builder.AddLog4Net(options);
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="builder">The logging builder instance.</param>
        /// <param name="log4NetConfigFile">The log4net Config File.</param>
        /// <returns>The <see ref="ILoggingBuilder" /> passed as parameter with the new provider registered.</returns>
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string log4NetConfigFile)
        {
            var options = new Log4NetProviderOptions(log4NetConfigFile);
            return builder.AddLog4Net(options);
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="builder">The logging builder instance.</param>
        /// <param name="log4NetConfigFile">The log4net Config File.</param>
        /// <param name="watch">if set to <c>true</c>, the configuration will be reloaded when the xml configuration file changes.</param>
        /// <returns>
        /// The <see ref="ILoggingBuilder" /> passed as parameter with the new provider registered.
        /// </returns>
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string log4NetConfigFile, bool watch)
        {
            var options = new Log4NetProviderOptions(log4NetConfigFile, watch);
            return builder.AddLog4Net(options);
        }

        /// <summary>
        /// Adds the log4net logging provider.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="ILoggingBuilder"/> instance used to configure logging.
        /// </param>
        /// <param name="options">
        /// The <see cref="Log4NetProviderOptions"/> used to configure the log4net provider.
        /// <para/>
        /// This includes settings such as the log4net configuration file path, repository name,
        /// and other provider-specific behaviors.
        /// </param>
        /// <returns>
        /// The <see cref="ILoggingBuilder"/> instance so that additional calls can be chained.
        /// </returns>
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, Log4NetProviderOptions options)
        {
            builder.Services.AddSingleton<ILoggerProvider>(new Log4NetProvider(options));
            return builder;
        }
    }
}