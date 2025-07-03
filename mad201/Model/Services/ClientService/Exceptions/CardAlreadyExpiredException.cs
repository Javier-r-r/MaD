using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ClientService.Exceptions
{
    public class CardAlreadyExpiredException : Exception
    {
        public CardAlreadyExpiredException(DateTime expirationDate)
            : base($"La tarjeta ya está expirada (Fecha: {expirationDate:MM/yyyy})")
        {
        }
    }

}
