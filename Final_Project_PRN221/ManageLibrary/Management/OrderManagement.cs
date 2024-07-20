using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management
{
    public class OrderManagement
    {
    //    private static OrderManagement instance;
    //    private static readonly object instanceLock = new object();

    //    public OrderManagement() { }
    //    public static OrderManagement Instance
    //    {
    //        get
    //        {
    //            lock (instanceLock)
    //            {
    //                if (instance == null)
    //                {
    //                    instance = new OrderManagement();
    //                }
    //                return instance;
    //            }
    //        }
    //    }

    //    public List<dynamic> getOrderList()
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                var list = context..Select(
    //                    o => new
    //                    {
    //                        Id = o.Id,
    //                        Name = o.Name,
    //                        UnitPrice = o.UnitPrice,
    //                        UnitsInStock = o.UnitsInStock,
    //                        Image = "Images/" + o.Image,
    //                        CategoryId = o.CategoryId,
    //                        CategoryName = o.Category.Name
    //                    });

    //                return list.Cast<dynamic>().ToList();
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public List<dynamic> getOrderByFilter
    //        (string textSearch, string category, string orderBy)
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                var list = context.Orders.Select(
    //                    o => new
    //                    {
    //                        Id = o.Id,
    //                        Name = o.Name,
    //                        UnitPrice = o.UnitPrice,
    //                        UnitsInStock = o.UnitsInStock,
    //                        Image = "Images/" + o.Image,
    //                        CategoryId = o.CategoryId,
    //                        CategoryName = o.Category.Name
    //                    });
    //                if (textSearch.Trim() != "")
    //                {
    //                    list = list.Where(o => o.Name.ToLower().Contains(textSearch.ToLower())
    //                    || o.CategoryName.ToLower().Contains(textSearch.ToLower()));
    //                }
    //                if (category != "")
    //                {
    //                    list = list.Where(o => o.CategoryId.ToString().Equals(category));
    //                }
    //                if (orderBy != "")
    //                {
    //                    if (orderBy == "1")
    //                    {
    //                        list = list.OrderBy(o => o.UnitPrice);
    //                    }
    //                    else if (orderBy == "2")
    //                    {
    //                        list = list.OrderByDescending(o => o.UnitPrice);
    //                    }
    //                    else if (orderBy == "3")
    //                    {
    //                        list = list.OrderBy(o => o.UnitsInStock);
    //                    }
    //                    else if (orderBy == "4")
    //                    {
    //                        list = list.OrderByDescending(o => o.UnitsInStock);
    //                    }
    //                }

    //                return list.Cast<dynamic>().ToList();
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public bool addOrder(Order Order)
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                context.Orders.Add(Order);
    //                context.SaveChanges();
    //                return true;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public bool deleteOrder(int id)
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                Order Order = context.Orders.FirstOrDefault(o => o.Id == id);
    //                if (Order != null)
    //                {
    //                    context.Orders.Remove(Order);
    //                    context.SaveChanges();
    //                    return true;
    //                }
    //                return false;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public Order getOrderById(int id)
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                Order Order = context.Orders.FirstOrDefault(o => o.Id == id);
    //                if (Order != null)
    //                {
    //                    return Order;
    //                }
    //                return null;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public bool EditOrder(Order Order)
    //    {
    //        using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
    //        {
    //            try
    //            {
    //                Order _Order = context.Orders.FirstOrDefault(o => o.Id == Order.Id);
    //                if (_Order != null)
    //                {
    //                    _Order.Name = Order.Name;
    //                    _Order.UnitPrice = Order.UnitPrice;
    //                    _Order.UnitsInStock = Order.UnitsInStock;
    //                    _Order.Image = Order.Image;
    //                    _Order.CategoryId = Order.CategoryId;

    //                    context.SaveChanges();
    //                    return true;
    //                }
    //                return false;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }
    }
}
