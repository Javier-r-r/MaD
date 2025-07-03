
using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.OrderDao
{
    public interface IOrderDao : IGenericDao<Order, Int64>
    {
        /// <summary>
        /// Finds  orders by user
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>The list of orders/returns>
        PagedResult<Order> FindByClientId(long clientId, int pageNumber, int pageSize);

        /// <summary>
        /// Finds  order by user and orderDate
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="orderDate"></param>
        /// <returns>The the order/returns>
        Order FindByClientIdAndOrderDate(long clientId, DateTime orderDate);
    }
}
