// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Castle.DynamicProxy;

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class ServiceProviderCallSite : IServiceCallSite
    {
        public Type ServiceType { get; } = typeof(IServiceProvider);
        public Type ImplementationType { get; } = typeof(ServiceProvider);
        public IInterceptor[] Interceptors { get; } = null;
    }
}
