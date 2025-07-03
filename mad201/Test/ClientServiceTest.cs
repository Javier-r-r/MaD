    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Daos.ClientDao;
    using Model.Services.ClientService;
    using Model.Daos.BankCardDao;
    using Model.Daos.UserDao;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Transactions;
    using Model.Services.UserService.Util;
    using Model.Services.ClientService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model;
using Model.Daos.Util;

namespace Test
{
    [TestClass()]
    public class ClientServiceTest
    {
        private const string login = "clientLogin";
        private const string password = "clientPassword";
        private const string name = "Client Name";
        private const string address = "Client Address";
        private const string email = "client@udc.es";
        private const string surname = "Client Surname";
        private const string language = "en";
        private const string country = "Country";

        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IClientService clientService;
        private static IUserDao userDao;
        private static IClientDao clientDao;
        private static IBankCardDao bankCardDao;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            kernel = TestManager.ConfigureNInjectKernel();  // Usar el kernel configurado
            clientService = kernel.Get<IClientService>();
            userDao = kernel.Get<IUserDao>();
            clientDao = kernel.Get<IClientDao>();
            bankCardDao = kernel.Get<IBankCardDao>();
        }


        [TestMethod]
        public void RegisterClientTest()
        {
            using (var scope = new TransactionScope())
            {
                var clientId =
                    clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                var client = clientDao.Find(clientId);

                Assert.AreEqual(clientId, client.Id);
                Assert.AreEqual(login, client.login);
                Assert.AreEqual(PasswordEncrypter.Crypt(password), client.password);
                Assert.AreEqual(name, client.name);
                Assert.AreEqual(address, client.address);
                Assert.AreEqual(email, client.email);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicateClientTest()
        {
            using (var scope = new TransactionScope())
            {
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);
                long client2Id = clientService.RegisterClient(login, password, name, address, email, surname, language, country);
            }
        }

        [TestMethod]
        public void AddBankCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Registrar un cliente
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar una tarjeta bancaria
                string cardType = "Credit";
                long bankCardNumber = 1234567890123456;
                string owner = "pepe";
                Bankcard newBankCard = clientService.AddBankCard(clientId, cardType, bankCardNumber, owner, 123, DateTime.Now.AddYears(1));

                // Verificar que la tarjeta se ha agregado correctamente
                Assert.AreEqual(bankCardNumber, newBankCard.number);
                Assert.AreEqual(cardType, newBankCard.cardtype);
                Assert.IsTrue(newBankCard.@default);
            }
        }

        [TestMethod]
        public void SetBankCardAsDefaultTest()
        {
            using (var scope = new TransactionScope())
            {
                // Registrar un cliente
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar tarjetas bancarias
                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddYears(1));
                Bankcard newBankCard2 = clientService.AddBankCard(clientId, "Credit", 77777777777777, "ramdom", 777, DateTime.Now.AddYears(1));
                // Establecer la tarjeta como predeterminada
                clientService.SetBankCardAsDefault(newBankCard2.Id);

                // Obtener la tarjeta y verificar que es la predeterminada
                var updatedBankCard = bankCardDao.Find(newBankCard2.Id);
                Assert.IsTrue(updatedBankCard.@default);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void SetNonExistentBankCardAsDefaultTest()
        {
            using (var scope = new TransactionScope())
            {
                // Registrar un cliente
                var clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);


                clientService.SetBankCardAsDefault(9999);
            }
        }

        [TestMethod]
        public void FindBankCardsByClientIdTest()
        {
            using (var scope = new TransactionScope())
            {
                // Registrar un cliente
                var clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar tarjetas bancarias
                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddYears(1));
                Bankcard newBankCard2 = clientService.AddBankCard(clientId, "Credit", 77777777777777, "ramdom", 777, DateTime.Now.AddYears(1));

                // Buscar las tarjetas del cliente
                PagedResult<BankCardDetails> bankCards = clientService.FindBankCardsByClientId(clientId, 1, 5);

                // Verificar que las tarjetas se han encontrado correctamente
                Assert.AreEqual(2, bankCards.TotalItems);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedBankCardException))]
        public void AddDuplicateBankCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Registrar un cliente
                var clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar tarjetas bancarias
                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddYears(1));
                Bankcard newBankCard2 = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddYears(1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCardNumberException))]
        public void AddBankCard_InvalidCardNumber_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar tarjeta con numero no valido
                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 666, "cliente", 123, DateTime.Now.AddYears(1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CardAlreadyExpiredException))]
        public void AddBankCard_ExpiredCard_ThrowsException()
        {
            using (var scope = new TransactionScope())
            {
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                // Agregar tarjeta con caducidad superada
                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddDays(-1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void deleteBankCard()
        {
            using (var scope = new TransactionScope())
            {
                long clientId = clientService.RegisterClient(login, password, name, address, email, surname, language, country);

                Bankcard newBankCard = clientService.AddBankCard(clientId, "Credit", 1234567890123456, "cliente", 123, DateTime.Now.AddDays(1));

                Bankcard addedBankCard = bankCardDao.Find(newBankCard.Id);

                Assert.IsNotNull(addedBankCard);

                clientService.RemoveBankcard(newBankCard.Id);

                Bankcard removedBankCard = bankCardDao.Find(newBankCard.Id);
            }
        }
    }
}
