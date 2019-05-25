using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public interface IPageGroup:IDisposable
    {
        IEnumerable<PageGroup> GetAllPageGroup();
        PageGroup GetPageGroupById(int pageGroupId);
        void InsertPageGroup(PageGroup pageGroup);
        void UpdatePageGroup(PageGroup pageGroup);
        void DeletePageGroup(PageGroup pageGroup);
        void DeletePageGroup(int pageGroupId);
        void SavePageGroup();
    }
}
