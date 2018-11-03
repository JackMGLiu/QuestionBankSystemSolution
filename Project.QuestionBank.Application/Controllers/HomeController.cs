using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.QuestionBank.Utils.Helper;

namespace Project.QuestionBank.Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string title = ConfigHelper.GetConfig("SystemName");
            string shorTitle = ConfigHelper.GetConfig("ShortName");
            ViewBag.SystemName = title;
            ViewBag.ShortName = shorTitle;
            return View();
        }

        public ActionResult Main()
        {
            string title = ConfigHelper.GetConfig("SystemName");
            ViewBag.SystemName = title;
            return View();
        }

        public ActionResult Navs()
        {
            return Json("");
        }
    }
}