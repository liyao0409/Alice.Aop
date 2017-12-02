// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.ExceptionServices;
using Castle.DynamicProxy;

namespace Alice.Aop.Di.ServiceLookup
{
    internal class CreateInstanceCallSite : IServiceCallSite
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public IInterceptor[] Interceptors { get; }

        public CreateInstanceCallSite(Type serviceType, Type implementationType, IInterceptor[] interceptors)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Interceptors = interceptors;
        }
    }
}
