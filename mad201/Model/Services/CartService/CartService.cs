using Es.Udc.DotNet.ModelUtil.Exceptions;
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
using System;
using System.Collections.Generic;

namespace Model.Services.CartService
{
    public class CartService : ICartService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }
        [Inject]
        public IOrderDao OrderDao { private get; set; }
        [Inject]
        public ICartDao CartDao { private get; set; }
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public IClientDao ClientDao { private get; set; }
        [Inject]
        public IBankCardDao BankCardDao { private get; set; }
        [Transactional]

        long ICartService.AddOrder(long clientId, string address, long bankardId, List<CartLineDto> cartLines)
        {
            Client client = ClientDao.Find(clientId);
            Bankcard card = BankCardDao.Find(bankardId);

            Order order = new Order();

            order.orderDate = DateTime.Now;
            order.orderAddress = address;
            order.bankcardNumber = card.number;
            order.Client = client;

            OrderDao.Create(order);

            foreach (CartLineDto cartLineDto in cartLines)
            {
                Cartline cartline = new Cartline();
                cartline.Order = order;
                cartline.price = cartLineDto.price;
                cartline.uds = cartLineDto.units;
                cartline.productName = cartLineDto.productName;
                CartDao.Create(cartline);

            }

            return order.Id;
        }

        [Transactional]
        public PagedResult<Order> ViewClientOrders(long clientId, int pageNumber, int pageSize)
        {
            var orders = OrderDao.FindByClientId(clientId, pageNumber, pageSize);
            return orders;
        }
    }
}
