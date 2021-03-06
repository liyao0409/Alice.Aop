// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class ConstantCallSite : IServiceCallSite
    {
        internal object DefaultValue { get; }

        public ConstantCallSite(Type serviceType, object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public Type ServiceType => DefaultValue.GetType();
        public Type ImplementationType => DefaultValue.GetType();
    }
}
