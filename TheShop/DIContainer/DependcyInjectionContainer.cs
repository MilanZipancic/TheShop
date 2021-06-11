using TheShop.API;
using TheShop.DataAccess;
using TheShop.Logging;
using Unity;

namespace TheShop.DIContainer
{
    public class DependcyInjectionContainer
    {
        private readonly IUnityContainer _container;
        public DependcyInjectionContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<IShopService, ShopService>();
            _container.RegisterType<ILogger, Logger>();
            _container.RegisterType<IDatabaseDriver, DatabaseDriver>();
            RegisterSuppliers();
        }

        public IUnityContainer Container => _container;

        private void RegisterSuppliers()
        {
            _container.RegisterType<ISupplier, Supplier1>("Supplier1");
            _container.RegisterType<ISupplier, Supplier2>("Supplier2");
            _container.RegisterType<ISupplier, Supplier3>("Supplier3");
        }
    }
}
