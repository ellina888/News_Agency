using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{ 
    public interface IPage:IDisposable       
    {
        IEnumerable<Page> GetAllPage();
        Page GetPageById(int pageId);
        IEnumerable<Page> GetPageByGroupId(int groupId);
        Page Next_Page(int pageId);
        void InsertPageGroup(Page page);
        void UpdatePageGroup(Page page);
        void UpdatePageImagePath(int groupId, string path);
        void DeletePageGroup(Page page);
        void DeletePageGroup(int pageId);
        void SavePage();
    }
}
