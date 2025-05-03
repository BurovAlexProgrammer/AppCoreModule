using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> _services = new();

        public static void Bind<T>(IService service)
        {
            Bind(typeof(T), service);
        }

        public static void Bind(Type type, IService service)
        {
            if (_services.TryAdd(type, service)) return;
            
            Debug.LogError($"ServiceLocator: service [{type.Name}] already bound. Overrided.");
            _services[type] = service;
        }

        public static void Clean()
        {
            foreach (var service in _services.Values)
            {
                if (service is IDisposable disposableService)
                {
                    disposableService.Dispose();
                } 
            }

            Debug.Log("ServiceLocator cleaned.");
        }
    }
}