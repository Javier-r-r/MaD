using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.CategoryDao
{
    public class CategoryDaoImpl :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {

        public Category FindByCategoryName(string name)
        {

            DbSet<Category> categories = Context.Set<Category>();

            var result = (from c in categories
                          where c.categoryName == name
                          select c);

            return result.FirstOrDefault();
        }
    }
}
