using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alice.Aop.ServiceLookup
{
    public class AliceServiceDescriptor: ServiceDescriptor
    {
        public Castle.DynamicProxy.IInterceptor[] Interceptors { get; set; }
        public AliceServiceDescriptor(
            Type serviceType, 
            object instance,
            Castle.DynamicProxy.IInterceptor[] interceptors):base(serviceType,instance)
        {
            Interceptors = interceptors;
        }
        
        public AliceServiceDescriptor(Type serviceType, 
            Type implementationType, 
            ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors) : base(serviceType, implementationType, lifetime)
        {
            Interceptors = interceptors;
        }
        
        public AliceServiceDescriptor(Type serviceType, 
            Func<IServiceProvider, object> factory, 
            ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors) : base(serviceType, factory, lifetime)
        {
            Interceptors = interceptors;
        }

        public new static AliceServiceDescriptor Transient<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Transient(Type service, Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Transient<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Transient<TService>(Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Transient(Type service, Func<IServiceProvider, object> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Scoped<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Scoped(Type service, Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            return Describe(service, implementationType, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Scoped<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Scoped(Type service, Func<IServiceProvider, object> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton(Type service, Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton<TService>(Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton(
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(serviceType, implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationInstance"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationInstance">The instance of the implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton<TService>(TService implementationInstance,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return Singleton(typeof(TService), implementationInstance, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationInstance"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationInstance">The instance of the implementation.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Singleton(
            Type serviceType,
            object implementationInstance,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return new AliceServiceDescriptor(serviceType, implementationInstance, interceptors);
        }

        private new static AliceServiceDescriptor Describe<TService, TImplementation>(ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(
                typeof(TService),
                typeof(TImplementation),
                lifetime: lifetime, 
                interceptors: interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationType"/>,
        /// and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Describe(Type serviceType, Type implementationType, ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            return new AliceServiceDescriptor(serviceType, implementationType, lifetime, interceptors);
        }

        /// <summary>
        /// Creates an instance of <see cref="AliceServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A new instance of <see cref="AliceServiceDescriptor"/>.</returns>
        public new static AliceServiceDescriptor Describe(Type serviceType, 
            Func<IServiceProvider, object> implementationFactory, 
            ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            return new AliceServiceDescriptor(serviceType, implementationFactory, lifetime, interceptors);
        }

    }
}
