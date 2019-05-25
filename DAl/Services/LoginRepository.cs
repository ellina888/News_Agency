using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class LoginRepository : ILogin
    {
        News_Agency_Entities db;
        public LoginRepository(News_Agency_Entities context)
        {
            db = context;
        }
        public bool CheckValidUser(string Username, string Password, out string ErrorMessage, out string ErrorMessageFor)
        {
            ErrorMessageFor = "Username";
            ErrorMessage = "کاربری با نام کاربری " + Username + " یافت نشد.";
            if (db.Accounts.Any(a => a.Username == Username && a.Password == Password))
                return true;
            else
            {
                if (db.Accounts.Any(a => a.Username == Username && a.Password != Password))
                {
                    ErrorMessageFor = "Password";
                    ErrorMessage = "کلمه عبور اشتباه است.";
                }
                return false;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
