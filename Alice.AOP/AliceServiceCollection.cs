// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Alice.Aop.ServiceLookup;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Alice.Aop.Di
{
    /// <summary>
    /// Default implementation of <see cref="IServiceCollection"/>.
    /// </summary>
    public class AliceServiceCollection : IList<ServiceDescriptor>,
        ICollection<ServiceDescriptor>,
        IEnumerable<ServiceDescriptor>,
        IEnumerable
    {
        private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();

        /// <inheritdoc />
        public int Count => _descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public ServiceDescriptor this[int index]
        {
            get
            {
                return _descriptors[index];
            }
            set
            {
                _descriptors[index] = value;
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc />
        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc />
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public int IndexOf(ServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, ServiceDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        //-----------extension----------------------


        /// <summary>
        /// Adds the specified <paramref name="descriptor"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to add.</param>
        /// <returns>A reference to the current instance of <see cref="IServiceCollection"/>.</returns>
        public AliceServiceCollection Add(
            ServiceDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            this._descriptors.Add(descriptor);
            return this;
        }

        /// <summary>
        /// Adds a sequence of <see cref="ServiceDescriptor"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="descriptors">The <see cref="ServiceDescriptor"/>s to add.</param>
        /// <returns>A reference to the current instance of <see cref="IServiceCollection"/>.</returns>
        public AliceServiceCollection Add(
            IEnumerable<ServiceDescriptor> descriptors)
        {
            if (descriptors == null)
            {
                throw new ArgumentNullException(nameof(descriptors));
            }

            foreach (var descriptor in descriptors)
            {
                _descriptors.Add(descriptor);
            }

            return this;
        }

        /// <summary>
        /// Adds the specified <paramref name="descriptor"/> to the <paramref name="collection"/> if the
        /// service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to add.</param>
        public void TryAdd(
            ServiceDescriptor descriptor)
        {
            
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (!_descriptors.Any(d => d.ServiceType == descriptor.ServiceType))
            {
                _descriptors.Add(descriptor);
            }
        }

        /// <summary>
        /// Adds the specified <paramref name="descriptors"/> to the <paramref name="collection"/> if the
        /// service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="descriptors">The <see cref="ServiceDescriptor"/>s to add.</param>
        public void TryAdd(
            IEnumerable<ServiceDescriptor> descriptors)
        {
            if (descriptors == null)
            {
                throw new ArgumentNullException(nameof(descriptors));
            }

            foreach (var d in descriptors)
            {
                TryAdd(d);
            }
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Transient"/> service
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        public void TryAddTransient(
            Type service,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var descriptor = ServiceDescriptor.Transient(service, service);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Transient"/> service
        /// with the <paramref name="implementationType"/> implementation
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        public void TryAddTransient(
            Type service,
            Type implementationType,
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
            
            var descriptor = AliceServiceDescriptor.Transient(service, implementationType,interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Transient"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddTransient(
            Type service,
            Func<IServiceProvider, object> implementationFactory,
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

            var descriptor = AliceServiceDescriptor.Transient(service, implementationFactory, interceptors);
            TryAdd(descriptor);
        }

        public void TryAddTransient<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAddTransient(typeof(TService), typeof(TService), interceptors);
        }

        public void TryAddTransient<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            TryAddTransient(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Transient"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="services"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddTransient<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAdd(AliceServiceDescriptor.Transient(implementationFactory, interceptors));
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        public void TryAddScoped(
            Type service,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var descriptor = AliceServiceDescriptor.Scoped(service, service, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// with the <paramref name="implementationType"/> implementation
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        public void TryAddScoped(
            Type service,
            Type implementationType,
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

            var descriptor = AliceServiceDescriptor.Scoped(service, implementationType, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddScoped(
            Type service,
            Func<IServiceProvider, object> implementationFactory,
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

            var descriptor = AliceServiceDescriptor.Scoped(service, implementationFactory, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        public void TryAddScoped<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAddScoped(typeof(TService), typeof(TService), interceptors);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// implementation type specified in <typeparamref name="TImplementation"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        public void TryAddScoped<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            TryAddScoped(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Scoped"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="services"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddScoped<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAdd(AliceServiceDescriptor.Scoped(implementationFactory, interceptors));
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        public void TryAddSingleton(
            Type service,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var descriptor = AliceServiceDescriptor.Singleton(service, service, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// with the <paramref name="implementationType"/> implementation
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        public void TryAddSingleton(
            Type service,
            Type implementationType,
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

            var descriptor = AliceServiceDescriptor.Singleton(service, implementationType, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <paramref name="service"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="service">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddSingleton(
            Type service,
            Func<IServiceProvider, object> implementationFactory,
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

            var descriptor = AliceServiceDescriptor.Singleton(service, implementationFactory, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        public void TryAddSingleton<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAddSingleton(typeof(TService), typeof(TService), interceptors);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// implementation type specified in <typeparamref name="TImplementation"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        public void TryAddSingleton<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            TryAddSingleton(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// with an instance specified in <paramref name="instance"/>
        /// to the <paramref name="collection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="instance">The instance of the service to add.</param>
        public void TryAddSingleton<TService>(TService instance,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var descriptor = AliceServiceDescriptor.Singleton(typeof(TService), instance, interceptors);
            TryAdd(descriptor);
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/> service
        /// using the factory specified in <paramref name="implementationFactory"/>
        /// to the <paramref name="services"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        public void TryAddSingleton<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            TryAdd(AliceServiceDescriptor.Singleton(implementationFactory, interceptors));
        }

        /// <summary>
        /// Adds a <see cref="ServiceDescriptor"/> if an existing descriptor with the same
        /// <see cref="ServiceDescriptor.ServiceType"/> and an implementation that does not already exist
        /// in <paramref name="services."/>.
        /// </summary>
        /// <param name="services">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/>.</param>
        /// <remarks>
        /// Use <see cref="TryAddEnumerable(AliceServiceCollection, ServiceDescriptor)"/> when registing a service implementation of a
        /// service type that
        /// supports multiple registrations of the same service type. Using
        /// <see cref="Add(AliceServiceCollection, ServiceDescriptor)"/> is not idempotent and can add
        /// duplicate
        /// <see cref="ServiceDescriptor"/> instances if called twice. Using
        /// <see cref="TryAddEnumerable(AliceServiceCollection, ServiceDescriptor)"/> will prevent registration
        /// of multiple implementation types.
        /// </remarks>
        public void TryAddEnumerable(
            ServiceDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var implementationType = descriptor.GetImplementationType();

            if (implementationType == typeof(object) ||
                implementationType == descriptor.ServiceType)
            {
                throw new ArgumentException(
                    Resources.FormatTryAddIndistinguishableTypeToEnumerable(
                        implementationType,
                        descriptor.ServiceType),
                    nameof(descriptor));
            }

            if (!_descriptors.Any(d =>
                              d.ServiceType == descriptor.ServiceType &&
                              d.GetImplementationType() == implementationType))
            {
                _descriptors.Add(descriptor);
            }
        }

        /// <summary>
        /// Adds the specified <see cref="ServiceDescriptor"/>s if an existing descriptor with the same
        /// <see cref="ServiceDescriptor.ServiceType"/> and an implementation that does not already exist
        /// in <paramref name="services."/>.
        /// </summary>
        /// <param name="services">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="descriptors">The <see cref="ServiceDescriptor"/>s.</param>
        /// <remarks>
        /// Use <see cref="TryAddEnumerable(AliceServiceCollection, ServiceDescriptor)"/> when registing a service
        /// implementation of a service type that
        /// supports multiple registrations of the same service type. Using
        /// <see cref="Add(AliceServiceCollection, ServiceDescriptor)"/> is not idempotent and can add
        /// duplicate
        /// <see cref="ServiceDescriptor"/> instances if called twice. Using
        /// <see cref="TryAddEnumerable(AliceServiceCollection, ServiceDescriptor)"/> will prevent registration
        /// of multiple implementation types.
        /// </remarks>
        public void TryAddEnumerable(
            IEnumerable<ServiceDescriptor> descriptors)
        {
            if (descriptors == null)
            {
                throw new ArgumentNullException(nameof(descriptors));
            }

            foreach (var d in descriptors)
            {
                TryAddEnumerable(d);
            }
        }

        /// <summary>
        /// Removes the first service in <see cref="AliceServiceCollection"/> with the same service type
        /// as <paramref name="descriptor"/> and adds <paramef name="descriptor"/> to the collection.
        /// </summary>
        /// <param name="collection">The <see cref="AliceServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <returns></returns>
        public AliceServiceCollection Replace(
            ServiceDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var registeredServiceDescriptor = _descriptors.FirstOrDefault(s => s.ServiceType == descriptor.ServiceType);
            if (registeredServiceDescriptor != null)
            {
                Remove(registeredServiceDescriptor);
            }

            Add(descriptor);
            return this;
        }

        /// <summary>
        /// Removes all services of type <typeparamef name="T"/> in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <returns></returns>
        public AliceServiceCollection RemoveAll<T>()
        {
            return RemoveAll(typeof(T));
        }

        /// <summary>
        /// Removes all services of type <paramef name="serviceType"/> in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="serviceType">The service type to remove.</param>
        /// <returns></returns>
        public AliceServiceCollection RemoveAll(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            for (var i = _descriptors.Count - 1; i >= 0; i--)
            {
                var descriptor = _descriptors[i];
                if (descriptor.ServiceType == serviceType)
                {
                    _descriptors.RemoveAt(i);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient(
            Type serviceType,
            Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(serviceType, implementationType, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient(
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

            return Add(serviceType, implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return AddTransient(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient(
            Type serviceType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return AddTransient(serviceType, serviceType, interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            return AddTransient(typeof(TService), interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddTransient(typeof(TService), implementationFactory, interceptors);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public AliceServiceCollection AddTransient<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddTransient(typeof(TService), implementationFactory, interceptors);
        }



        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped(
            Type serviceType,
            Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(serviceType, implementationType, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped(
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

            return Add(serviceType, implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return AddScoped(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped(
            Type serviceType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return AddScoped(serviceType, serviceType, interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            return AddScoped(typeof(TService), interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddScoped(typeof(TService), implementationFactory, interceptors);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public AliceServiceCollection AddScoped<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddScoped(typeof(TService), implementationFactory, interceptors);
        }


        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton(
            Type serviceType,
            Type implementationType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(serviceType, implementationType, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton(
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

            return Add(serviceType, implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton<TService, TImplementation>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return AddSingleton(typeof(TService), typeof(TImplementation), interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton(
            Type serviceType,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return AddSingleton(serviceType, serviceType, interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton<TService>(
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            return AddSingleton(typeof(TService), interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton<TService>(
            Func<IServiceProvider, TService> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddSingleton(typeof(TService), implementationFactory, interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return AddSingleton(typeof(TService), implementationFactory, interceptors);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton(
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

            var serviceDescriptor = new AliceServiceDescriptor(serviceType, implementationInstance, interceptors);
            Add(serviceDescriptor);
            return this;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public AliceServiceCollection AddSingleton<TService>(
            TService implementationInstance,
            Castle.DynamicProxy.IInterceptor[] interceptors)
            where TService : class
        {
            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return AddSingleton(typeof(TService), implementationInstance, interceptors);
        }

        private AliceServiceCollection Add(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            var descriptor = new AliceServiceDescriptor(serviceType, implementationType, lifetime, interceptors);
            Add(descriptor);
            return this;
        }

        private AliceServiceCollection Add(
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            ServiceLifetime lifetime,
            Castle.DynamicProxy.IInterceptor[] interceptors)
        {
            var descriptor = new AliceServiceDescriptor(serviceType, implementationFactory, lifetime, interceptors);
            Add(descriptor);
            return this;
        }

        public void Populate(Microsoft.Extensions.DependencyInjection.IServiceCollection msServices)
        {
            foreach (var descriptor in msServices)
            {
                this._descriptors.Add(descriptor);
            }
        }
    }
}