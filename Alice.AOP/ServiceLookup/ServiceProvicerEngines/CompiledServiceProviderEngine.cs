// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Alice.Aop.Scope;
using Alice.Aop.ServiceLookup.CallSite;
using Alice.Aop.ServiceLookup.CallSite.BaseClass;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop.ServiceLookup.ServiceProvicerEngines
{
    internal class CompiledServiceProviderEngine : ServiceProviderEngine
    {
        public CompiledServiceProviderEngine(IEnumerable<ServiceDescriptor> serviceDescriptors, IServiceProviderEngineCallback callback) : base(serviceDescriptors, callback)
        {
        }

        protected override Func<ServiceProviderEngineScope, object> RealizeService(IServiceCallSite callSite)
        {
            var realizedService = ExpressionBuilder.Build(callSite);
            RealizedServices[callSite.ServiceType] = realizedService;
            return realizedService;
        }
    }
}