// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Alice.Aop.Di
{
    /// <summary>
    /// Extension methods for building a <see cref="ServiceProvider"/> from an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Creates an <see cref="IServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing service descriptors.</param>
        /// <returns>The <see cref="IServiceProvider"/>.</returns>

        public static ServiceProvider BuildServiceProvider(this AliceServiceCollection services)
        {
            return BuildServiceProvider(services, ServiceProviderOptions.Default);
        }

        /// <summary>
        /// Creates an <see cref="IServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>
        /// optionaly enabling scope validation.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing service descriptors.</param>
        /// <param name="validateScopes">
        /// <c>true</c> to perform check verifying that scoped services never gets resolved from root provider; otherwise <c>false</c>.
        /// </param>
        /// <returns>The <see cref="IServiceProvider"/>.</returns>
        public static ServiceProvider BuildServiceProvider(this AliceServiceCollection services, bool validateScopes)
        {
            return services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = validateScopes });
        }

        /// <summary>
        /// Creates an <see cref="IServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>
        /// optionaly enabling scope validation.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing service descriptors.</param>
        /// <param name="options">
        /// Configures various service provider behaviors.
        /// </param>
        /// <returns>The <see cref="IServiceProvider"/>.</returns>
        public static ServiceProvider BuildServiceProvider(this AliceServiceCollection services, ServiceProviderOptions options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return new ServiceProvider(services, options);
        }
    }
}