using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.BankCardDao;
using Model.Daos.CartDao;
using Model.Daos.CategoryDao;
using Model.Daos.ClientDao;
using Model.Daos.OrderDao;
using Model.Daos.ProductDao;
using Model.Daos.RestaurantDao;
using Model.Services.CartService;
using Model.Services.CartService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Test
{
    [TestClass()]
    public class CartServiceTest
    {
        private static IKernel kernel;
        private static ICartService cartService;
        private static IClientDao clientDao;
        private static IBankCardDao bankCardDao;
        private static IOrderDao orderDao;
        private static ICartDao cartDao;
        private static IProductDao productDao;
        private static IRestaurantDao restaurantDao;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }


        private const string clientLogin = "clientLogin";
        private const string clientPassword = "clientPassword";
        private const string clientName = "Client Name";
        private const string clientAddress = "Client Address";
        private const string clientEmail = "client@udc.es";
        private const string clientSurname = "Client Surname";
        private const string clientLanguage = "en";
        private const string clientCountry = "Country";

        private const string login = "login";
        private const string password = "password";
        private const string name = "restaurant";
        private const string address = "restaurant-address";
        private const string email = "restaurant@gmail.es";
        private const string schedule = "schedule";
        private const string restaurantType = "type";
        private const string language = "language";
        private const string country = "country";

        private const long NON_EXISTENT_ID = -999;


        [TestMethod]
        public void AddOrderCreatesOrderAndCartlinesTest()
        {
            using (var scope = new TransactionScope())
            {
                // Arrange: Crear cliente y tarjeta
                var client = new Client
                {
                    login = clientLogin,
                    name = clientName,
                    address = clientAddress,
                    email = clientEmail,
                    password = clientPassword,
                    surname = clientSurname,
                    language = clientLanguage,
                    country = clientCountry
                };
                clientDao.Create(client);

                Bankcard card = new Bankcard
                {
                    number = 1234567787856431,
                    name = name,
                    cardtype = "Debit",
                    cvv = 111,
                    expirationdate = DateTime.Now.AddHours(1),
                    Client = client,
                    @default = false,
                };

                bankCardDao.Create(card);

                var restaurant = new Restaurant
                {
                    login = login,
                    password = password,
                    name = name,
                    address = address,
                    email = email,
                    schedule = schedule,
                    type = restaurantType,
                    language = language,
                    country = country
                };
                restaurantDao.Create(restaurant);

                var category = new Category
                {
                    categoryName = "Comida"
                };
                categoryDao.Create(category);

                // Crear producto
                var product = new Product
                {
                    name = "Pizza",
                    price = 10.5,
                    stock = 10,
                    creationDate = DateTime.Now,
                    Category = category,
                    Restaurant = restaurant
                };
                productDao.Create(product);

                var cartLines = new List<CartLineDto>
                {
                    new CartLineDto(product.name, product.price)
                };

                // Act
                var orderId = cartService.AddOrder(client.Id, "Fake Address", card.Id, cartLines);
                var order = orderDao.Find(orderId);

                // Assert
                Assert.IsNotNull(order);
                Assert.AreEqual(client.Id, order.Client.Id);
                Assert.AreEqual(card.number, order.bankcardNumber);
                Assert.AreEqual("Fake Address", order.orderAddress);

                var cartlineList = cartDao.FindByOrderId(orderId);
                Assert.AreEqual(1, cartlineList.Count);
                Assert.AreEqual(cartLines[0].price, cartlineList[0].price, 0.001);
                Assert.AreEqual(cartLines[0].productName, cartlineList[0].productName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddOrderThrowsIfClientNotFoundTest()
        {

            Client client = new Client
            {
                login = clientLogin,
                name = clientName,
                address = clientAddress,
                email = clientEmail,
                password = clientPassword,
                surname = clientSurname,
                language = clientLanguage,
                country = clientCountry
            };
            clientDao.Create(client);

            Bankcard card = new Bankcard
            {
                number = 1234567787856431,
                name = name,
                cardtype = "Debit",
                cvv = 111,
                expirationdate = DateTime.Now.AddHours(1),
                Client = client,
                @default = false,
            };
            bankCardDao.Create(card);

            List<CartLineDto> cartLines = new List<CartLineDto>
            {
                new CartLineDto("Product", 10.0)
            };

            cartService.AddOrder(NON_EXISTENT_ID, "Fake Address", card.Id, cartLines);
        }


        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddOrderThrowsIfCardNotFoundTest()
        {
            Client client = new Client
            {
                login = clientLogin,
                name = clientName,
                address = clientAddress,
                email = clientEmail,
                password = clientPassword,
                surname = clientSurname,
                language = clientLanguage,
                country = clientCountry
            };
            clientDao.Create(client);

            var cartLines = new List<CartLineDto>
            {
                new CartLineDto("Product", 10.0)
            };

            cartService.AddOrder(client.Id, "Fake Address", NON_EXISTENT_ID, cartLines);
        }


        [TestMethod]
        public void AddOrderStoresMultipleCartLinesTest()
        {
            Client client = new Client
            {
                login = clientLogin,
                name = clientName,
                address = clientAddress,
                email = clientEmail,
                password = clientPassword,
                surname = clientSurname,
                language = clientLanguage,
                country = clientCountry
            };
            clientDao.Create(client);

            Bankcard card = new Bankcard
            {
                number = 1234567787856431,
                name = name,
                cardtype = "Debit",
                cvv = 111,
                expirationdate = DateTime.Now.AddHours(1),
                Client = client,
                @default = false,
            };
            bankCardDao.Create(card);

            Restaurant restaurant = new Restaurant
            {
                login = login,
                password = password,
                name = name,
                address = address,
                email = email,
                schedule = schedule,
                type = restaurantType,
                language = language,
                country = country
            };
            restaurantDao.Create(restaurant);

            Category category = new Category
            {
                categoryName = "Comida"
            };
            categoryDao.Create(category);

            Product product1 = new Product
            {
                name = "Pizza",
                price = 10.5,
                stock = 10,
                creationDate = DateTime.Now,
                Category = category,
                Restaurant = restaurant
            };
            productDao.Create(product1);

            Product product2 = new Product
            {
                name = "Pasta",
                price = 8.0,
                stock = 5,
                creationDate = DateTime.Now,
                Category = category,
                Restaurant = restaurant
            };
            productDao.Create(product2);

            var cartLines = new List<CartLineDto>
            {
                new CartLineDto(product1.name, product1.price),
                new CartLineDto(product2.name, product2.price)
            };

            long orderId = cartService.AddOrder(client.Id, "Fake Address", card.Id, cartLines);

            var orderList = cartService.ViewClientOrders(client.Id, 1, 10);
            Assert.AreEqual(1, orderList.Items.Count);
            Assert.AreEqual(2, orderList.Items[0].Cartline.Count);
        }


        #region Test lifecycle methods

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            cartService = kernel.Get<ICartService>();
            clientDao = kernel.Get<IClientDao>();
            restaurantDao = kernel.Get<IRestaurantDao>();
            bankCardDao = kernel.Get<IBankCardDao>();
            orderDao = kernel.Get<IOrderDao>();
            cartDao = kernel.Get<ICartDao>();
            productDao = kernel.Get<IProductDao>();
            categoryDao = kernel.Get<ICategoryDao>();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion
    }
}