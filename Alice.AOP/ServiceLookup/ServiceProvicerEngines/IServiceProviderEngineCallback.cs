// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Alice.Aop.ServiceLookup.CallSite;
using Alice.Aop.ServiceLookup.CallSite.BaseClass;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop.ServiceLookup.ServiceProvicerEngines
{
    internal interface IServiceProviderEngineCallback
    {
        void OnCreate(IServiceCallSite callSite);
        void OnResolve(Type serviceType, IServiceScope scope);
    }
}