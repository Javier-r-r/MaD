using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Daos.ProductDao;
using Model.Daos.PropertyDao;
using Model.Services.ProductService;
using Model.Services.ProductService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Test
{
    [TestClass]
    public class ProductServiceTest
    {
        private static IKernel kernel;
        private static IProductService productService;
        private static IProductDao productDao;
        private static IPropertyDao propertyDao;

        private TransactionScope transactionScope;

        private const long categoryId = 1;
        private const long restaurantId = 1;
        private const long productId = 1;
        private const string productName = "Pizza";
        private const int pageNumber = 1;
        private const int pageSize = 10;

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindByProductCategory_ValidId_ReturnsProductDtos()
        {
            using (var scope = new TransactionScope())
            {
                var result = productService.FindByProductCategory(categoryId, pageNumber, pageSize);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindByProductCategory_InvalidId_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                productService.FindByProductCategory(0, pageNumber, pageSize);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindByProductName_ValidName_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                productService.FindByProductName("AnyName");
            }
        }

        [TestMethod]
        public void SearchFilters_ValidInputs_ReturnsProductDtos()
        {
            using (var scope = new TransactionScope())
            {
                var result = productService.SearchFilters(restaurantId, pageNumber, pageSize, categoryId, "burger");
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchFilters_InvalidRestaurantId_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                productService.SearchFilters(0, pageNumber, pageSize, categoryId, "burger");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPropertiesByProductId_InvalidId_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                productService.FindPropertiesByProductId(0, pageNumber, pageSize);
            }
        }

        [TestMethod]
        public void ViewAllProperties_ReturnsList()
        {
            using (var scope = new TransactionScope())
            {
                var result = productService.ViewAllProperties();
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count >= 0);
            }
        }

        #region Test lifecycle methods

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            productService = kernel.Get<IProductService>();
            productDao = kernel.Get<IProductDao>();
            propertyDao = kernel.Get<IPropertyDao>();
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
