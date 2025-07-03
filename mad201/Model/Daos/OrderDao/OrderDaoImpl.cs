using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.OrderDao
{
    public class OrderDaoImpl :
        GenericDaoEntityFramework<Order, Int64>, IOrderDao
    {

        public PagedResult<Order> FindByClientId(long clientId, int pageNumber, int pageSize)
        {

            DbSet<Order> allOrders = Context.Set<Order>();

            var result =
                (from o in allOrders
                 where o.Client.Id == clientId
                 select o);

            return result.ToPagedList(pageNumber, pageSize);
        }

        public Order FindByClientIdAndOrderDate(long clientId, DateTime orderDate)
        {
            Order order = null;

            DbSet<Order> allOrders = Context.Set<Order>();

            var result =
                (from o in allOrders
                 where o.Client.Id == clientId &&  o.orderDate == orderDate
                 select o);

            order = result.FirstOrDefault();

            return order;
        }
    }
}
