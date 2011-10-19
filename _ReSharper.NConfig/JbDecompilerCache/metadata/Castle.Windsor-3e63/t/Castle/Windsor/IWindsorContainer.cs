// Type: Castle.Windsor.IWindsorContainer
// Assembly: Castle.Windsor, Version=2.5.1.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc
// Assembly location: E:\Work\Sources\nconfig\lib\Castle\Castle.Windsor.dll

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System;
using System.Collections;
using System.ComponentModel;

namespace Castle.Windsor
{
    public interface IWindsorContainer : IServiceProviderEx, IServiceProvider, IDisposable
    {
        [Obsolete("Use Resolve<object>(key) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        object this[string key] { get; }

        [Obsolete("Use Resolve(service) or generic version instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        object this[Type service] { get; }

        IKernel Kernel { get; }
        string Name { get; }
        IWindsorContainer Parent { get; set; }
        void AddChildContainer(IWindsorContainer childContainer);

        [Obsolete("Use Register(Component.For(classType).Named(key)) or generic version instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponent(string key, Type classType);

        [Obsolete("Use Register(Component.For(serviceType).ImplementedBy(classType).Named(key)) or generic version instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponent(string key, Type serviceType, Type classType);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For<T>()) instead.")]
        IWindsorContainer AddComponent<T>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For<T>().Named(key)) instead.")]
        IWindsorContainer AddComponent<T>(string key);

        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>()) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponent<I, T>() where T : class;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>().Named(key)) instead.")]
        IWindsorContainer AddComponent<I, T>(string key) where T : class;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For(classType).Named(key).Lifestyle.Is(lifestyle)) or generic version instead.")]
        IWindsorContainer AddComponentLifeStyle(string key, Type classType, LifestyleType lifestyle);

        [Obsolete("Use Register(Component.For(serviceType).ImplementedBy(classType).Named(key).Lifestyle.Is(lifestyle)) or generic version instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentLifeStyle(string key, Type serviceType, Type classType, LifestyleType lifestyle);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For<T>().Lifestyle.Is(lifestyle)) instead.")]
        IWindsorContainer AddComponentLifeStyle<T>(LifestyleType lifestyle);

        [Obsolete("Use Register(Component.For<T>().Named(key).Lifestyle.Is(lifestyle)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentLifeStyle<T>(string key, LifestyleType lifestyle);

        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>().Lifestyle.Is(lifestyle)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentLifeStyle<I, T>(LifestyleType lifestyle) where T : class;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>().Named(key).Lifestyle.Is(lifestyle)) instead.")]
        IWindsorContainer AddComponentLifeStyle<I, T>(string key, LifestyleType lifestyle) where T : class;

        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>().ExtendedProperties(extendedProperties)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentProperties<I, T>(IDictionary extendedProperties) where T : class;

        [Obsolete("Use Register(Component.For<I>().ImplementedBy<T>().Named(key).ExtendedProperties(extendedProperties)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentProperties<I, T>(string key, IDictionary extendedProperties) where T : class;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For(classType).Named(key).ExtendedProperties(extendedProperties)) or generic version instead.")]
        IWindsorContainer AddComponentWithProperties(string key, Type classType, IDictionary extendedProperties);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Register(Component.For(serviceType).ImplementedBy(classType).Named(key).ExtendedProperties(extendedProperties)) or generic version instead.")]
        IWindsorContainer AddComponentWithProperties(string key, Type serviceType, Type classType, IDictionary extendedProperties);

        [Obsolete("Use Register(Component.For<T>().ExtendedProperties(extendedProperties)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentWithProperties<T>(IDictionary extendedProperties);

        [Obsolete("Use Register(Component.For<T>().Named(key).ExtendedProperties(extendedProperties)) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        IWindsorContainer AddComponentWithProperties<T>(string key, IDictionary extendedProperties);

        IWindsorContainer AddFacility(string key, IFacility facility);
        IWindsorContainer AddFacility<T>(string key) where T : new(), IFacility;
        IWindsorContainer AddFacility<T>(string key, Action<T> onCreate) where T : new(), IFacility;
        IWindsorContainer AddFacility<T>(string key, Func<T, object> onCreate) where T : new(), IFacility;
        IWindsorContainer AddFacility<T>() where T : new(), IFacility;
        IWindsorContainer AddFacility<T>(Action<T> onCreate) where T : new(), IFacility;
        IWindsorContainer AddFacility<T>(Func<T, object> onCreate) where T : new(), IFacility;
        IWindsorContainer GetChildContainer(string name);
        IWindsorContainer Install(params IWindsorInstaller[] installers);
        IWindsorContainer Register(params IRegistration[] registrations);
        void Release(object instance);
        void RemoveChildContainer(IWindsorContainer childContainer);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Resolve<object>(key, arguments) instead.")]
        object Resolve(string key, IDictionary arguments);

        [Obsolete("Use Resolve<object>(key, argumentsAsAnonymousType) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        object Resolve(string key, object argumentsAsAnonymousType);

        object Resolve(string key, Type service);
        object Resolve(Type service);
        object Resolve(Type service, IDictionary arguments);
        object Resolve(Type service, object argumentsAsAnonymousType);
        T Resolve<T>();
        T Resolve<T>(IDictionary arguments);
        T Resolve<T>(object argumentsAsAnonymousType);
        T Resolve<T>(string key);
        T Resolve<T>(string key, IDictionary arguments);
        T Resolve<T>(string key, object argumentsAsAnonymousType);
        object Resolve(string key, Type service, IDictionary arguments);
        object Resolve(string key, Type service, object argumentsAsAnonymousType);
        T[] ResolveAll<T>();
        Array ResolveAll(Type service);
        Array ResolveAll(Type service, IDictionary arguments);
        Array ResolveAll(Type service, object argumentsAsAnonymousType);
        T[] ResolveAll<T>(IDictionary arguments);
        T[] ResolveAll<T>(object argumentsAsAnonymousType);
    }
}
