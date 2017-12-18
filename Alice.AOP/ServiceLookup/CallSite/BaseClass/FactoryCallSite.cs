// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class FactoryCallSite : IServiceCallSite
    {
        public Func<IServiceProvider, object> Factory { get; }
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public FactoryCallSite(Type serviceType, 
            Func<IServiceProvider, object> factory)
        {
            Factory = factory;
            ServiceType = serviceType;
            ImplementationType = null;
        }
    }
}
