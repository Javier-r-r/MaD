using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.ProductDao
{
    public class ProductDaoImpl :
        GenericDaoEntityFramework<Product, long>, IProductDao
    {   
        
        public Product FindByProductName(string productName)
        {
            Product product = null;

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.name == productName
                 select p);

            product = result.FirstOrDefault();



            if (product == null)
                throw new InstanceNotFoundException(productName,
                    typeof(Product).FullName);

            return product;
        }

        public PagedResult<Product> FindByProductCategory(long categoryId, int pageNumber, int pageSize)
        {
            DbSet<Product> allProducts = Context.Set<Product>();

            var result =
                (from p in allProducts
                 .Where(p => p.Category.Id == categoryId)
                 select p);


            return result.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<Product> FindByRestaurant(long restaurantId, int pageNumber, int pageSize)
        {
            DbSet<Product> allProducts = Context.Set<Product>();

            var result =
                (from p in allProducts
                 .Where(p => p.Restaurant.Id == restaurantId)
                 select p);


            return result.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<Product> FindByRestaurantAndCategory(long restaurantId, long categoryId, int pageNumber, int pageSize)
        {
            DbSet<Product> allProducts = Context.Set<Product>();
            DbSet<Category> allCategories = Context.Set<Category>();

            var categoryIds = allCategories
                .Where(c => c.Id == categoryId || c.Category2.Id == categoryId)
                .Select(c => c.Id)
                .ToList();

            var result = allProducts
                .Where(p => p.Restaurant.Id == restaurantId && categoryIds.Contains(p.Category.Id));

            return result.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<Product> FindByRestaurantAndKeywords(long restaurantId, string keywords, int pageNumber, int pageSize)
        {
            DbSet<Product> allProducts = Context.Set<Product>();
            var lowerKeywords = keywords.ToLower();

            var result = allProducts
                .Where(p =>
                    p.Restaurant.Id == restaurantId && (p.name.ToLower().Contains(lowerKeywords)));

            return result.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<Product> FindByRestaurantAndCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize)
        {
            DbSet<Product> allProducts = Context.Set<Product>();
            DbSet<Category> allCategories = Context.Set<Category>();

            var categoryIds = allCategories
                .Where(c => c.Id == categoryId || c.Category2.Id == categoryId)
                .Select(c => c.Id)
                .ToList();

            string keyword = keywords?.Trim().ToLower();

            var result = allProducts
                .Where(p =>
                    p.Restaurant.Id == restaurantId &&
                    categoryIds.Contains(p.Category.Id) &&
                    (string.IsNullOrEmpty(keyword) || p.name.ToLower().Contains(keyword))
                );

            return result.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<Product> SearchFilters(long restaurantId, int pageNumber, int pageSize, long categoryId = 0, string name = "") 
        {
            DbSet<Product> allProducts = Context.Set<Product>();
            var query = allProducts.AsQueryable();

            if(restaurantId <= 0)
            {
                throw new ArgumentException("Debe seleccionar un restaurante");
            }

            if(categoryId <= 0)
            {
                query = query.Where(p => p.Category.Id == categoryId);
            }

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.name.Contains(name));
            }

            return query.ToPagedList(pageNumber, pageSize);
        }

        public PagedResult<ProductProperty> FindPropertiesByProductId(long productId, int pageNumber, int pageSize)
        {
            DbSet<ProductProperty> allProperties = Context.Set<ProductProperty>();

            var query = allProperties.AsQueryable();

            if(productId <= 0)
            {
                throw new ArgumentNullException();
            }

            return query.Where(p => p.Product.Id == productId).ToPagedList(pageNumber, pageSize);
        }
    }
}
