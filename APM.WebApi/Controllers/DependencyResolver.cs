using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace APM.WebApi.Controllers
{
    /// <summary>
    /// Dependency resolver for Unity Dependency Injection
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        /// <summary>
        /// Creates a Unity container for DI
        /// </summary>
        /// <param name="container"></param>
        public UnityResolver(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException("container");
        }

        /// <summary>
        /// Returns a Service object
        /// </summary>
        /// <param name="serviceType">Type of service</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a enumerator of service objects
        /// </summary>
        /// <param name="serviceType">Type of Service</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Initiate a scope
        /// </summary>
        /// <returns>Returns a dependancy scope object</returns>
        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        /// <summary>
        /// Dispose of Unity Container
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }

    }
}