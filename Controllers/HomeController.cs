using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginSignupMVC.Context;
using LoginSignupMVC.Models;
using LoginSignupMVC.ViewModels;

namespace LoginSignupMVC.Controllers
{
    public class HomeController : Controller
    {
        AuthContext _db = new AuthContext();

        public ActionResult Index()
        {
            
            return View(_db.RegsDbSet.ToList());
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            { 
                var data = _db.RegsDbSet.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["access_token"] = data.FirstOrDefault().idUser;
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                   
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    ViewBag.TheResult = false;
                    return View();
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        public ActionResult SignUp()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Register reg)
        {
            if (ModelState.IsValid)
            {
                var check = _db.RegsDbSet.FirstOrDefault(s => s.Email == reg.Email);
                if (check == null)
                {
                    
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.RegsDbSet.Add(reg);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                   
                    ViewBag.TheResult = false;
                    ViewBag.error = "Account already exists";
                    return View();
                }


            }
            return View();



        }
    }
}