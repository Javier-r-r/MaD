using Es.Udc.DotNet.ModelUtil.IoC;
using Model.Daos.BankCardDao;
using Model.Daos.CategoryDao;
using Model.Daos.ClientDao;
using Model.Daos.ProductDao;
using Model.Daos.ProductPorpertyDao;
using Model.Daos.PropertyDao;
using Model.Daos.RestaurantDao;
using Model.Daos.UserDao;
using Model.Services.ClientService;
using Model.Services.RestaurantService;
using Model.Services.UserService;
using Model.Daos.CartDao;
using Model.Daos.OrderDao;
using Model.Services.CartService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserDao>().
                To<UserDaoImpl>();

            /* ClientDao */
            kernel.Bind<IClientDao>().
                To<ClientDaoImpl>();

            /* RestaurantDao */
            kernel.Bind<IRestaurantDao>().
                To<RestaurantDaoImpl>();

            /* BankCardDao */
            kernel.Bind<IBankCardDao>().
                To<BankCardDaoImpl>();

            /* ProductDao */
            kernel.Bind<IProductDao>().
                To<ProductDaoImpl>();

            /* CategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoImpl>();

            /* PropertyDao */
            kernel.Bind<IPropertyDao>().
                To<PropertyDaoImpl>();

            /* ProductPropertyDao */
            kernel.Bind<IProductPropertyDao>().
                To<ProductPropertyDaoImpl>();

            /* CartDao */
            kernel.Bind<ICartDao>().
                To<CartDaoImpl>();

            /* OrderDao */
            kernel.Bind<IOrderDao>().
                To<OrderDaoImpl>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* ClientService */
            kernel.Bind<IClientService>().
                To<ClientService>();

            /* RestaurantService */
            kernel.Bind<IRestaurantService>().
                To<RestaurantService>();

            /* CartService */
            kernel.Bind<ICartService>().
                To<CartService>();




            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["Mad201Entities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}