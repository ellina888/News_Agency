using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class PageGroupRepository : IPageGroup
    {
        News_Agency_Entities db ;

        public PageGroupRepository(News_Agency_Entities context)
        {
            db = context;
        }
        public IEnumerable<PageGroup> GetAllPageGroup()
        {
            return db.PageGroups;
        }

        public PageGroup GetPageGroupById(int pageGroupId)
        {
            return db.PageGroups.Find(pageGroupId);
        }

        public void InsertPageGroup(PageGroup pageGroup)
        {
            db.PageGroups.Add(pageGroup);
        }

        public void UpdatePageGroup(PageGroup pageGroup)
        {
            db.Entry(pageGroup).State = EntityState.Modified;
        }

        public void DeletePageGroup(PageGroup pageGroup)
        {
            db.Entry(pageGroup).State = EntityState.Deleted;
        }

        public void DeletePageGroup(int pageGroupId)
        {
            DeletePageGroup(GetPageGroupById(pageGroupId));
        }

        public void SavePageGroup()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
