using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management
{
    public class AccountManagement
    {
        private static AccountManagement instance;
        private static readonly object instanceLock = new object();

        public AccountManagement() { }
        public static AccountManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountManagement();
                    }
                    return instance;
                }
            }
        }

        public Account GetAccountByUser(string user)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account account = context.Accounts.FirstOrDefault(o => o.User.Equals(user));
                    return account;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool ChangePassword(Account account)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account _account = context.Accounts.FirstOrDefault(o => o.Id == account.Id);
                    if (_account != null)
                    {
                        _account.Password = account.Password;
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return false;
            }
        }

        public List<Account> getEmployeeAccount()
        {
            List<Account> list = new List<Account>();
            using(FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    list = context.Accounts.Where(o=> o.Role != null).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return list;
        }

        public Account getAccountById(int id)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account account = context.Accounts.FirstOrDefault(o=>o.Id == id);
                    return account;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool setAccountRole(int id, bool role)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account account = context.Accounts.FirstOrDefault(o => o.Id == id);
                    if (account != null)
                    {
                        account.Role = role;
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool addEmployeeAccount(Account account)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool removeEmployeeAccount(int id)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account account = context.Accounts.FirstOrDefault(o => o.Id==id);
                    if (account != null)
                    {
                        context.Accounts.Remove(account);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool updateEmployeeAccount(Account account)
        {
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    Account _account = context.Accounts.FirstOrDefault(o => o.Id == account.Id);
                    if (_account != null)
                    {
                        _account.FullName = account.FullName;
                        _account.User = account.User;
                        _account.Password = account.Password;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
