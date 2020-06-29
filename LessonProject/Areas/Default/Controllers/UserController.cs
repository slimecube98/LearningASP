using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LessonProject.Model;
using LessonProject.Controllers;
using LessonProject.Models.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;
using LessonProject.Tools;
using System.Drawing.Imaging;

namespace LessonProject.Areas.Default.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var users = Repository.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var newUserView = new UserView();
            return View(newUserView);
        }

        [HttpPost]
        public ActionResult Register(UserView userView)
        {
            if (userView.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            }
            var anyUser = Repository.Users.Any(p => string.Compare(p.Email, userView.Email) == 0);
            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже зарегестрирован");
            }
            
            
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<UserView, User>()
                .ForMember(dest=>dest.Berthdate, opt => opt.MapFrom(src=> new DateTime(src.BirthdateYear, src.BirthdateMonth,src.BirthdateDay))));
                var mapper = new Mapper(config);
                var user = (User)(mapper.Map(userView, typeof(UserView), typeof(User)));
                Repository.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(userView);
        }

        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Arial");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
            return null;
        }
    }
}