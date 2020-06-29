using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LessonProject.Controllers;

namespace LessonProject.Areas.Default.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            var roles = Repository.Roles.ToList();
            return View(roles);
        }
    }
}