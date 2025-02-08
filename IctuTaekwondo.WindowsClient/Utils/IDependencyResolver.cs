using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IctuTaekwondo.WindowsClient.Utils
{
    public interface IDependencyResolver
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        TService Resolve<TService>();
    }

    public class DependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, Func<object>> _transientServices = new Dictionary<Type, Func<object>>();
        private readonly Dictionary<Type, object> _singletonServices = new Dictionary<Type, object>();

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            _transientServices[typeof(TService)] = () => Activator.CreateInstance(typeof(TImplementation));
        }

        public void Register<TService, TImplementation>(Func<IDependencyResolver, TImplementation> factory) where TImplementation : TService
        {
            _transientServices[typeof(TService)] = () => factory(this);
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            var implementationInstance = Activator.CreateInstance(typeof(TImplementation));
            _singletonServices[typeof(TService)] = implementationInstance;
        }

        public TService Resolve<TService>()
        {
            if (_singletonServices.ContainsKey(typeof(TService)))
            {
                return (TService)_singletonServices[typeof(TService)];
            }

            if (_transientServices.ContainsKey(typeof(TService)))
            {
                return (TService)_transientServices[typeof(TService)]();
            }

            throw new Exception($"Service of type {typeof(TService)} is not registered.");
        }
    }
}
