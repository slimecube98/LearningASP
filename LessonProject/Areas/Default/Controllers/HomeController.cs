using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LessonProject.Model;
using LessonProject.Controllers;
using System.Security.Principal;

namespace LessonProject.Areas.Default.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult UserLogin()
        {
            return View(CurrentUser);
        }
    }
}