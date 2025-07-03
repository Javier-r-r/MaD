using System;
using System.Collections.Generic;
using Model.Daos.UserDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ClientDao;
using Model.Daos.BankCardDao;
using Model.Daos.Util;

namespace Model.Services.ClientService
{
    /// <summary>
    /// Interfaz que define los servicios relacionados con la gestión de clientes.  
    /// Permite registrar clientes, gestionar tarjetas bancarias y recuperar información de clientes.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// DAO para la gestión de usuarios.
        /// </summary>
        IUserDao UserDao { set; }

        /// <summary>
        /// DAO para la gestión de clientes.
        /// </summary>
        IClientDao ClientDao { set; }

        /// <summary>
        /// DAO para la gestión de tarjetas bancarias.
        /// </summary>
        IBankCardDao BankCardDao { set; }

        /// <summary>
        /// Registra un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="loginName">Nombre de usuario del cliente.</param>
        /// <param name="clearPassword">Contraseña sin cifrar.</param>
        /// <param name="name">Nombre del cliente.</param>
        /// <param name="address">Dirección del cliente.</param>
        /// <param name="email">Correo electrónico del cliente.</param>
        /// <param name="surname">Apellido del cliente.</param>
        /// <param name="language">Idioma preferido del cliente.</param>
        /// <param name="country">País de residencia del cliente.</param>
        /// <returns>El ID del cliente registrado.</returns>
        [Transactional]
        long RegisterClient(string loginName, string clearPassword, string name,
            string address, string email, string surname, string language, string country);

        /// <summary>
        /// Agrega una tarjeta bancaria a la cuenta de un cliente.
        /// </summary>
        /// <param name="ownerName">Nombre de usuario del propietario de la tarjeta.</param>
        /// <param name="creditType">Tipo de tarjeta (Visa, MasterCard, etc.).</param>
        /// <param name="creditCardNumber">Número de la tarjeta bancaria.</param>
        /// <param name="cvv">Código CVV de la tarjeta.</param>
        /// <param name="expirationDate">Fecha de expiración de la tarjeta.</param>
        /// <returns>La tarjeta bancaria agregada.</returns>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si el cliente no existe en el sistema.
        /// </exception>
        /// <exception cref="DuplicateBankCardException">
        /// Se lanza si la tarjeta bancaria ya está registrada en la cuenta del cliente.
        /// </exception>
        [Transactional]
        Bankcard AddBankCard(long clientId, string creditType,
            long bankCardNumber, string name, int cvv, DateTime expirationDate);

        /// <summary>
        /// Establece una tarjeta bancaria como la predeterminada para el usuario.
        /// </summary>
        /// <param name="bankCardId">ID de la tarjeta bancaria.</param>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si la tarjeta bancaria no se encuentra en el sistema.
        /// </exception>
        [Transactional]
        void SetBankCardAsDefault(long bankCardId);

        [Transactional]
        void RemoveBankcard(long bankcardId);

        /// <summary>
        /// Recupera la lista de tarjetas bancarias asociadas a un cliente.
        /// </summary>
        /// <param name="login">Nombre de usuario del cliente.</param>
        /// <returns>Lista de tarjetas bancarias del cliente.</returns>
        /// <exception cref="InstanceNotFoundException">
        /// Se lanza si el usuario no tiene tarjetas registradas.
        /// </exception>
        [Transactional]
        PagedResult<BankCardDetails> FindBankCardsByClientId(long clientId, int pageNumber, int pageSize);

        Client FindClientById(long clientId);

        Bankcard FindBankcardById(long bankcardId);
        List<BankCardDetails> FindAllBankCardsByClientId(long clientId);

        [Transactional]
        void UpdateBankcard(long bankcardId, string creditType, long bankCardNumber, string name, int cvv, DateTime expirationDate);

        [Transactional]
        void UpdateClientProfile(long userId, string name, string surname, string email, string language, string country);

    }
}
