using EhkBackend.BLL;
using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EhkBackend.ui.Controllers
{
    public class AdminController : Controller
    {
        IAdminService adminService = new AdminService();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login1()
        {
            string admin_id = Request["name"];
            string pwd = Request["password"];
            Admins a = new Admins();
            a.admin_id = admin_id;
            a.admin_pwd = pwd;
           var login12=adminService.checkLogin(a);
            if (login12 != null)
            {
                return Redirect("/Home/index");

            }
            else {
                return Content("error");
            }
            return View();
        }
    }
}