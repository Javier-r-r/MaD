using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Linq;

namespace Model.Daos.CartDao
{
    public class CartDaoImpl : GenericDaoEntityFramework<Cartline, long>, ICartDao
    {
        public List<Cartline> FindByOrderId(long orderId)
        {
            var context = Context;

            // Accede al DbSet dinámicamente
            var cartlines = context.Set<Cartline>()
                                   .Where(cl => cl.Order.Id == orderId)
                                   .ToList();

            return cartlines;
        }
    }

}
