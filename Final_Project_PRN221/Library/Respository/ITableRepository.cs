using Library.DataAccess;

namespace Library.Respository
{
    public interface ITableRepository
    {
        public List<Table> getListTable();
        public void setTableActive(int tableId, bool active);

        public Table getTableById(int tableId);

        public bool IsPreOrder(int tableId);

        public List<dynamic> getPreOrderListByTableId(int tableId);

        public bool createPreOrder(int tableId, PreOrder preOrder);

        public bool checkPreOrder(int tableId, DateTime orderDate);

        public bool deletePreOrder(int preOrderId);

        /*        public bool checkPreOrderOfTable(int tableId);
        */

        public bool deletePreOrder(int tableId, DateTime orderDate);
    }
}
