using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public interface IAccountRepository
    {
        public Account GetAccountByUser(string user);
        public bool ChangePassword(Account account);
        public List<Account> getEmployeeAccount();
        public Account getAccountById(int id);
        public bool setAccountRole(int id, bool role);
        public bool addEmployeeAccount(Account account);
        public bool removeEmployeeAccount(int id);
        public bool updateEmployeeAccount(Account account);
    }
}
