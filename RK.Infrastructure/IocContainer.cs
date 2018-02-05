using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Infrastructure
{
    public class IocContainer
    {

        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static IContainer Container
        {
            get { return _container; }
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string serviceName)
        {
            return _container.ResolveNamed<T>(serviceName);
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static object Resolve(Type type, string serviceName)
        {
            return _container.ResolveNamed(serviceName, type);
        }
    }
}
