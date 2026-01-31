using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Infrastructure
{
    public sealed class ServiceContainer
    {
        private readonly Dictionary<Type, object> services = new();

        public void Register<T>(T instance) where T : class
        {
            services[typeof(T)] = instance;
        }

        public T Resolve<T>() where T : class
        {
            return services.TryGetValue(typeof(T), out var obj) ? (T)obj : null;
        }
    }
}

