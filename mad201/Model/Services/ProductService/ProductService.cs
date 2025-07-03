using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ProductDao;
using Model.Daos.PropertyDao;
using Model.Services.ProductService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }
        [Inject]
        public IPropertyDao PropertyDao { private get; set; }

        [Transactional]
        public List<ProductDto> FindByProductCategory(long categoryId, int pageNumber, int pageSize)
        {
            if(categoryId <= 0)
            {
                throw new ArgumentNullException();
            }

            return ConvertToProductDto(ProductDao.FindByProductCategory(categoryId, pageNumber, pageSize).Items);
        }

        [Transactional]
        public ProductDto FindByProductName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException();
            }

            return FromProduct(ProductDao.FindByProductName(productName));
        }

        [Transactional]
        public List<ProductDto> SearchFilters(long restaurantId, int pageNumber, int pageSize, long categoryId, string name)
        {
            if(restaurantId <= 0)
            {
                throw new ArgumentNullException();
            }

            return ConvertToProductDto(ProductDao.SearchFilters(restaurantId, pageNumber, pageSize, categoryId, name).Items);
        }

        [Transactional]
        public List<ProductProperty> FindPropertiesByProductId(long productId, int pageNumber, int pageSize)
        {
            if(productId <= 0)
            {
                throw new ArgumentException();
            }

            return ProductDao.FindPropertiesByProductId(productId, pageNumber, pageSize).Items;
        }

        [Transactional]
        public List<Property> ViewAllProperties()
        {
            return PropertyDao.GetAllElements().ToList();
        }

        private ProductDto FromProduct(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                name = product.name,
                price = product.price,
                creationDate = product.creationDate,
                stock = product.stock 
            };
        }

        private List<ProductDto> ConvertToProductDto(List<Product> products)
        {
            return products.Select(p => FromProduct(p)).ToList();
        }


    }
}
