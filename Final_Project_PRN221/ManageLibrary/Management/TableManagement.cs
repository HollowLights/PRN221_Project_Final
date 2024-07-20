using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if(instance == null)
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
            using(FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    list = context.Tables.ToList();
                    return list;
                }catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
