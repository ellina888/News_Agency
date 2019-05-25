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
    public class PagesController : Controller
    {
        private IPage p ;
        private IPageGroup pg;

        public PagesController()
        {
            p = new PageRepository(new News_Agency_Entities());
            pg= new PageGroupRepository(new News_Agency_Entities());
        }

        // GET: AdminPanel/Pages
        public ActionResult Index()
        {
            var pages = p.GetAllPage();
            return View(pages);
        }

        // GET: AdminPanel/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = p.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: AdminPanel/Pages/Create

        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: AdminPanel/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,PageTitle,ImagePath,ShortDescription,Text,Visitor,ShowingInSlider,CreateTime,Author")] Page page, HttpPostedFileBase imgInp)
        {
            if (imgInp == null || imgInp.ContentLength <= 0)
            {
                this.ModelState.AddModelError("ImagePath", "فیلد تصویر خبر اجباری است.");
            }
            else if (imgInp != null || imgInp.ContentLength > 0 && ModelState.IsValid)
            {
                var fileName = FileUpload(imgInp, page.GroupID);
                page.ImagePath = fileName;
                page.CreateTime = DateTime.Now;
                page.Visitor = 0;

                p.InsertPageGroup(page);
                p.SavePage();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: AdminPanel/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = p.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: AdminPanel/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,PageTitle,ImagePath,ShortDescription,Text,Visitor,ShowingInSlider,CreateTime,Author")] Page page, HttpPostedFileBase imgInp)
        {
            if (ModelState.IsValid)
            {
                if (imgInp != null && imgInp.ContentLength > 0)
                {
                    var fileName = FileUpload(imgInp, page.GroupID);
                    page.ImagePath = fileName;
                }

                p.UpdatePageGroup(page);
                p.SavePage();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: AdminPanel/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = p.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: AdminPanel/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (System.IO.File.Exists(Server.MapPath(p.GetPageById(id).PageTitle)))
                System.IO.File.Delete(Server.MapPath(p.GetPageById(id).PageTitle));

            p.DeletePageGroup(id);
            p.SavePage();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                p.Dispose();
            }
            base.Dispose(disposing);
        }

        /*********************************************************/
        /**************************Image**************************/
        /*********************************************************/
        [HttpGet]
        public ActionResult ShowImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = p.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }

            string path = page.ImagePath;

            return File(path, "image/jpeg");
        }

        public string FileUpload(HttpPostedFileBase file, int groupid)
        {
            string path = string.Empty;
            if (file != null)
            {
                string pageGroupTilte =pg.GetPageGroupById(groupid).GroupTitle;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                path = "~/Images/Pages/" + pageGroupTilte + "/" + fileName + extension;
                file.SaveAs(Server.MapPath(path));
            }
            return path;
        }
    }
}
