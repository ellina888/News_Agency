using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public interface ILogin:IDisposable
    {
        bool CheckValidUser(string Username,string Password,out string ErrorMessage, out string ErrorMessageFor);
    }
}
