using DAl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pro_News_Agency.Controllers
{
    public class HomeController : Controller
    {
        private IPage p;
        private IPageGroup pg;
        private IPageComment pc;
        private ILogin lg;

        public HomeController()
        {
            p = new PageRepository(new News_Agency_Entities());
            pg = new PageGroupRepository(new News_Agency_Entities());
            pc = new PageCommentRepository(new News_Agency_Entities());
            lg = new LoginRepository(new News_Agency_Entities());
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(p.GetAllPage());
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login,string returnUrl = "/")
        {
            string message = "";
            string messageFor = "";
            if (ModelState.IsValid)
            {
                if (lg.CheckValidUser(login.Username, login.Password,out message,out messageFor))
                {
                    FormsAuthentication.SetAuthCookie(login.Username, true);
                    return Redirect(returnUrl);
                }
                else
                    ModelState.AddModelError(messageFor,message);
            }
            return View();
        }

        public ActionResult Single_page(int? id)
        {
            p.GetPageById(id.Value).Visitor += 1;
            p.SavePage();
            return View(p.GetPageById(id.Value));
        }

        [ChildActionOnly]
        public ActionResult Popular_Pages()
        {
            var page = p.GetAllPage().OrderByDescending(p => p.Visitor).Take(4);
            return PartialView(page.ToList());
        }

        [ChildActionOnly]
        public ActionResult Last_Pages(string partialName, int count)
        {
            var page = p.GetAllPage().OrderByDescending(p => p.CreateTime).Take(count);
            return PartialView(partialName, page.ToList());
        }

        [ChildActionOnly]
        public ActionResult PageGroups(string partialName)
        {
            return PartialView(partialName, pg.GetAllPageGroup());
        }

        [ChildActionOnly]
        public ActionResult PagesByGroupId(int groupId, string partialName, int? count)
        {
            var pages =p.GetAllPage().Where(p => p.GroupID == groupId).OrderByDescending(p => p.CreateTime).ToList();
            if (count == null)
                return PartialView(partialName, pages);
            else
                return PartialView(partialName, pages.Take(count.Value));
        }

        [ChildActionOnly]
        public ActionResult Video_Tab()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Comment_Tab(int id)
        {
            return PartialView(pc.GetAllPageCommentByPageId(id));
        }

        [ChildActionOnly]
        public ActionResult Navigate_Pags(int id)
        {
            return PartialView(p.Next_Page(id));
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}