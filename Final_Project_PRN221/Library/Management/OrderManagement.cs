using Library.DataAccess;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management
{
    public class OrderManagement
    {
        private static OrderManagement instance;
        private static readonly object instanceLock = new object();

        public OrderManagement() { }
        public static OrderManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderManagement();
                    }
                    return instance;
                }
            }
        }

        public List<dynamic> getOrderList()
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Orders.Select(
                        o => new
                        {
                            Id = o.Id,
                            TableId = o.TableId,
                            Table = o.Table.Name,
                            Discount = o.Discount,
                            StartTime = o.StartTime,
                            EndTime = o.EndTime,
                            PlayTime = manage.getTotalHoursOrder(o.StartTime, o.EndTime),
                            Total = (manage.getServiceFee(o.Id) +
                            manage.getTotalHoursOrder(o.StartTime, o.EndTime)
                            * (Convert.ToDouble(o.Table.Type.PricePerHour))) * (1 - o.Discount / 100),
                            OrderById = o.OrderBy,
                            OrderBy = o.Account.FullName
                        }).Where(o=>o.EndTime != o.StartTime).OrderByDescending(o => o.Id);

                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<dynamic> getOrderByFilter
            (string textSearch, bool sortByDate)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Orders.Select(
                        o => new
                        {
                            Id = o.Id,
                            TableId = o.TableId,
                            Table = o.Table.Name,
                            Discount = o.Discount,
                            StartTime = o.StartTime,
                            EndTime = o.EndTime,
                            PlayTime = manage.getTotalHoursOrder(o.StartTime, o.EndTime),
                            Total = (manage.getServiceFee(o.Id) +
                            manage.getTotalHoursOrder(o.StartTime, o.EndTime)
                            * (Convert.ToDouble(o.Table.Type.PricePerHour))) * (1 - o.Discount / 100),
                            OrderById = o.OrderBy,
                            OrderBy = o.Account.FullName
                        }).Where(o => o.EndTime != o.StartTime);
                    if (textSearch.Trim() != "")
                    {
                        list = list.Where(o => o.Table.ToLower().Contains(textSearch.ToLower())
                        || o.StartTime.ToString().Contains(textSearch)
                        || o.EndTime.ToString().Contains(textSearch)
                        || o.OrderBy.ToLower().Contains(textSearch.ToLower()));
                    }
                    if (sortByDate == true)
                    {
                        list = list.OrderByDescending(o => o.Id);
                    }
                    else
                    {
                        list = list.OrderBy(o => o.Id);
                    }

                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public double getTotalHoursOrder(DateTime startTime, DateTime endTime)
        {
            double totalHours = (endTime - startTime).TotalHours;
            if(totalHours < 0.25)
            {
                totalHours = 0.25;
            }
            return Math.Round(totalHours, 2);
        }

        public double getServiceFee(int id)
        {
            double total = 0;
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    total = (double)context.OrderDetails.Where(o => o.OrderId == id)
                        .Sum(o => o.Product.UnitPrice * o.Quantity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Math.Round(total,2);
        }

        public double getTableFee(int id)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var tableFee = context.Orders
                        .Where(o => o.Id == id)
                        .Select(o => manage.getTotalHoursOrder(o.StartTime, o.EndTime)
                            * Convert.ToDouble(o.Table.Type.PricePerHour))
                        .FirstOrDefault();
                    return Math.Round(tableFee,2);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public double getDiscount(int id)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var tableFee = context.Orders
                        .Where(o => o.Id == id)
                        .Select(o => o.Discount)
                        .FirstOrDefault();
                    return (double)tableFee;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public object getObjectOrderById(int id)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var order = context.Orders.Where(o=> o.Id ==id).Select(
                        o => new
                        {
                            Id = o.Id,
                            TableId = o.TableId,
                            Table = o.Table.Name,
                            Discount = o.Discount,
                            StartTime = o.StartTime,
                            EndTime = o.EndTime,
                            PlayTime = manage.getTotalHoursOrder(o.StartTime, o.EndTime),
                            Total = (manage.getServiceFee(o.Id) +
                            manage.getTotalHoursOrder(o.StartTime, o.EndTime)
                            * (Convert.ToDouble(o.Table.Type.PricePerHour))) * (1 - o.Discount / 100),
                            OrderById = o.OrderBy,
                            OrderBy = o.Account.FullName
                        });
                    return order;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<dynamic> getOrderDetailById(int id)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.OrderDetails.Select(
                        o => new
                        {
                            OrderId = o.OrderId,
                            ProductId = o.ProductId,
                            ProductName = o.Product.Name,
                            UnitPrice = o.Product.UnitPrice,
                            Quantity = o.Quantity,
                            Total = o.Product.UnitPrice * o.Quantity,
                            Image = "Images/" + o.Product.Image
                        }
                    ).Where(o => o.OrderId == id);
                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool createNewOrder(int tableId)
        {
            Order order = new Order()
            {
                Id = 0,
                TableId = tableId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
            };
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool updateOrderDetail(OrderDetail orderDetail)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    OrderDetail _orderDetail = context.OrderDetails.
                        FirstOrDefault(o=> o.OrderId == orderDetail.OrderId 
                        && o.ProductId == orderDetail.ProductId);
                    if(_orderDetail != null)
                    {
                        _orderDetail.Quantity = orderDetail.Quantity;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        context.OrderDetails.Add(orderDetail);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int getOrderOfTableOn(int tableId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Order order = context.Orders.
                        FirstOrDefault(o=> o.TableId == tableId
                        && o.StartTime ==o.EndTime);
                    return order.Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public double getCurrentTableFee(int orderId, DateTime endTime)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var tableFee = context.Orders
                        .Where(o => o.Id == orderId)
                        .Select(o => manage.getTotalHoursOrder(o.StartTime, endTime)
                            * Convert.ToDouble(o.Table.Type.PricePerHour))
                        .FirstOrDefault();
                    return Math.Round(tableFee,2);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool updateOrder(Order order)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Order _order = context.Orders.FirstOrDefault(o => o.Id == order.Id);
                    if (_order != null)
                    {
                        _order.EndTime = order.EndTime;
                        _order.Discount = order.Discount;
                        _order.OrderBy = order.OrderBy;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Order getOrderById(int id)
        {
            OrderManagement manage = new OrderManagement();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var order = context.Orders.FirstOrDefault(o => o.Id == id);
                    return order;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
