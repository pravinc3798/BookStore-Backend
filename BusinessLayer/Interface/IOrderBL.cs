using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel);
        public List<OrderModel> ViewOrders(int userId);
        public bool CancelOrder(int orderId);
    }
}
