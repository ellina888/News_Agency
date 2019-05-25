using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAl
{
    public class AccountRepository : IAccount
    {
        News_Agency_Entities db;
        public AccountRepository(News_Agency_Entities context)
        {
            db = context;
        }

        public IEnumerable<Account> GetAllAccount()
        {
            return db.Accounts;
        }
        public Account AccountById(int accountId)
        {
            return db.Accounts.Find(accountId);
        }

        public void InsertAccount(Account account)
        {
            db.Accounts.Add(account);
        }

        public void UpdateAccount(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
        }

        public void DeleteAccount(int accountId)
        {
            db.Entry(AccountById(accountId)).State = EntityState.Modified;
        }

        public void DeleteAccount(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
        }

        public void SaveAccount()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
