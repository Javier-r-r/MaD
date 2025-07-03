using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ClientDao;
using Model.Daos.UserDao;
using Model.Daos.BankCardDao;
using Model.Daos.OrderDao;
using Model.Services.UserService.Util;
using Ninject;
using Model.Services.ClientService.Exceptions;
using Model.Daos.Util;

namespace Model.Services.ClientService
{
    /// <summary>
    /// Servicio de gestión de clientes.  
    /// Proporciona métodos para registrar clientes, administrar tarjetas bancarias  
    /// y recuperar información asociada a los clientes.
    /// </summary>
    public class ClientService : IClientService
    {
        /// <summary>
        /// DAO para la gestión de clientes.
        /// </summary>
        [Inject]
        public IClientDao ClientDao { private get; set; }

        /// <summary>
        /// DAO para la gestión de usuarios.
        /// </summary>
        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <summary>
        /// DAO para la gestión de tarjetas bancarias.
        /// </summary>
        [Inject]
        public IBankCardDao BankCardDao { private get; set; }

        #region IClientService Members

        /// <summary>
        /// Registra un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="loginName">Nombre de usuario.</param>
        /// <param name="clearPassword">Contraseña sin cifrar.</param>
        /// <param name="name">Nombre del cliente.</param>
        /// <param name="address">Dirección del cliente.</param>
        /// <param name="email">Correo electrónico.</param>
        /// <param name="surname">Apellido del cliente.</param>
        /// <param name="language">Idioma preferido.</param>
        /// <param name="country">País de residencia.</param>
        /// <returns>El ID del cliente registrado.</returns>
        /// <exception cref="DuplicateInstanceException">
        /// Se lanza si ya existe un cliente con el mismo nombre de usuario.
        /// </exception>
        [Transactional]
        public long RegisterClient(string loginName, string clearPassword, string name,
            string address, string email, string surname, string language, string country)
        {
            try
            {
                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName, typeof(Client).FullName);
            }
            catch (InstanceNotFoundException)
            {
                string encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                Client client = new Client
                {
                    login = loginName,
                    password = encryptedPassword,
                    name = name,
                    address = address,
                    email = email,
                    surname = surname,
                    language = language,
                    country = country
                };

                ClientDao.Create(client);
                return client.Id;
            }
        }

        /// <summary>
        /// Agrega una nueva tarjeta bancaria a la cuenta de un cliente.
        /// </summary>
        /// <param name="ownerLogin">Nombre de usuario del propietario.</param>
        /// <param name="creditType">Tipo de tarjeta (Visa, MasterCard, etc.).</param>
        /// <param name="bankCardNumber">Número de la tarjeta bancaria.</param>
        /// <param name="cvv">Código CVV de la tarjeta.</param>
        /// <param name="expirationDate">Fecha de expiración de la tarjeta.</param>
        /// <returns>La tarjeta bancaria agregada.</returns>
        /// <exception cref="DuplicatedBankCardException">
        /// Se lanza si la tarjeta bancaria ya existe para el cliente.
        /// </exception>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si el usuario no existe.
        /// </exception>
        /// <exception cref="InvalidCardNumberException">
        /// Se lanza si el número de la tarjeta no es válido.
        /// </exception>
        [Transactional]
        public Bankcard AddBankCard(long clientId, string creditType,
            long bankCardNumber, string name, int cvv, DateTime expirationDate)
        {

            string numberString = bankCardNumber.ToString();
            if (numberString.Length < 13 || numberString.Length > 19)
            {
                throw new InvalidCardNumberException(bankCardNumber);
            }

            if (expirationDate < DateTime.Now.Date)
            {
                throw new CardAlreadyExpiredException(expirationDate);
            }

            Client client = ClientDao.Find(clientId);
            List<Bankcard> bankCards = BankCardDao.FindAllByClientId(clientId);

            foreach (Bankcard bankCard in bankCards)
            {
                if (bankCard.number == bankCardNumber)
                {
                    throw new DuplicatedBankCardException(bankCardNumber);
                }
            }

            Bankcard newBankCard = new Bankcard
            {
                number = bankCardNumber,
                name = name,
                cardtype = creditType,
                cvv = cvv,
                expirationdate = expirationDate,
                Client = client,
                @default = false,
            };

            BankCardDao.Create(newBankCard);

            if (!bankCards.Any())
            {
                newBankCard.@default = true;
                BankCardDao.Update(newBankCard);
            }

            return newBankCard;
        }

        [Transactional]
        public void RemoveBankcard(long bankcardId)
        {
            Bankcard bankcard = BankCardDao.Find(bankcardId);
            long clientId = bankcard.Client.Id;

            BankCardDao.Remove(bankcardId);

            if (bankcard.@default)
            {
                var remainingCards = BankCardDao.FindAllByClientId(clientId);
                if (remainingCards.Any())
                {
                    Bankcard newDef = remainingCards.First();
                    newDef.@default = true;
                    BankCardDao.Update(newDef);
                }
            }
        }

        /// <summary>
        /// Establece una tarjeta bancaria como la tarjeta predeterminada del usuario.
        /// </summary>
        /// <param name="bankCardId">ID de la tarjeta bancaria.</param>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si la tarjeta bancaria no se encuentra.
        /// </exception>
        public void SetBankCardAsDefault(long bankcardId)
        {
            Bankcard newDefCard = BankCardDao.Find(bankcardId);
            List<Bankcard> bankcards = BankCardDao.FindAllByClientId(newDefCard.Client.Id);

            foreach(Bankcard bankcard in bankcards)
            {
                if (bankcard.@default) 
                { 
                    bankcard.@default = false;
                    BankCardDao.Update(bankcard);
                }
            }

            newDefCard.@default = true;
            BankCardDao.Update(newDefCard);
        }

        public Bankcard FindBankcardById(long bankcardId)
        {
            return BankCardDao.Find(bankcardId);
        }

        [Transactional]
        public void UpdateBankcard(long bankcardId, string creditType,
            long bankCardNumber, string name, int cvv, DateTime expirationDate)
        {
            Bankcard bankcard = BankCardDao.Find(bankcardId);

            if(bankcard == null)
                throw new InstanceNotFoundException("Bankcard not found", bankcardId.ToString());

            bankcard.cardtype = creditType;
            bankcard.number = bankCardNumber;
            bankcard.name = name;
            bankcard.cvv = cvv;
            bankcard.expirationdate = expirationDate;

            BankCardDao.Update(bankcard);
        }

        /// <summary>
        /// Busca las tarjetas bancarias de un cliente.
        /// </summary>
        /// <param name="login">Nombre de usuario del cliente.</param>
        /// <returns>Lista de tarjetas bancarias asociadas al cliente.</returns>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si no se encuentran tarjetas bancarias para el usuario.
        /// </exception>
        public PagedResult<BankCardDetails> FindBankCardsByClientId(long clientId, int pageNumber, int pageSize)
        {
            PagedResult<Bankcard> pagedResult = BankCardDao.FindByOwner(clientId, pageNumber, pageSize);
            return BankCardDetails.fromBankCardToBankCardDetails(pagedResult);
        }

        public List<BankCardDetails> FindAllBankCardsByClientId(long clientId)
        {
            return BankCardDetails.fromBankCardToBankCardDetailsList(BankCardDao.FindAllByClientId(clientId));
        }

        public Client FindClientById(long clientId)
        {
            return ClientDao.Find(clientId);
        }

        [Transactional]
        public void UpdateClientProfile(long userId, string name, string surname, string email, string language, string country)
        {
            
            Client client = ClientDao.Find(userId); // Usa el ID para recuperar

            if (client == null)
            {
                throw new InstanceNotFoundException("Client not found", userId.ToString());
            }

            // Actualiza los campos
            client.name = name;
            client.surname = surname;
            client.email = email;
            client.language = language;
            client.country = country;

            ClientDao.Update(client);
        }

        #endregion
    }
}
