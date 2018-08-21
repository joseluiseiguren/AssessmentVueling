using AssessmentVueling.Interfaces;
using AssessmentVueling.Manager;
using System;
using System.Linq;
using Unity;
using Unity.Lifetime;

namespace AssessmentVueling.Configuration
{
    public class DependencyResolver
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Register all the dependecies
        /// </summary>
        public static void RegisterDependencies()
        {
            if (_container == null)
            {
                _container = new UnityContainer();
            }
            else
            {
                _container.Registrations.ToList().Clear();
            }

            _container.RegisterType<ITransactionRepository, Repository.WebService.TransactionRepository>(new ContainerControlledLifetimeManager()); //as singleton
            _container.RegisterType<ITransactionRepository, Repository.File.TransactionRepository>("backup", new ContainerControlledLifetimeManager()); //as singleton
            _container.RegisterType<ITransactionManager, TransactionManager>(new ContainerControlledLifetimeManager()); //as singleton

            _container.RegisterType<IRateRepository, Repository.WebService.RateRepository>(new ContainerControlledLifetimeManager()); //as singleton
            _container.RegisterType<IRateRepository, Repository.File.RateRepository>("backup", new ContainerControlledLifetimeManager()); //as singleton
            _container.RegisterType<IRateManager, RateManager>(new ContainerControlledLifetimeManager()); //as singleton
            
            _container.RegisterType<IAppLogger, NLogManager>(new ContainerControlledLifetimeManager()); //as singleton            
        }

        public static T Resolve<T>(string name = "")
        {
            if (_container.IsRegistered(typeof(T), name))
            {
                return _container.Resolve<T>(name);
            }
            else
            {
                throw new NotSupportedException("Could not resolve dependency for: " + typeof(T).Name);
            }
        }
    }
}