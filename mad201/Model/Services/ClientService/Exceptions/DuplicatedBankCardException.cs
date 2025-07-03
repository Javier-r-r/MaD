using System;

namespace Model.Services.ClientService.Exceptions
{
    /// <summary>
    /// Excepci�n lanzada cuando se detecta que una tarjeta bancaria ya est� registrada
    /// en el sistema para un cliente, evitando la duplicaci�n de tarjetas bancarias.
    /// </summary>
    [Serializable]
    public class DuplicatedBankCardException : Exception
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DuplicatedBankCardException"/>.
        /// </summary>
        /// <param name="bankCardNumber">
        /// N�mero de la tarjeta bancaria que causa el error. 
        /// Este n�mero es usado para identificar la tarjeta duplicada.
        /// </param>
        public DuplicatedBankCardException(long bankCardNumber)
            : base("Duplicated credit card => credit card = " + "**** **** **** " + $"{bankCardNumber % 10000}")
        {
            BankCardNumber = bankCardNumber;
        }

        /// <summary>
        /// Obtiene el n�mero de tarjeta bancaria que caus� la excepci�n.
        /// </summary>
        public long BankCardNumber { get; private set; }

    }
}
