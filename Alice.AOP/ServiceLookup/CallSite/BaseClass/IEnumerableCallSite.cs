// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class IEnumerableCallSite : IServiceCallSite
    {
        internal Type ItemType { get; }
        internal IServiceCallSite[] ServiceCallSites { get; }
        public IEnumerableCallSite(Type itemType, IServiceCallSite[] serviceCallSites)
        {
            ItemType = itemType;
            ServiceCallSites = serviceCallSites;
        }

        public Type ServiceType => typeof(IEnumerable<>).MakeGenericType(ItemType);
        public Type ImplementationType  => ItemType.MakeArrayType();

    }
}