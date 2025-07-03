using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.RestaurantDao
{
    public class RestaurantDaoImpl :
         GenericDaoEntityFramework<Restaurant, Int64>, IRestaurantDao
    {


        public PagedResult<Restaurant> FindAllRestaurants(int pageNumber, int pageSize)
        {
            DbSet<Restaurant> restaurants = Context.Set<Restaurant>();

            var result = (from r in restaurants
                          select r);

            return result.ToPagedList(pageNumber, pageSize);
        }

        public List<string> FindAllRestaurantTypes()
        {
            DbSet<Restaurant> restaurants = Context.Set<Restaurant>();

            var result = (from r in restaurants
                          select r.type)
                         .Distinct()
                         .OrderBy(type => type);

            return result.ToList();
        }
        public Restaurant FindByRestaurantName(string name)
        {

            DbSet<Restaurant> restaurants = Context.Set<Restaurant>();

            var result = (from r in restaurants
                          where r.name == name
                          select r);

            return result.FirstOrDefault();
        }

        public PagedResult<Restaurant> FindByRestaurantType(string type, int pageNumber = 1, int pageSize = 10)
        {

            DbSet<Restaurant> restaurants = Context.Set<Restaurant>();

            var result = (from r in restaurants
                          where r.type == type
                          select r);

            return result.ToPagedList(pageNumber, pageSize);
        }
    }
}
