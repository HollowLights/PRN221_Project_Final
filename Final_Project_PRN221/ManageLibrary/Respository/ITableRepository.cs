using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public interface ITableRepository
    {
        public List<Table> getListTable();
    }
}
