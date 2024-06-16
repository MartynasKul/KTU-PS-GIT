using System;
using System.Collections.Generic;

namespace LD5_10_MKuliesius.AppCode
{
    /// <summary>
    /// A container to store all orders for a given date
    /// </summary>
    public class OrderContainer
    {
        public DateTime Date;
        public List<Order> Orders;

        public OrderContainer() { }
        public OrderContainer(DateTime date, List<Order> orders) 
        {
            this.Date = date;
            this.Orders = orders;
        }

        public Order GetOrder(int id) 
        {
            return Orders[id];
        }
        public List<Order> GetAllOrders()
        {
            return Orders;
        }
    }
}