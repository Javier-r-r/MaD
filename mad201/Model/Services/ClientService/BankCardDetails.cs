using Model.Daos.Util;
using System;
using System.Collections.Generic;

namespace Model.Services.ClientService
{
    /// <summary>
    /// Representa los detalles de una tarjeta bancaria.
    /// Contiene información relevante sobre la tarjeta, como su tipo y fecha de expiración.
    /// </summary>
    public class BankCardDetails
    {
        #region Properties 

        /// <summary>
        /// Identificador único de la tarjeta bancaria.
        /// </summary>
        public long BankCardId { get; private set; }

        /// <summary>
        /// Nombre del propietario de la tarjeta.
        /// </summary>
        public string Name { get; private set; }

        public string CreditCardNumber { get; private set; }

        public int Cvv { get; private set; }

        /// <summary>
        /// Tipo de tarjeta de crédito (por ejemplo, Visa, MasterCard).
        /// </summary>
        public string CreditCardType { get; private set; }

        /// <summary>
        /// Fecha de expiración de la tarjeta.
        /// </summary>
        public DateTime ExpirationDate { get; private set; }

        /// <summary>
        /// Indica si esta tarjeta es la predeterminada del usuario.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Constructor para inicializar los detalles de una tarjeta bancaria.
        /// </summary>
        /// <param name="bankCardId">Identificador único de la tarjeta.</param>
        /// <param name="ownerName">Nombre del propietario de la tarjeta.</param>
        /// <param name="creditCardType">Tipo de tarjeta de crédito.</param>
        /// <param name="expirationDate">Fecha de expiración de la tarjeta.</param>
        public BankCardDetails(long bankCardId, string ownerName, string creditCardNumber, string creditCardType, DateTime expirationDate, bool @default)
        {
            BankCardId = bankCardId;
            Name = ownerName;
            CreditCardNumber = creditCardNumber;
            CreditCardType = creditCardType;
            ExpirationDate = expirationDate;
            IsDefault = @default;
        }

        #endregion Properties Region

        /// <summary>
        /// Convierte una lista paginada Bankcard en una lista paginadas BankCardDetails.
        /// </summary>
        /// <param name="bankCards">Lista paginada de Bankcard a convertir.</param>
        /// <returns>Lista paginada BankCardDetails.</returns>
        public static PagedResult<BankCardDetails> fromBankCardToBankCardDetails(PagedResult<Bankcard> pagedBankCards)
        {
            List<BankCardDetails> bankCardDetails = new List<BankCardDetails>();

            foreach (var bankCard in pagedBankCards.Items)
            {
                BankCardDetails newBankCardDetails = new BankCardDetails(
                    bankCard.Id,
                    bankCard.name,
                    bankCard.number.ToString().Substring(bankCard.number.ToString().Length - 4),
                    bankCard.cardtype,
                    bankCard.expirationdate,
                    bankCard.@default
                );

                bankCardDetails.Add(newBankCardDetails);
            }

            return new PagedResult<BankCardDetails>(
                bankCardDetails,
                pagedBankCards.TotalItems,
                pagedBankCards.PageNumber,
                pagedBankCards.PageSize
            );
        }

        /// <summary>
        /// Convierte una lista de objetos Bankcard en una lista de objetos BankCardDetails.
        /// </summary>
        /// <param name="bankCards">Lista de objetos Bankcard a convertir.</param>
        /// <returns>Lista de objetos BankCardDetails.</returns>
        public static List<BankCardDetails> fromBankCardToBankCardDetailsList(List<Bankcard> bankCards)
        {
            List<BankCardDetails> bankCardDetails = new List<BankCardDetails>();

            foreach (var bankCard in bankCards)
            {
                BankCardDetails newBankCardDetails = new BankCardDetails(
                    bankCard.Id,
                    bankCard.name,
                    bankCard.number.ToString().Substring(bankCard.number.ToString().Length - 4),
                    bankCard.cardtype,
                    bankCard.expirationdate,
                    bankCard.@default
                );

                bankCardDetails.Add(newBankCardDetails);
            }

            return bankCardDetails;
        }
    }
}
