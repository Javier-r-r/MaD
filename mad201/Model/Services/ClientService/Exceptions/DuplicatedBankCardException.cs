using System;

namespace Model.Services.ClientService.Exceptions
{
    /// <summary>
    /// Excepción lanzada cuando se detecta que una tarjeta bancaria ya está registrada
    /// en el sistema para un cliente, evitando la duplicación de tarjetas bancarias.
    /// </summary>
    [Serializable]
    public class DuplicatedBankCardException : Exception
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DuplicatedBankCardException"/>.
        /// </summary>
        /// <param name="bankCardNumber">
        /// Número de la tarjeta bancaria que causa el error. 
        /// Este número es usado para identificar la tarjeta duplicada.
        /// </param>
        public DuplicatedBankCardException(long bankCardNumber)
            : base("Duplicated credit card => credit card = " + "**** **** **** " + $"{bankCardNumber % 10000}")
        {
            BankCardNumber = bankCardNumber;
        }

        /// <summary>
        /// Obtiene el número de tarjeta bancaria que causó la excepción.
        /// </summary>
        public long BankCardNumber { get; private set; }

    }
}
