using System;

namespace Model.Services.ClientService.Exceptions
{
    public class InvalidCardNumberException : Exception
    {
        public long InvalidNumber { get; }

        public InvalidCardNumberException(long number)
            : base($"El número de tarjeta {number} no es válido.")
        {
            this.InvalidNumber = number;
        }
    }
}
