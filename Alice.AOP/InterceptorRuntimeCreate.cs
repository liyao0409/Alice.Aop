using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using Castle.DynamicProxy;

namespace Alice.Aop
{
    public class InterceptorRuntimeCreate
    {
        private static readonly List<Func<IServiceProvider,IInterceptor>> InterceptorFuncs = new List<Func<IServiceProvider, IInterceptor>>();
        public static readonly List<IInterceptor> CreatedInterceptors = new List<IInterceptor>();
        private static readonly List<string> InterceptableNamespaces = new List<string>();
        private static readonly List<Regex> InterceptableRegex = new List<Regex>();

        public static void AddInterceptorFunc(Func<IServiceProvider, IInterceptor> func)
        {
            InterceptorFuncs.Add(func);
        }

        public static List<IInterceptor> CreateInterceptors(IServiceProvider serviceProvider)
        {
            if (CreatedInterceptors.Count == 0)
            {
                foreach (var interceptorFunc in InterceptorFuncs)
                {
                    var interceptor = interceptorFunc.Invoke(serviceProvider);
                    CreatedInterceptors.Add(interceptor);
                }
            }
            return CreatedInterceptors;
        }

        public static void AddInterceptableQualifyName(string qualifyName)
        {
            if (!InterceptableNamespaces.Contains(qualifyName.ToLower()))
            {
                InterceptableNamespaces.Add(qualifyName.ToLower());
                Regex reg = new Regex(qualifyName.ToLower(),RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
                InterceptableRegex.Add(reg);
            }
        }

        public static bool CanIntercept(string implementName)
        {
            bool canIntercept = false;
            foreach (var reg in InterceptableRegex)
            {
                if (reg.IsMatch(implementName))
                {
                    canIntercept = true;
                    break;
                }
            }
            return canIntercept;
        }
    }
}
