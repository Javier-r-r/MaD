using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.BankCardDao;
using Model.Daos.CartDao;
using Model.Daos.ClientDao;
using Model.Daos.OrderDao;
using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Model.Daos.Util;
using Model.Services.CartService.DTOs;
using Ninject;
using System.Collections.Generic;

namespace Model.Services.CartService
{
    public interface ICartService
    {

        [Inject]
        IProductDao ProductDao { set; }
        [Inject]
        IOrderDao OrderDao { set; }
        [Inject]
        ICartDao CartDao { set; }

        [Inject]
        IClientDao ClientDao { set; }
        [Inject]
        IBankCardDao BankCardDao { set; }


        [Transactional]
        long AddOrder(long clientId, string address, long bankardId, List<CartLineDto> cartLines);

        [Transactional]
        PagedResult<Order> ViewClientOrders(long clientId, int pageNumber, int pageSize);
        
    }
}
