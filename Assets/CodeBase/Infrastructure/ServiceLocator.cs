using System;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
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

        public void Unregister<TService>() where TService : IService
        {
            if (Implementation<TService>.ServiceInstance == null)
            {
                throw new Exception("You try delete unregister service");
            }
            
            Implementation<TService>.ServiceInstance = default(TService);
        }

        public static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}