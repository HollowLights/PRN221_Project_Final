using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management
{
    public class ProductManagement
    {
        private static ProductManagement instance;
        private static readonly object instanceLock = new object();

        public ProductManagement() { }
        public static ProductManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductManagement();
                    }
                    return instance;
                }
            }
        }

        public List<dynamic> getProductList()
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Products.Select(
                        o => new
                        {
                            Id = o.Id,
                            Name = o.Name,
                            UnitPrice = o.UnitPrice,
                            UnitsInStock = o.UnitsInStock,
                            Image = "Images/" + o.Image,
                            CategoryId = o.CategoryId,
                            CategoryName = o.Category.Name
                        });

                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<dynamic> getProductByFilter
            (string textSearch,string category, string orderBy)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Products.Select(
                        o => new
                        {
                            Id = o.Id,
                            Name = o.Name,
                            UnitPrice = o.UnitPrice,
                            UnitsInStock = o.UnitsInStock,
                            Image = "Images/" + o.Image,
                            CategoryId = o.CategoryId,
                            CategoryName = o.Category.Name
                        });
                    if(textSearch.Trim() != "")
                    {
                        list = list.Where(o=> o.Name.ToLower().Contains(textSearch.ToLower())
                        || o.CategoryName.ToLower().Contains(textSearch.ToLower()));
                    }
                    if(category != "")
                    {
                        list = list.Where(o=>o.CategoryId.ToString().Equals(category));
                    }
                    if (orderBy != "")
                    {
                        if(orderBy == "1")
                        {
                            list = list.OrderBy(o => o.UnitPrice);
                        }
                        else if(orderBy == "2")
                        {
                            list = list.OrderByDescending(o => o.UnitPrice);
                        }else if(orderBy == "3")
                        {
                            list = list.OrderBy(o=>o.UnitsInStock);
                        }else if (orderBy == "4")
                        {
                            list = list.OrderByDescending(o => o.UnitsInStock);
                        }
                    }

                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool addProduct(Product product)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                   context.Products.Add(product);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool deleteProduct(int id)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Product product = context.Products.FirstOrDefault(o => o.Id == id);
                    if(product != null)
                    {
                        context.Products.Remove(product);
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

        public Product getProductById(int id)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Product product = context.Products.FirstOrDefault(o => o.Id == id);
                    if(product != null)
                    {
                        return product;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool EditProduct(Product product)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Product _product = context.Products.FirstOrDefault(o=> o.Id == product.Id);
                    if(_product != null)
                    {
                        _product.Name = product.Name;
                        _product.UnitPrice = product.UnitPrice;
                        _product.UnitsInStock = product.UnitsInStock;
                        _product.Image = product.Image;
                        _product.CategoryId = product.CategoryId;

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

        public List<dynamic> getProductListInOrder(int orderId)
        {
            ProductManagement manage = new ProductManagement();
            List<OrderDetail> orderDetailList = manage.getOrderDetailByOrderId(orderId);

            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Products.Select(o => new ProductListItem
                    {
                        Id = o.Id,
                        Name = o.Name,
                        UnitPrice = o.UnitPrice,
                        UnitsInStock = o.UnitsInStock,
                        Image = "Images/" + o.Image,
                        CategoryId = o.CategoryId,
                        CategoryName = o.Category.Name,
                        Quantity = manage.getQuantityInOrder(o.Id, orderDetailList)
                    });


                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private List<OrderDetail> getOrderDetailByOrderId(int orderId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    List<OrderDetail> orderDetailList = context.OrderDetails.
                        Where(o => o.OrderId == orderId).ToList();
                    return orderDetailList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private int getQuantityInOrder(int productId, List<OrderDetail> list)
        {
            int quantity = 0;
            foreach (OrderDetail item in list)
            {
                if(item.ProductId == productId)
                {
                    quantity = item.Quantity;
                }
            }

            return quantity;
        }

        public List<dynamic> getProductListItemByFilter
            (string textSearch, string category, string orderBy, int orderId)
        {
            ProductManagement manage = new ProductManagement();
            List<OrderDetail> orderDetailList = manage.getOrderDetailByOrderId(orderId);

            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.Products.Select(o => new ProductListItem
                    {
                        Id = o.Id,
                        Name = o.Name,
                        UnitPrice = o.UnitPrice,
                        UnitsInStock = o.UnitsInStock,
                        Image = "Images/" + o.Image,
                        CategoryId = o.CategoryId,
                        CategoryName = o.Category.Name,
                        Quantity = manage.getQuantityInOrder(o.Id, orderDetailList)
                    });
                    if (textSearch.Trim() != "")
                    {
                        list = list.Where(o => o.Name.ToLower().Contains(textSearch.ToLower())
                        || o.CategoryName.ToLower().Contains(textSearch.ToLower()));
                    }
                    if (category != "")
                    {
                        list = list.Where(o => o.CategoryId.ToString().Equals(category));
                    }
                    if (orderBy != "")
                    {
                        if (orderBy == "1")
                        {
                            list = list.OrderBy(o => o.UnitPrice);
                        }
                        else if (orderBy == "2")
                        {
                            list = list.OrderByDescending(o => o.UnitPrice);
                        }
                        else if (orderBy == "3")
                        {
                            list = list.OrderBy(o => o.UnitsInStock);
                        }
                        else if (orderBy == "4")
                        {
                            list = list.OrderByDescending(o => o.UnitsInStock);
                        }
                    }

                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
