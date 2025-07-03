using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.RestaurantService.Exceptions
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException (string productName)
            : base($"El producto '{productName}' ya existe.")
        {
        }
    }
}
