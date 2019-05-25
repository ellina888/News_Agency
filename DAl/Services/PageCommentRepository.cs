using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class PageCommentRepository : IPageComment
    {
        News_Agency_Entities db ;

        public PageCommentRepository(News_Agency_Entities context)
        {
            db = context;
        }
        public IEnumerable<PageComment> GetAllPageComment()
        {
            return db.PageComments.Include(p => p.Page);
        }

        public IEnumerable<PageComment> GetAllPageCommentByPageId(int pageId)
        {
            return GetAllPageComment().Where(p => p.PageID == pageId).OrderByDescending(p =>p.CreateDate);
        }

        public PageComment GetPageCommentById(int PageCommentId)
        {
            return db.PageComments.Find(PageCommentId);
        }

        public void InsertPageComment(PageComment PageComment)
        {
            db.PageComments.Add(PageComment);
        }

        public void UpdatePageComment(PageComment PageComment)
        {
            db.Entry(PageComment).State =EntityState.Modified;
        }
        public void DeletePageComment(PageComment PageComment)
        {
            db.Entry(PageComment).State = EntityState.Deleted;
        }
        public void DeletePageComment(int PageCommentId)
        {
            DeletePageComment(GetPageCommentById(PageCommentId));
        }

        public void SavePageComment()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
