// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Castle.DynamicProxy;
using System;

namespace Alice.Aop.Di.ServiceLookup
{
    internal class TransientCallSite : IServiceCallSite
    {
        internal IServiceCallSite ServiceCallSite { get; }

        public TransientCallSite(IServiceCallSite serviceCallSite)
        {
            ServiceCallSite = serviceCallSite;
        }

        public Type ServiceType => ServiceCallSite.ServiceType;
        public Type ImplementationType => ServiceCallSite.ImplementationType;
        public IInterceptor[] Interceptors => ServiceCallSite.Interceptors;
    }
}