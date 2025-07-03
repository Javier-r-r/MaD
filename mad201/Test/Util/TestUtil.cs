using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Daos.BankCardDao;
using Model.Daos.ClientDao;
using Model.Daos.OrderDao;
using Model.Daos.ProductDao;
using Model.Daos.RestaurantDao;
using Model.Daos.UserDao;

namespace Test.Util
{
    class TestUtil
    {
        public static IBankCardDao bankCardDao;
        public static IClientDao clientDao;
        public static IOrderDao orderDao;
        public static IProductDao productDao;
        public static IRestaurantDao restaurantDao;
        public static IUserDao userDao;

        public static Client CreateExistentClient()
        {
            User client;
            try
            {
                client = userDao.FindByLoginName("client")
            }
            catch(Exception)
            {
                client = new Client
                {
                    login = "client",
                    name = "client",
                    password = "1234",
                    address = "A Coruña",
                    email = "client@udc.es",
                    surname = "Pérez",
                    language = "Español",
                    country = "España"
                };

                clientDao.Create(client);
            }
            return client;
        }

        public static Bankcard CreateBankCard()
        {
            Bankcard bankcard = new Bankcard
            {

            }
        }
    }
}
