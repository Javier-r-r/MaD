using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Model.Daos.CartDao
{
    public interface ICartDao : IGenericDao<Cartline, long>
    {
        List<Cartline> FindByOrderId(long orderId);
    }
}
