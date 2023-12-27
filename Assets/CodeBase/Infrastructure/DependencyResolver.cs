using System.Linq;

namespace CodeBase.Infrastructure
{
    public class DependencyResolver
    {
        private const string SingleMethodName = "Single";

        private readonly ServiceLocator _serviceLocator;

        public DependencyResolver(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public T Get<T>()
        {
            var constructor = typeof(T).GetConstructors().FirstOrDefault();

            if (constructor != null)
            {
                var parameters = constructor.GetParameters();

                var resolvedParameters = parameters
                    .Select(parameter => typeof(ServiceLocator).GetMethod(SingleMethodName)
                        ?.MakeGenericMethod(parameter.ParameterType).Invoke(_serviceLocator, null))
                    .ToArray();

                return (T) constructor.Invoke(resolvedParameters);
            }

            return default(T);
        }
    }
}