// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alice.Aop.Scope;
using Alice.Aop.ServiceLookup.CallSite;
using Alice.Aop.ServiceLookup.CallSite.BaseClass;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop.ServiceLookup.ServiceProvicerEngines
{
    internal class DynamicServiceProviderEngine : CompiledServiceProviderEngine
    {
        public DynamicServiceProviderEngine(IEnumerable<ServiceDescriptor> serviceDescriptors, IServiceProviderEngineCallback callback) : base(serviceDescriptors, callback)
        {
        }

        protected override Func<ServiceProviderEngineScope, object> RealizeService(IServiceCallSite callSite)
        {
            var callCount = 0;
            return scope =>
            {
                //if (Interlocked.Increment(ref callCount) == 2)
                //{
                //    Task.Run(() => base.RealizeService(callSite));
                //}
                return RuntimeResolver.Resolve(callSite, scope);
            };
        }
    }
}