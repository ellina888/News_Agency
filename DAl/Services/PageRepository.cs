using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class PageRepository : IPage
    {
        News_Agency_Entities db ;

        public PageRepository(News_Agency_Entities context)
        {
            db = context;
        }
        public IEnumerable<Page> GetAllPage()
        {
            return db.Pages.Include(p => p.PageGroup).OrderByDescending(p => p.CreateTime);
        }

        public Page GetPageById(int pageId)
        {
            return db.Pages.Find(pageId);
        }

        public IEnumerable<Page> GetPageByGroupId(int groupId)
        {
            return db.Pages.Where(p => p.GroupID==groupId).Include(p => p.PageGroup);
        }

        public Page Next_Page(int pageId)
        {

            return db.Pages.OrderByDescending(p => p.CreateTime).SkipWhile(p => p.PageID == pageId).First();
        }

        public void InsertPageGroup(Page page)
        {
            db.Pages.Add(page);
        }
        public void UpdatePageGroup(Page page)
        {
            db.Entry(page).State = EntityState.Modified;
        }

        public void UpdatePageImagePath(int groupId, string path)
        {
            if(GetPageByGroupId(groupId).Any())
            {
                IEnumerable<Page> page = GetPageByGroupId(groupId);
                foreach (var item in page)
                {
                    string filename = Path.GetFileName(item.ImagePath);
                    item.ImagePath = path + filename;
                    db.Entry(item).Property(p => p.ImagePath).IsModified = true;
                }
            }
        }

        public void DeletePageGroup(Page page)
        {
            db.Entry(page).State = EntityState.Deleted;
        }

        public void DeletePageGroup(int pageId)
        {
            DeletePageGroup(GetPageById(pageId));
        }

        public void SavePage()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
