using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LessonProject.Global.Auth;
using LessonProject.Model;
using Ninject;

namespace LessonProject.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        [Inject]
        public IRepository Repository { get; set; }


        [Inject]
        public IAuthentication Auth { get; set; }
        public User CurrentUser
        {
            get
            {
                return ((UserIdentity)Auth.CurrentUser.Identity).User;
            }
        }
      }
}