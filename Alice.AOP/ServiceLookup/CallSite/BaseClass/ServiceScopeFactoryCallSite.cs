// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Alice.Aop.ServiceLookup.ServiceProvicerEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class ServiceScopeFactoryCallSite : IServiceCallSite
    {
        public Type ServiceType { get; } = typeof(IServiceScopeFactory);
        public Type ImplementationType { get; } = typeof(ServiceProviderEngine);
    }
}
