using Castle.DynamicProxy;
using System;
using System.Runtime.ExceptionServices;

namespace Alice.Aop.Di.ServiceLookup
{
    internal class CallSiteRuntimeResolver : CallSiteVisitor<ServiceProviderEngineScope, object>
    {
        public object Resolve(IServiceCallSite callSite, ServiceProviderEngineScope scope)
        {
            return VisitCallSite(callSite, scope);
        }

        protected override object VisitTransient(TransientCallSite transientCallSite, ServiceProviderEngineScope scope)
        {
            return scope.CaptureDisposable(
                VisitCallSite(transientCallSite.ServiceCallSite, scope));
        }

        protected override object VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProviderEngineScope scope)
        {
            object[] parameterValues = new object[constructorCallSite.ParameterCallSites.Length];
            for (var index = 0; index < parameterValues.Length; index++)
            {
                parameterValues[index] = VisitCallSite(constructorCallSite.ParameterCallSites[index], scope);
            }

            try
            {
                return constructorCallSite.ConstructorInfo.Invoke(parameterValues);
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                // The above line will always throw, but the compiler requires we throw explicitly.
                throw;
            }
        }

        protected override object VisitSingleton(SingletonCallSite singletonCallSite, ServiceProviderEngineScope scope)
        {
            return VisitScoped(singletonCallSite, scope.Engine.Root);
        }

        protected override object VisitScoped(ScopedCallSite scopedCallSite, ServiceProviderEngineScope scope)
        {
            lock (scope.ResolvedServices)
            {
                if (!scope.ResolvedServices.TryGetValue(scopedCallSite.CacheKey, out var resolved))
                {
                    resolved = VisitCallSite(scopedCallSite.ServiceCallSite, scope);
                    scope.CaptureDisposable(resolved);
                    scope.ResolvedServices.Add(scopedCallSite.CacheKey, resolved);
                }
                return resolved;
            }
        }

        protected override object VisitConstant(ConstantCallSite constantCallSite, ServiceProviderEngineScope scope)
        {
            return constantCallSite.DefaultValue;
        }

        protected override object VisitCreateInstance(CreateInstanceCallSite createInstanceCallSite, ServiceProviderEngineScope scope)
        {
            try
            {
                object obj;
                if (createInstanceCallSite.Interceptors != null && createInstanceCallSite.Interceptors.Length > 0)
                {
                    Castle.DynamicProxy.ProxyGenerator generator = new Castle.DynamicProxy.ProxyGenerator();
                    obj = generator.CreateClassProxy(createInstanceCallSite.ImplementationType, 
                        new[] {createInstanceCallSite.ServiceType},
                        createInstanceCallSite.Interceptors);
                }
                else
                {
                    obj = Activator.CreateInstance(createInstanceCallSite.ImplementationType);
                }
                //return Activator.CreateInstance(createInstanceCallSite.ImplementationType);
                return obj;
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                // The above line will always throw, but the compiler requires we throw explicitly.
                throw;
            }
        }

        protected override object VisitServiceProvider(ServiceProviderCallSite serviceProviderCallSite, ServiceProviderEngineScope scope)
        {
            return scope;
        }

        protected override object VisitServiceScopeFactory(ServiceScopeFactoryCallSite serviceScopeFactoryCallSite, ServiceProviderEngineScope scope)
        {
            return scope.Engine;
        }

        protected override object VisitIEnumerable(IEnumerableCallSite enumerableCallSite, ServiceProviderEngineScope scope)
        {
            var array = Array.CreateInstance(
                enumerableCallSite.ItemType,
                enumerableCallSite.ServiceCallSites.Length);

            for (var index = 0; index < enumerableCallSite.ServiceCallSites.Length; index++)
            {
                var value = VisitCallSite(enumerableCallSite.ServiceCallSites[index], scope);
                array.SetValue(value, index);
            }
            return array;
        }

        protected override object VisitFactory(FactoryCallSite factoryCallSite, ServiceProviderEngineScope scope)
        {
            var obj = factoryCallSite.Factory(scope);
            if (factoryCallSite.Interceptors != null && factoryCallSite.Interceptors.Length > 0)
            {
                Castle.DynamicProxy.ProxyGenerator generator = new Castle.DynamicProxy.ProxyGenerator();
                obj = generator.CreateClassProxyWithTarget(
                    obj.GetType(),
                    new[] { factoryCallSite.ServiceType },
                    obj,
                    factoryCallSite.Interceptors);
            }
            return obj;
        }
    }
}