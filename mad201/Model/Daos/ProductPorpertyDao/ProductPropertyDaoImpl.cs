using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.PropertyDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.ProductPorpertyDao
{
    public class ProductPropertyDaoImpl : GenericDaoEntityFramework<ProductProperty, long>, IProductPropertyDao
    {
    }
}
