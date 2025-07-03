using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.RestaurantDao
{
    public interface IRestaurantDao : IGenericDao<Restaurant, Int64>
    {
        /// <summary>
        /// Finds all Restaurants
        /// </summary>
        /// <returns>The List of restaurants/returns>
        PagedResult<Restaurant> FindAllRestaurants(int pageNumber, int pageSize);

        /// <summary>
        /// Finds all Restaurant types
        /// </summary>
        /// <returns>The List of Restaurant types/returns>
        List<string> FindAllRestaurantTypes();

        /// <summary>
        /// Finds  Restaurants by type
        /// </summary>
        /// <param type="name">name</param>
        /// <returns>The restaurant/returns>
        Restaurant FindByRestaurantName(String name);

        /// <summary>
        /// Finds  Restaurants by type
        /// </summary>
        /// <param type="restaurantType">restaurantType</param>
        /// <returns>The List of restaurants/returns>
        PagedResult<Restaurant> FindByRestaurantType(String type, int pageNumber, int pageSize);
    }
}
