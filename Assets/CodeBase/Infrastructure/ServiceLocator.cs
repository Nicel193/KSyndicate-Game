using System;
using System.Runtime.CompilerServices;

namespace CodeBase.Infrastructure.States
{
    public class ServiceLocator
    {
        public static ServiceLocator Contener => _instance ?? (_instance = new ServiceLocator());

        private static ServiceLocator _instance;

        public void RegisterSingle<TService>(TService service) where TService : IService
            => Implementation<TService>.ServiceInstance = service;

        public TService Single<TService>() where TService : IService
        {
            TService service = Implementation<TService>.ServiceInstance;

            if (service == null)
            {
                throw new Exception("You try get unregister service");
            }

            return service;
        }

        public static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}