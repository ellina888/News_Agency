using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public interface IAccount:IDisposable
    {
        IEnumerable<Account> GetAllAccount();
        Account AccountById(int accountId);
        void InsertAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        void DeleteAccount(int accountId);
        void SaveAccount();
    }
}
