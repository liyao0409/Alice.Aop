// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Alice.Aop.ServiceLookup;
using Alice.Aop.ServiceLookup.CallSite;
using Alice.Aop.ServiceLookup.CallSite.BaseClass;
using Alice.Aop.ServiceLookup.ServiceProvicerEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop
{
    /// <summary>
    /// The default IServiceProvider.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider, IDisposable, IServiceProviderEngineCallback
    {
        private readonly IServiceProviderEngine _engine;

        private readonly CallSiteValidator _callSiteValidator;

        internal ServiceProvider(IEnumerable<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            IServiceProviderEngineCallback callback = null;
            if (options.ValidateScopes)
            {
                callback = this;
                _callSiteValidator = new CallSiteValidator();
            }
            switch (options.Mode)
            {
                case ServiceProviderMode.Dynamic:
                    _engine = new DynamicServiceProviderEngine(serviceDescriptors, callback);
                    break;
                case ServiceProviderMode.Runtime:
                    _engine = new RuntimeServiceProviderEngine(serviceDescriptors, callback);
                    break;
                case ServiceProviderMode.Compiled:
                    _engine = new CompiledServiceProviderEngine(serviceDescriptors, callback);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Mode));
            }
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType) => _engine.GetService(serviceType);

        /// <inheritdoc />
        public void Dispose() => _engine.Dispose();

        void IServiceProviderEngineCallback.OnCreate(IServiceCallSite callSite)
        {
            _callSiteValidator.ValidateCallSite(callSite);
        }

        void IServiceProviderEngineCallback.OnResolve(Type serviceType, IServiceScope scope)
        {
            _callSiteValidator.ValidateResolution(serviceType, scope, _engine.RootScope);
        }
    }
}
