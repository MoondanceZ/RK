using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Infrastructure
{
    public static class IocContainer
    {
        public static void SetContainer(IContainer container)
        {
            Container = container;
        }

        public static IContainer Container { get; private set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(string serviceName)
        {
            return Container.ResolveNamed<T>(serviceName);
        }

        public static object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public static object Resolve(Type type, string serviceName)
        {
            return Container.ResolveNamed(serviceName, type);
        }
    }
}
