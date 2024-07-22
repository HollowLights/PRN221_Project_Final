using Library.DataAccess;
using Library.Management;
using Table = Library.DataAccess.Table;

namespace Library.Respository
{
    public class TableRepository : ITableRepository
    {
        public List<Table> getListTable() => TableManagement.Instance.getListTable();
        public void setTableActive(int tableId, bool active)
            => TableManagement.Instance.setTableActive(tableId, active);
        public Table getTableById(int tableId) => TableManagement.Instance.getTableById(tableId);

        public bool IsPreOrder(int tableId) => TableManagement.Instance.IsPreOrder(tableId);

        public List<dynamic> getPreOrderListByTableId(int tableId)
            => TableManagement.Instance.getPreOrderListByTableId(tableId);

        public bool createPreOrder(int tableId, PreOrder preOrder)
            => TableManagement.Instance.createPreOrder(tableId, preOrder);

        public bool checkPreOrder(int tableId, DateTime orderDate)
            => TableManagement.Instance.checkPreOrder(tableId, orderDate);

        public bool deletePreOrder(int preOrderId)
            => TableManagement.Instance.deletePreOrder(preOrderId);

        /*        public bool checkPreOrderOfTable(int tableId)
                    => TableManagement.Instance.checkPreOrderOfTable(tableId);*/

        public bool deletePreOrder(int tableId, DateTime orderDate)
            => TableManagement.Instance.deletePreOrder(tableId, orderDate);
    }
}
