// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Alice.Aop.ServiceLookup.ServiceProvicerEngines
{
    internal interface IServiceProviderEngine : IDisposable, IServiceProvider
    {
        IServiceScope RootScope { get; }
    }
}