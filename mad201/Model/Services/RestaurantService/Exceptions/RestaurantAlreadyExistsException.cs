using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.RestaurantService.Exceptions
{
    public class RestaurantAlreadyExistsException : Exception
    {
        public RestaurantAlreadyExistsException(string restaurantName)
            : base($"Ya existe un restaurante con nombre '{restaurantName}' ")
        {
        }
    }
}
