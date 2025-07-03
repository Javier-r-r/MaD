using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;

namespace Model.Daos.ProductDao
{
    public interface IProductDao : IGenericDao<Product, long>
    {

        /// <summary>
        /// Finds a product by its name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>The product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Product FindByProductName(String productName);

        /// <summary>
        /// Finds  Products by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>The list of products/returns>
        PagedResult<Product> FindByProductCategory(long categoryId, int pageNumber, int pageSize);

        PagedResult<Product> FindByRestaurant(long restaurantId, int pageNumber, int pageSize);

        PagedResult<Product> FindByRestaurantAndCategory(long restaurantId, long categoryId, int pageNumber, int pageSize);

        PagedResult<Product> FindByRestaurantAndKeywords(long restaurantId, string keywords, int pageNumber, int pageSize);

        PagedResult<Product> FindByRestaurantAndCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize);

        PagedResult<Product> SearchFilters(long restaurantId, int pageNumber, int pageSize, long categoryId, string name);

        PagedResult<ProductProperty> FindPropertiesByProductId(long productId, int pageNumber, int pageSize);
    }
}
