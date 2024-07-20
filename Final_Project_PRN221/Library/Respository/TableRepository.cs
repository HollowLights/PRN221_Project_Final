using Library.DataAccess;
using Library.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public class TableRepository : ITableRepository
    {
        public List<Table> getListTable()=>TableManagement.Instance.getListTable();
        public void setTableActive(int tableId, bool active)
            => TableManagement.Instance.setTableActive(tableId, active);
    }
}
