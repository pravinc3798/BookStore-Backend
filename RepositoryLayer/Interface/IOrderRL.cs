using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel orderModel);
        public List<OrderModel> ViewOrders(int userId);
        public bool CancelOrder(int orderId);
    }
}
