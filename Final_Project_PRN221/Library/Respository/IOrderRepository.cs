﻿using Library.DataAccess;

namespace Library.Respository
{
    public interface IOrderRepository
    {
        public List<dynamic> getOrderList();
        public List<dynamic> getOrderByFilter
            (string textSearch, bool sortByDate);
        public object getObjectOrderById(int id);
        public List<dynamic> getOrderDetailById(int id);
        public double getServiceFee(int id);
        public double getTableFee(int id);
        public double getDiscount(int id);
        public bool createNewOrder(int tableId);
        public bool updateOrderDetail(OrderDetail orderDetail);
        public int getOrderOfTableOn(int tableId);
        public double getCurrentTableFee(int orderId, DateTime endTime);
        public bool updateOrder(Order order);
        public Order getOrderById(int id);
        public Order getLastestOrderOfTable(int tableId);
        public List<Order> getOrderByStartTime(DateTime dateTime);

    }
}
