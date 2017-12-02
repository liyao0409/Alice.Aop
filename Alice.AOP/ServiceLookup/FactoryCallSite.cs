// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Alice.Aop.Di.ServiceLookup
{
    internal class FactoryCallSite : IServiceCallSite
    {
        public Func<IServiceProvider, object> Factory { get; }
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public Castle.DynamicProxy.IInterceptor[] Interceptors { get; }
        public FactoryCallSite(Type serviceType, 
            Func<IServiceProvider, object> factory, 
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            Factory = factory;
            ServiceType = serviceType;
            ImplementationType = null;
            Interceptors = interceptors;
        }
    }
}
