using Library.DataAccess;
using Library.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public class AccountRepository:IAccountRepository
    {
        public Account GetAccountByUser(string user)
            => AccountManagement.Instance.GetAccountByUser(user);
        public bool ChangePassword(Account account)
            => AccountManagement.Instance.ChangePassword(account);
        public List<Account> getEmployeeAccount()
            => AccountManagement.Instance.getEmployeeAccount();
        public Account getAccountById(int id)
            => AccountManagement.Instance.getAccountById(id);
        public bool setAccountRole(int id, bool role)
            => AccountManagement.Instance.setAccountRole(id, role);
        public bool addEmployeeAccount(Account account)
            => AccountManagement.Instance.addEmployeeAccount(account);
        public bool removeEmployeeAccount(int id)
            => AccountManagement.Instance.removeEmployeeAccount(id);
        public bool updateEmployeeAccount(Account account)
            => AccountManagement.Instance.updateEmployeeAccount(account);
    }
}
