using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAl;
using System.IO;

namespace Pro_News_Agency.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class PageGroupsController : Controller
    {
        private IPageGroup pg;
        private IPage pa;

        public PageGroupsController()
        {
            pg = new PageGroupRepository(new News_Agency_Entities());
            pa = new PageRepository(new News_Agency_Entities());
        }

        // GET: AdminPanel/PageGroups
        public ActionResult Index()
        {
            return View(pg.GetAllPageGroup());
        }

        // GET: AdminPanel/PageGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: AdminPanel/PageGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                if (!pg.GetAllPageGroup().Any(m => m.GroupTitle == pageGroup.GroupTitle))
                {
                    pg.InsertPageGroup(pageGroup);
                    pg.SavePageGroup();

                    if (!Directory.Exists(Server.MapPath("~/Images/Pages/" + pageGroup.GroupTitle)))
                        Directory.CreateDirectory(Server.MapPath("~/Images/Pages/" + pageGroup.GroupTitle));

                    return RedirectToAction("Index");
                }
                else
                    this.ModelState.AddModelError("GroupTitle", "گروه خبری " + pageGroup.GroupTitle + " قبلا ایجاد شده است.");
            }

            return PartialView(pageGroup);
        }

        // GET: AdminPanel/PageGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = pg.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }

            return PartialView(pageGroup);
        }

        // POST: AdminPanel/PageGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                string title = new News_Agency_Entities().PageGroups.AsNoTracking().FirstOrDefault(p => p.GroupID == pageGroup.GroupID).GroupTitle;
                if (Directory.Exists(Server.MapPath("~/Images/Pages/" + title)) && title != pageGroup.GroupTitle)
                    Directory.Move(Server.MapPath("~/Images/Pages/" + title), Server.MapPath("~/Images/Pages/" + pageGroup.GroupTitle));

                pg.UpdatePageGroup(pageGroup);
                pg.SavePageGroup();

                if (pa.GetAllPage().Any(p => p.GroupID == pageGroup.GroupID))
                {
                    pa.UpdatePageImagePath(pageGroup.GroupID, "~/Images/Pages/" + pageGroup.GroupTitle + "/");
                    pa.SavePage();
                }
                return RedirectToAction("Index");
            }
            return PartialView(pageGroup);
        }

        // GET: AdminPanel/PageGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = pg.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: AdminPanel/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PageGroup pageGroup = pg.GetPageGroupById(id);

            if (!pa.GetPageByGroupId(id).Any())
            {
                if (Directory.Exists(Server.MapPath("/Images/Pages/" + pg.GetPageGroupById(id).GroupTitle)))
                    Directory.Delete(Server.MapPath("/Images/Pages/" + pg.GetPageGroupById(id).GroupTitle));

                pg.DeletePageGroup(id);
                pg.SavePageGroup();
                return RedirectToAction("Index");
            }
            else
                this.ModelState.AddModelError("", "امکان حذف گروه خبری " + pg.GetPageGroupById(id).GroupTitle + " وجود ندارد.");
            return PartialView(pageGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pg.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
