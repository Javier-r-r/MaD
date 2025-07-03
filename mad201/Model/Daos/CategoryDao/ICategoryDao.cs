using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {

        /// <summary>
        /// Finds  Category by name
        /// </summary>
        /// <param type="name">name</param>
        /// <returns>The category/returns>
        Category FindByCategoryName(String name);
    }
}
