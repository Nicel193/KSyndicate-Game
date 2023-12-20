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
            => Implementation<TService>.ServiceInstance;

        public static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}