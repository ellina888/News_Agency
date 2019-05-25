using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public interface IPageComment:IDisposable
    {
        IEnumerable<PageComment> GetAllPageComment();
        IEnumerable<PageComment> GetAllPageCommentByPageId(int PageId);
        PageComment GetPageCommentById(int PageCommentId);
        void InsertPageComment(PageComment PageComment);
        void UpdatePageComment(PageComment PageComment);
        void DeletePageComment(PageComment PageComment);
        void DeletePageComment(int PageCommentId);
        void SavePageComment();
    }
}
