using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.CategoryDao;
using Model.Daos.ProductDao;
using Model.Daos.ProductPorpertyDao;
using Model.Daos.PropertyDao;
using Model.Daos.RestaurantDao;
using Model.Daos.UserDao;
using Model.Daos.Util;
using Model.Services.RestaurantService.Exceptions;
using Model.Services.UserService.Util;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        [Inject]
        public IRestaurantDao RestaurantDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public IPropertyDao PropertyDao { private get; set; }

        [Inject]
        public IProductPropertyDao ProductPropertyDao { private get; set; }


        [Transactional]
        public long RegisterRestaurant(string loginName, string clearPassword, string name, string address, string email, string schedule, string restaurantType, string language, string country)
        {
            try
            {
                if (RestaurantDao.FindByRestaurantName(name) != null)
                {
                    throw new RestaurantAlreadyExistsException(name);
                }

                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException("User", loginName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                Restaurant restaurant = new Restaurant();

                restaurant.login = loginName;
                restaurant.password = encryptedPassword;
                restaurant.name = name;
                restaurant.address = address;
                restaurant.email = email;
                restaurant.schedule = schedule;
                restaurant.type = restaurantType;
                restaurant.language = language;
                restaurant.country = country;

                RestaurantDao.Create(restaurant);

                return restaurant.Id;
            }
        }

        [Transactional]
        public PagedResult<Restaurant> listRestaurants(int pageNumber, int pageSize)
        {

            PagedResult<Restaurant> restaurants = null;

            restaurants = RestaurantDao.FindAllRestaurants(pageNumber, pageSize);

            return restaurants;
        }



        [Transactional]
        public PagedResult<Restaurant> listRestaurantsFilterByType(string type, int pageNumber, int pageSize)
        {

            PagedResult<Restaurant> restaurants = null;

            restaurants = RestaurantDao.FindByRestaurantType(type, pageNumber, pageSize);

            return restaurants;
        }

        [Transactional]
        public List<string> getRestaurantTypes()
        {

            List<string> restaurantTypes = null;

            restaurantTypes = RestaurantDao.FindAllRestaurantTypes();

            return restaurantTypes;
        }
        

        public PagedResult<Product> listRestaurantProducts(long restaurantId, int pageNumber, int pageSize)
        {

            PagedResult<Product> products = null;

            products = ProductDao.FindByRestaurant(restaurantId, pageNumber, pageSize);

            return products;
        }

        public PagedResult<Product> listRestaurantProductsFilterByCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize)
        {

            if (categoryId != 0 && !string.IsNullOrWhiteSpace(keywords))
            {
                return ProductDao.FindByRestaurantAndCategoryAndKeywords(categoryId, keywords, restaurantId, pageNumber, pageSize);
            }
            else if (categoryId != 0)
            {
                return ProductDao.FindByRestaurantAndCategory(restaurantId, categoryId, pageNumber, pageSize);
            }
            else if (!string.IsNullOrWhiteSpace(keywords))
            {
                return ProductDao.FindByRestaurantAndKeywords(restaurantId, keywords, pageNumber, pageSize);
            }
            else
            {
                return ProductDao.FindByRestaurant(restaurantId, pageNumber, pageSize);
            }
        }


        [Transactional]

        public long AddProduct(long restaurantId, String productName, double price, DateTime creationDate, int stock, String category, List<ProductProperty> specificProperties)
        {
            if (!RestaurantDao.Exists(restaurantId))
            {
                throw new InstanceNotFoundException(restaurantId,
                typeof(Restaurant).FullName);
            }

            if (CategoryDao.FindByCategoryName(category) == null)   
            {
                Category newCategory = new Category();
                newCategory.categoryName = category;

                CategoryDao.Create(newCategory);
            }

            try
            {
                ProductDao.FindByProductName(productName);

                throw new ProductAlreadyExistsException(productName);
            }
            catch (InstanceNotFoundException)
            {
                Product product = new Product();

                product.name = productName;
                product.price = price;
                product.creationDate = creationDate;
                product.stock = stock;
                product.Category = CategoryDao.FindByCategoryName(category);
                product.Restaurant = RestaurantDao.Find(restaurantId);

                ProductDao.Create(product);

                foreach (ProductProperty specificProperty in specificProperties)
                {

                    specificProperty.Product = product;
                    specificProperty.Property = PropertyDao.FindByName(specificProperty.Property.name);
                    ProductPropertyDao.Create(specificProperty);
                }


                return product.Id;
            }
        }

        [Transactional]

        public Product FindProduct(long productId)
        {
            if (!ProductDao.Exists(productId))
            {
                throw new InstanceNotFoundException(productId,
                    typeof(Product).FullName);
            }

            return ProductDao.Find(productId);
        }

        public void UpdateProduct(long productId, String productName, double price, DateTime creationDate, int stock, String category, List<ProductProperty> specificProperties)
        {
            if (!ProductDao.Exists(productId))
            {
                throw new InstanceNotFoundException(productId,
                    typeof(Product).FullName);
            }

            Product product = ProductDao.Find(productId);

            product.name = productName;
            product.price = price;
            product.creationDate = creationDate;
            product.stock = stock;

            if (product.Category.categoryName != category)
            {
                product.Category = CategoryDao.FindByCategoryName(category);
            }

            var newPropertyIds = specificProperties
                .Where(p => p.Id != 0)
                .Select(p => p.Id)
                .ToList();

            foreach (ProductProperty oldSpecificProperty in product.ProductPorperty.ToList())
            {
                if (!newPropertyIds.Contains(oldSpecificProperty.Id))
                {
                    ProductPropertyDao.Remove(oldSpecificProperty.Id);
                }
            }

            foreach (ProductProperty newSpecificProperty in specificProperties.ToList())
            {
                if (newSpecificProperty.Id == 0)
                {
                    ProductProperty propertyToAdd = new ProductProperty()
                    {
                        Product = product,
                        Property = PropertyDao.FindByName(newSpecificProperty.Property.name),
                        value = newSpecificProperty.value
                    };
                    ProductPropertyDao.Create(propertyToAdd);
                }
            }


            ProductDao.Update(product);
        }

        public void DeleteProduct(long productId)
        {
            if (!ProductDao.Exists(productId))
            {
                throw new InstanceNotFoundException(productId,
                    typeof(Product).FullName);
            }
            var properties = PropertyDao.FindProductProperties(productId);
            foreach (var property in properties)
            {
                ProductPropertyDao.Remove(property.Id);
            }

            ProductDao.Remove(productId);

        }


        public long AddProductCategory(String categoryName)
        {
            if (CategoryDao.FindByCategoryName(categoryName) != null)
            {
                throw new CategoryAlreadyExistsException(categoryName);
            }

            Category newCategory = new Category();
            newCategory.categoryName = categoryName;

            CategoryDao.Create(newCategory);

            return CategoryDao.FindByCategoryName(categoryName).Id;

        }

        public List<Category> FindAllCategories()
        {
            return CategoryDao.GetAllElements();
        }

        public List<Property> FindAllSpecificProperties()
        {
            return PropertyDao.FindDistinctProperties();
        }

        public Restaurant FindRestaurantById(long restaurantId)
        {
            return RestaurantDao.Find(restaurantId);
        }

        public void UpdateRestaurantProfile(long userId, string name, string email, string schedule, string type, string language, string country)
        {
            Restaurant restaurant = RestaurantDao.Find(userId);

            if (restaurant == null)
            {
                throw new InstanceNotFoundException("Restaurant not found", userId.ToString());
            }

            restaurant.name = name;
            restaurant.email = email;
            restaurant.schedule = schedule;
            restaurant.type = type;
            restaurant.language = language;
            restaurant.country = country;

            RestaurantDao.Update(restaurant);
            
        }

        public void RemoveProperty(long propertyId)
        {
            ProductPropertyDao.Remove(propertyId);
        }

    }
}
