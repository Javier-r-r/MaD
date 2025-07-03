using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public System.DateTime creationDate { get; set; }
        public int stock { get; set; }
    }
}
