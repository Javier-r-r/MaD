using Model.Daos.ProductDao;
using Model.Daos.RestaurantDao;
using Model.Daos.UserDao;
using Model.Daos.Util;
using Model.Services.UserService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.RestaurantService
{
    public interface IRestaurantService
    {
        IRestaurantDao RestaurantDao { set; }

        IUserDao UserDao { set; }

        IProductDao ProductDao { set; }

        long RegisterRestaurant(String loginName, String clearPassword, String name, String address, String email, String schedule, String RestaurantType, string language, string country);

        PagedResult<Restaurant> listRestaurants(int pageNumber, int pageSize);

        PagedResult<Restaurant> listRestaurantsFilterByType(string type, int pageNumber, int pageSize);

        List<string> getRestaurantTypes();

        PagedResult<Product> listRestaurantProducts(long restaurantId, int pageNumber, int pageSize);

        PagedResult<Product> listRestaurantProductsFilterByCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize);

        long AddProduct(long restaurantId, String productName, double price, DateTime creationDate, int stock, String category, List<ProductProperty> specificProperty);

        Product FindProduct(long productId);

        void UpdateProduct(long productId, String productName, double price, DateTime creationDate, int stock, String category, List<ProductProperty> specificProperty);

        void DeleteProduct(long product);

        long AddProductCategory(String categoryName);

        List<Category> FindAllCategories();

        List<Property> FindAllSpecificProperties();

        Restaurant FindRestaurantById(long restaurantId);

        void UpdateRestaurantProfile(long userId, string name, string email, string schedule, string type, string language, string country);

        void RemoveProperty(long propertyId);

    }
}