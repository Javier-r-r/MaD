using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Daos.UserDao;
using Model.Services.UserService;
using Model.Services.UserService.DTOs;
using Model.Services.UserService.Util;
using Ninject;
using System;
using System.Transactions;

namespace Test
{
    [TestClass()]
    public class UserServiceTest
    {
        private const string login = "login";
        private const string password = "password";
        private const string name = "name";
        private const string address = "address";
        private const string email = "user@udc.es";
        private const string country = "Spain";
        private const string language = "es-Es";

        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserDao userDao;



        //Due to the limited precision of floating point numbers, the equality
        //operator may provide unexpected results if two numbers are close to
        //each other (e.g. 25 and 25.00000000001). In order to solve this
        //issue, a small margin of error (delta) can be allowed.
        private const double delta = 0.00001;



        public TestContext TestContext { get; set; }


        [TestMethod]
        public void RegisterUserTest()
        {

            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.RegisterUser(login, password,
                        new UserSummaryDto(login, name, address, email, language, country));

                var user = userDao.Find(userId);

                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(login, user.login);
                Assert.AreEqual(PasswordEncrypter.Crypt(password), user.password);
                Assert.AreEqual(name, user.name);
                Assert.AreEqual(address, user.address);
                Assert.AreEqual(email, user.email);
                Assert.AreEqual(language, user.language);
                Assert.AreEqual(country, user.country);


            }
        }


        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(login, password,
                    new UserSummaryDto(login, name, address, email, language, country));

                var expected = new UserDto(userId, login,
                    PasswordEncrypter.Crypt(password), name, address, email, language, country);

                var actual =
                    userService.Login(login,
                        password, false);

                Assert.AreEqual(expected, actual);

            }
        }


        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(login, password,
                    new UserSummaryDto(login, name, address, email, language, country));

                var expected = new UserDto(userId, login,
                    PasswordEncrypter.Crypt(password), name, address, email, language, country);

                var obtained =
                    userService.Login(login,
                        PasswordEncrypter.Crypt(password), true);

                Assert.AreEqual(expected, obtained);

            }
        }


        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var expected =
                    new UserSummaryDto(login, name, address, email, language, country);

                var userId =
                    userService.RegisterUser(login, password, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                Assert.AreEqual(expected, obtained);

            }
        }

        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(login, password,
                    new UserSummaryDto(login, name, address, email, language, country));

                var expected =
                    new UserSummaryDto(login + "X", name + "X",
                        address + "X", "XX", language, country);

                userService.UpdateUserProfileDetails(userId, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                Assert.AreEqual(expected, obtained);

            }
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(login, password,
                    new UserSummaryDto(login, name, address, email, language, country));

                var newClearPassword = password + "X";
                userService.ChangePassword(userId, password, newClearPassword);

                userService.Login(login, newClearPassword, false);

            }
        }

        [TestMethod]
        public void UserExistsForValidUser()
        {
            using (var scope = new TransactionScope())
            {
                userService.RegisterUser(login, password,
                    new UserSummaryDto(login, name, address, email, language, country));

                bool userExists = userService.UserExists(login);

                Assert.IsTrue(userExists);

            }
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes
    }
}
