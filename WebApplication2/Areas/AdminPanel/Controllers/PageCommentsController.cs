using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAl;

namespace Pro_News_Agency.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class PageCommentsController : Controller
    {
        private IPageGroup pg;
        private IPage pa;
        private IPageComment pc;

        public PageCommentsController()
        {
            pg= new PageGroupRepository(new News_Agency_Entities());
            pa = new PageRepository(new News_Agency_Entities());
            pc = new PageCommentRepository(new News_Agency_Entities());
        }

        // GET: AdminPanel/PageComments
        public ActionResult Index()
        {
            var pageComments = pc.GetAllPageComment();
            return View(pageComments.ToList());
        }

        // GET: AdminPanel/PageComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageComment pageComment = pc.GetPageCommentById(id.Value);
            if (pageComment == null)
            {
                return HttpNotFound();
            }
            return View(pageComment);
        }

        // GET: AdminPanel/PageComments/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle",1);
            ViewBag.PageID = new SelectList(pa.GetPageByGroupId(1), "PageID", "PageTitle");
            return View();
        }

        // POST: AdminPanel/PageComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,PageID,Name,Email,Website,Comment,CreateDate")] PageComment pageComment)
        {
            if (ModelState.IsValid)
            {
                pageComment.CreateDate = DateTime.Now;
                pc.InsertPageComment(pageComment);
                pc.SavePageComment();
                return RedirectToAction("Single_page", "Home", new { area="",id = pageComment.PageID });
            }
            int groupId = pa.GetPageById(pageComment.PageID).GroupID;
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", groupId);
            ViewBag.PageID = new SelectList(pa.GetPageByGroupId(groupId), "PageID", "PageTitle",pageComment.PageID);
            //if (status == true)
            //    return RedirectToAction("Single_page", "Home", new { area = "", id = pageComment.PageID });
            //else
                return View(pageComment);
        }

        // GET: AdminPanel/PageComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageComment pageComment = pc.GetPageCommentById(id.Value);
            if (pageComment == null)
            {
                return HttpNotFound();
            }
            int groupId = pa.GetPageById(pageComment.PageID).GroupID;
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", groupId);
            ViewBag.PageID = new SelectList(pa.GetAllPage(), "PageID", "PageTitle", pageComment.PageID);
            return View(pageComment);
        }

        // POST: AdminPanel/PageComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,PageID,Name,Email,Website,Comment,CreateDate")] PageComment pageComment)
        {
            if (ModelState.IsValid)
            {
                pc.UpdatePageComment(pageComment);
                pc.SavePageComment();
                return RedirectToAction("Index");
            }
            int groupId = pa.GetPageById(pageComment.PageID).GroupID;
            ViewBag.GroupID = new SelectList(pg.GetAllPageGroup(), "GroupID", "GroupTitle", groupId);
            ViewBag.PageID = new SelectList(pa.GetAllPage(), "PageID", "PageTitle", pageComment.PageID);
            return View(pageComment);
        }

        // GET: AdminPanel/PageComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageComment pageComment = pc.GetPageCommentById(id.Value);
            if (pageComment == null)
            {
                return HttpNotFound();
            }
            return View(pageComment);
        }

        // POST: AdminPanel/PageComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pc.DeletePageComment(id);
            pc.SavePageComment();
            return RedirectToAction("Index");
        }

        public ActionResult SelectPage(int id)
        {
            var categoris = pa.GetPageByGroupId(id).Select(p => new { p.PageID,p.PageTitle});
            return Json(categoris, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
