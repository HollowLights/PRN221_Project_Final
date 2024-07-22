using Library.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Library.Management
{
    public class TableManagement
    {
        private static TableManagement instance { get; set; }
        private static readonly object instanceLock = new object();

        public TableManagement() { }

        public static TableManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TableManagement();
                    }
                    return instance;
                }
            }
        }

        public List<Table> getListTable()
        {
            List<Table> list = new List<Table>();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    list = context.Tables.ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void setTableActive(int tableId, bool active)
        {
            List<Table> list = new List<Table>();
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Table _table = context.Tables.FirstOrDefault(o => o.Id == tableId);
                    if (_table != null)
                    {
                        _table.IsOn = active;
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Table getTableById(int tableId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    return context.Tables.Include(t => t.Type).FirstOrDefault(t => t.Id == tableId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsPreOrder(int tableId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    return context.PreOrders.Any(c => c.TableId == tableId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<dynamic> getPreOrderListByTableId(int tableId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    var list = context.PreOrders.Include(a => a.Account).Where(t => t.TableId == tableId).Select(e => new
                    {
                        Id = e.Id,
                        TableId = e.TableId,
                        OrderDate = e.OrderDate.ToString("dd/MM/yyyy"),
                        OrderById = e.AccountId,
                        Customer = e.Customer,
                        OrderBy = e.Account.FullName,
                    });
                    return list.Cast<dynamic>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool checkPreOrder(int tableId, DateTime orderDate)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                // Check if there is a pre-order for the tableId on the specific orderDate
                bool existsOnOrderDate = context.PreOrders.Any(t => t.TableId == tableId && t.OrderDate.Date == orderDate.Date);

                // Check if there are any pre-orders older than the current date for the tableId
                bool hasPastPreOrders = context.PreOrders.Any(t => t.TableId == tableId && t.OrderDate.Date < DateTime.Now.Date);

                // Return true if either condition is met
                return existsOnOrderDate || hasPastPreOrders;
            }
        }

        public bool createPreOrder(int tableId, PreOrder preOrder)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    context.PreOrders.Add(preOrder);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool deletePreOrder(int preOrderId)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    PreOrder preOrder = context.PreOrders.FirstOrDefault(o => o.Id == preOrderId);
                    if (preOrder != null)
                    {
                        context.PreOrders.Remove(preOrder);
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

        public bool deletePreOrder(int tableId, DateTime orderDate)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    PreOrder preOrder = context.PreOrders.FirstOrDefault(o => o.TableId == tableId && o.OrderDate.Date == o.OrderDate.Date);
                    if (preOrder != null)
                    {
                        context.PreOrders.Remove(preOrder);
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


        /*        internal bool checkPreOrderOfTable(int tableId)
                {
                    using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
                    {
                        try
                        {
                            bool validDate = true;
                            var list = context.PreOrders.Include(a => a.Account).Where(t => t.TableId == tableId).ToList();
                            foreach(var o in list)
                            {
                                if(chec)
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }*/
    }
}
