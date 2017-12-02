// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace Alice.Aop.Di.ServiceLookup
{
    internal class ConstructorCallSite : IServiceCallSite
    {
        internal ConstructorInfo ConstructorInfo { get; }
        internal IServiceCallSite[] ParameterCallSites { get; }

        public ConstructorCallSite(Type serviceType, 
            ConstructorInfo constructorInfo, 
            IServiceCallSite[] parameterCallSites,
            IInterceptor[] interceptors)
        {
            ServiceType = serviceType;
            ConstructorInfo = constructorInfo;
            ParameterCallSites = parameterCallSites;
            Interceptors = interceptors;
        }

        public Type ServiceType { get; }

        public Type ImplementationType => ConstructorInfo.DeclaringType;
        public Castle.DynamicProxy.IInterceptor[] Interceptors { get; }
    }
}
