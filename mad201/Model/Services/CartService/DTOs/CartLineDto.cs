using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.CartService.DTOs
{
    public class CartLineDto
    {
        public string productName { get; set; }
        public double price { get; set; }
        public int units { get; set; }

        public CartLineDto(string productName, double price)
        {
            this.productName = productName;
            this.price = price;
            this.units = 1;
        }


    }
}
