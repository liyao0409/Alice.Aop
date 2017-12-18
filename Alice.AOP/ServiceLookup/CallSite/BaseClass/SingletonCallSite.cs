// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Alice.Aop.ServiceLookup.CallSite.BaseClass
{
    internal class SingletonCallSite : ScopedCallSite
    {
        public SingletonCallSite(IServiceCallSite serviceCallSite, object cacheKey) : base(serviceCallSite, cacheKey)
        {
        }
    }
}