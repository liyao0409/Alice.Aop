using System;
using Alice.Aop.ServiceContainer;
using Microsoft.Extensions.DependencyInjection;
using ServiceProviderOptions = Alice.Aop.ServiceProviderOptions;

namespace Alice.Aop
{
    /// <summary>
    /// Default implementation of <see cref="IServiceProviderFactory{TContainerBuilder}"/>.
    /// </summary>
    public class DefaultServiceProviderFactory
    {
        private readonly ServiceProviderOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with default options.
        /// </summary>
        /// <seealso cref="ServiceProviderOptions.Default"/>
        public DefaultServiceProviderFactory() : this(ServiceProviderOptions.Default)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The options to use for this instance.</param>
        public DefaultServiceProviderFactory(ServiceProviderOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
        }

        /// <inheritdoc />
        public AliceServiceCollection CreateBuilder(AliceServiceCollection services)
        {
            return services;
        }

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider(AliceServiceCollection containerBuilder)
        {
            return containerBuilder.BuildServiceProvider(_options);
        }
    }
}
