using Library.DataAccess;
using Library.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public class OrderRepository :IOrderRepository
    {
        public List<dynamic> getOrderList()
            => OrderManagement.Instance.getOrderList();
        public List<dynamic> getOrderByFilter(string textSearch, bool sortByDate)
            => OrderManagement.Instance.getOrderByFilter(textSearch, sortByDate);
        public object getObjectOrderById(int id)
            => OrderManagement.Instance.getObjectOrderById(id);
        public List<dynamic> getOrderDetailById(int id)
            => OrderManagement.Instance.getOrderDetailById(id);
        public double getServiceFee(int id)
            => OrderManagement.Instance.getServiceFee(id);
        public double getTableFee(int id)
            => OrderManagement.Instance.getTableFee(id);
        public double getDiscount(int id)
            => OrderManagement.Instance.getDiscount(id);
        public bool createNewOrder(int tableId)
            => OrderManagement.Instance.createNewOrder(tableId);
        public bool updateOrderDetail(OrderDetail orderDetail)
            => OrderManagement.Instance.updateOrderDetail(orderDetail);
        public int getOrderOfTableOn(int tableId)
            => OrderManagement.Instance.getOrderOfTableOn(tableId);
        public double getCurrentTableFee(int orderId, DateTime endTime)
            => OrderManagement.Instance.getCurrentTableFee(orderId, endTime);
        public bool updateOrder(Order order)
            => OrderManagement.Instance.updateOrder(order);
        public Order getOrderById(int id) 
            => OrderManagement.Instance.getOrderById(id);
    }
}
