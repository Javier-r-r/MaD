using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Model.Daos.PropertyDao;
using Model.Services.ProductService;
using Model.Services.UserService;
using Model.Services.ClientService;
using Model.Daos.ClientDao;
using Model.Daos.CartDao;
using Model.Daos.BankCardDao;
using Model.Daos.OrderDao;
using Model.Services.CartService;
using Ninject;
using System.Configuration;
using System.Data.Entity;
using Model.Services.RestaurantService;
using Model.Daos.RestaurantDao;
using Model.Daos.CategoryDao;
using Model.Services.CommentService;
using Model.Daos.CommentDao;
using Model.Daos.ProductPorpertyDao;

namespace Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region Option A : configuration via sourcecode

            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IUserDao>().To<UserDaoImpl>();

            kernel.Bind<IProductService>().To<ProductService>();

            kernel.Bind<IProductDao>().To<ProductDaoImpl>();

            kernel.Bind<IPropertyDao>().To<PropertyDaoImpl>();

            kernel.Bind<IProductPropertyDao>().To<ProductPropertyDaoImpl>();

            kernel.Bind<IClientService>().To<ClientService>();

            kernel.Bind<IClientDao>().To<ClientDaoImpl>();

            kernel.Bind<IBankCardDao>().To<BankCardDaoImpl>();

            kernel.Bind<IRestaurantService>().To<RestaurantService>();

            kernel.Bind<IRestaurantDao>().To<RestaurantDaoImpl>();

            kernel.Bind<ICategoryDao>().To<CategoryDaoImpl>();

            kernel.Bind<ICommentService>().To<CommentService>();

            kernel.Bind<ICommentDao>().To<CommentDaoImpl>();

            kernel.Bind<ICartService>().To<CartService>();

            kernel.Bind<ICartDao>().To<CartDaoImpl>();

            kernel.Bind<IOrderDao>().To<OrderDaoImpl>();


            string connectionString =
                ConfigurationManager.ConnectionStrings["Mad201Entities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion Option A : configuration via sourcecode

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
