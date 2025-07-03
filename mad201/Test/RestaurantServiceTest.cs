using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.CategoryDao;
using Model.Daos.ProductDao;
using Model.Daos.ProductPorpertyDao;
using Model.Daos.RestaurantDao;
using Model.Daos.Util;
using Model.Services.RestaurantService;
using Model.Services.RestaurantService.Exceptions;
using Model.Services.UserService.Util;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Test
{
    [TestClass]
    public class RestaurantServiceTest
    {

        private static IKernel kernel;
        private static IRestaurantService restaurantService;
        private static IRestaurantDao restaurantDao;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static IProductPropertyDao productPropertyDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        private const string login = "login";
        private const string password = "password";
        private const string name = "restaurant";
        private const string address = "restaurant-address";
        private const string email = "restaurant@gmail.es"; 
        private const string schedule = "schedule";
        private const string restaurantType = "type";
        private const string language = "language";
        private const string country = "country";

        private const string category1 = "Category1";

        [TestMethod]
        public void RegisterRestaurantTest()
        {
            using (var scope = new TransactionScope())
            {
                var restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

                Restaurant restaurant = restaurantDao.FindByRestaurantName(name);

                Assert.AreEqual(restaurantId, restaurant.Id);
                Assert.AreEqual(login, restaurant.login);
                Assert.AreEqual(PasswordEncrypter.Crypt(password), restaurant.password);
                Assert.AreEqual(name, restaurant.name);
                Assert.AreEqual(address, restaurant.address);
                Assert.AreEqual(email, restaurant.email);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void RegisterDuplicateRestaurantNameTest()
        {
            using (var scope = new TransactionScope())
            {
                restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);
                restaurantService.RegisterRestaurant("other", password, name, address, email, schedule, restaurantType, language, country);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicateRestaurantTest()
        {
            using (var scope = new TransactionScope())
            {
                restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);
                restaurantService.RegisterRestaurant(login, password, "other", address, email, schedule, restaurantType, language, country);

            }
        }

        [TestMethod]
        public void ListRestaurantsTest()
        {
            using (var scope = new TransactionScope())
            {
                PagedResult<Restaurant> restaurantsInBBDD = restaurantService.listRestaurants(1, 10);
                restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);
                restaurantService.RegisterRestaurant("other", password, "other", address, "other", schedule, restaurantType, language, country);
                restaurantService.RegisterRestaurant("other2", password, "other2", address, "other2", schedule, restaurantType, language, country);

                PagedResult<Restaurant> restaurants = restaurantService.listRestaurants(1, 10);

                Assert.AreEqual(3, restaurants.TotalItems - restaurantsInBBDD.TotalItems);
            }
        }

        [TestMethod]
        public void ListRestaurantsFilterByTypeTest()
        {
            using (var scope = new TransactionScope())
            {

                restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, "test", language, country);
                restaurantService.RegisterRestaurant("other", password, "other", address, email, schedule, "test", language, country);

                PagedResult<Restaurant> restaurants = restaurantService.listRestaurantsFilterByType("test", 1, 10);

                Assert.AreEqual(2, restaurants.Items.Count);

            }
        }


        [TestMethod]
        public void getRestaurantTypesTest()
        {
            using (var scope = new TransactionScope())
            {
                List<string> restaurantTypesInBBDD = restaurantService.getRestaurantTypes();
                restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, "testType1", language, country);
                restaurantService.RegisterRestaurant("other", password, "other", address, email, schedule, "testType2", language, country);
                restaurantService.RegisterRestaurant("other2", password, "other2", address, schedule, "testType3", restaurantType, language, country);

                List<string> restaurantTypesInTest = restaurantService.getRestaurantTypes();

                Assert.AreEqual(3, restaurantTypesInTest.Count - restaurantTypesInBBDD.Count);


            }
        }


        [TestMethod]
        public void CreateProductCategory()
        {
            using (var scope = new TransactionScope())
            {

                restaurantService.AddProductCategory(category1);

                Category savedCategory = categoryDao.FindByCategoryName(category1);

                Assert.AreEqual(savedCategory.categoryName, category1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryAlreadyExistsException))]
        public void CreateDuplicatedProductCategory()
        {
            using (var scope = new TransactionScope())
            {
                restaurantService.AddProductCategory(category1);
                restaurantService.AddProductCategory(category1);
            }
        }

        [TestMethod]
        public void AddProductTest()
        {
            using (var scope = new TransactionScope())
            {
                var restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

                Restaurant restaurant = restaurantDao.Find(restaurantId);

                string productName = "Pizza test";
                double price = 9.99;
                DateTime creationDate = DateTime.Now;
                int stock = 50;
                string category = "Category1";
                List<ProductProperty> properties = new List<ProductProperty>();

                long productId = restaurantService.AddProduct(restaurantId, productName, price, creationDate, stock, category, properties);

                Product product = productDao.Find(productId);

                Assert.IsNotNull(product);
                Assert.AreEqual(productName, product.name);
                Assert.AreEqual(price, product.price);
                Assert.AreEqual(creationDate.ToString(), product.creationDate.ToString());
                Assert.AreEqual(stock, product.stock);
                Assert.AreEqual(restaurantId, product.Restaurant.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddProductInNonExistentRestaurantTest()
        {
            using (var scope = new TransactionScope())
            {
                string productName = "Pizza Margarita";
                double price = 9.99;
                DateTime creationDate = DateTime.Now;
                int stock = 50;
                string category = "Category1";
                List<ProductProperty> properties = new List<ProductProperty>();
                restaurantService.AddProduct(999, productName, price, creationDate, stock, category, properties);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ProductAlreadyExistsException))]
        public void AddDuplicatedProductTest()
        {
            using (var scope = new TransactionScope())
            {
                var restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

                Restaurant restaurant = restaurantDao.Find(restaurantId);

                string productName = "Pizza test";
                double price = 9.99;
                DateTime creationDate = DateTime.Now;
                int stock = 50;
                string category = "Category1";
                List<ProductProperty> properties = new List<ProductProperty>();
                restaurantService.AddProduct(restaurantId, productName, price, creationDate, stock, category, properties);
                restaurantService.AddProduct(restaurantId, productName, price, creationDate, stock, category, properties);

            }
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            using (var scope = new TransactionScope())
            {
                var restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

                Restaurant restaurant = restaurantDao.Find(restaurantId);

                string productName = "Pizza test";
                double price = 9.99;
                DateTime creationDate = DateTime.Now;
                int stock = 50;
                string category = "Category1";
                List<ProductProperty> properties = new List<ProductProperty>();
                long productId = restaurantService.AddProduct(restaurantId, productName, price, creationDate, stock, category, properties);

                Product product = productDao.Find(productId);

                product.name = "Pizza2";
                product.price = 15.00;
                product.stock = 100;

                productDao.Update(product);

                Assert.IsNotNull(product);
                Assert.AreEqual(product.name, "Pizza2");
                Assert.AreEqual(product.price, 15.00);
                Assert.AreEqual(product.stock, 100);
            }
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            using (var scope = new TransactionScope())
            {
                var restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

                Restaurant restaurant = restaurantDao.Find(restaurantId);

                string productName = "Pizza test";
                double price = 9.99;
                DateTime creationDate = DateTime.Now;
                int stock = 50;
                string category = "Category1";
                List<ProductProperty> properties = new List<ProductProperty>();
                long productId = restaurantService.AddProduct(restaurantId, productName, price, creationDate, stock, category, properties);

                productDao.Remove(productId);

                var existsProduct = productDao.Exists(productId);

                Assert.IsFalse(existsProduct);
            }
        }


        [TestMethod]
        public void FindAllCategoriesTest()
        {
            using (var scope = new TransactionScope())
            {
                List<Category> categoriesInBBDD = restaurantService.FindAllCategories();
                restaurantService.AddProductCategory("testCategory");
                List<Category> categories = restaurantService.FindAllCategories();

                Assert.IsNotNull(categories);
                Assert.AreEqual(1, categories.Count - categoriesInBBDD.Count);
            }
        }

        [TestMethod]
        public void FindAllSpecificPropertiesTest()
        {
            using (var scope = new TransactionScope())
            {
                List<Property> propertiesInBBDD = restaurantService.FindAllSpecificProperties();

                Assert.IsNotNull(propertiesInBBDD);
                Assert.IsTrue(propertiesInBBDD.Count >= 0);
            }
        }


        [TestMethod]
        public void UpdateRestaurantProfileTest()
        {
            using (var scope = new TransactionScope())
            {
                long restaurantId = restaurantService.RegisterRestaurant(login, password, name, address, email, schedule, restaurantType, language, country);

               
                restaurantService.UpdateRestaurantProfile( restaurantId, "NewName", "new@email.com", "10-18", "typeB", "en", "US");

                Restaurant updatedRestaurant = restaurantService.FindRestaurantById(restaurantId);

                Assert.AreEqual("NewName", updatedRestaurant.name);
                Assert.AreEqual("new@email.com", updatedRestaurant.email);
                Assert.AreEqual("10-18", updatedRestaurant.schedule);
                Assert.AreEqual("typeB", updatedRestaurant.type);
                Assert.AreEqual("en", updatedRestaurant.language);
                Assert.AreEqual("US", updatedRestaurant.country);
            }
        }



        #region Test lifecycle methods

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            restaurantService = kernel.Get<IRestaurantService>();
            restaurantDao = kernel.Get<IRestaurantDao>();
            productDao = kernel.Get<IProductDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            productPropertyDao = kernel.Get<IProductPropertyDao>();
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
