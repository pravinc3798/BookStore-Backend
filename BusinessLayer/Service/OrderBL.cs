using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public OrderModel AddOrder(OrderModel orderModel)
        {
            try
            {
                return orderRL.AddOrder(orderModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OrderModel> ViewOrders(int userId)
        {
            try
            {
                return orderRL.ViewOrders(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                return orderRL.CancelOrder(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
