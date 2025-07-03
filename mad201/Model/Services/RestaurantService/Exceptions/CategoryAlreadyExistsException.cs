using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.RestaurantService.Exceptions
{
    public class CategoryAlreadyExistsException : Exception
    {
        public CategoryAlreadyExistsException(string categoryName)
            : base($"La categoria '{categoryName}' ya existe.")
        {
        }
    }
}
