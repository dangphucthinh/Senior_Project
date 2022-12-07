using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuleForgotPass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult ResetPasswordEmail(string userId="", string code="")
        {
            var model = new Models.UserForResetPasswordModel();
            model.userId = userId;
            model.code = code;
            model.Password = "";
            return PartialView(model);
        }
    }
}