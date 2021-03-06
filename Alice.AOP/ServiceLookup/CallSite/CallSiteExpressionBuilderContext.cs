// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;

namespace Alice.Aop.ServiceLookup.CallSite
{
    internal class CallSiteExpressionBuilderContext
    {
        public ParameterExpression ScopeParameter { get; set; }
        public bool RequiresResolvedServices { get; set; }
    }
}