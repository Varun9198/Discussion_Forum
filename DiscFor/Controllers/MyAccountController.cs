using DiscFor.Context;
using DiscFor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DiscFor.Controllers
{
    public class MyAccountController : Controller
    {
        Model1 db;
        // GET: MyAccount
        public ActionResult Index()
        {
            db = new Model1();
            return View(db.Users.ToList());
            
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                db = new Model1();
                    var usr = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                    if (usr != null)
                    {
                        if (user.UserName == "admin")
                        {
                            ViewBag.Message = "That username is reserved for Admin";
                        }
                        else
                        {
                            ViewBag.Message = "User with same username already exists";
                        }
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        ViewBag.Message = user.UserName + " successfully registered.";
                    }
                
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            db = new Model1();
            var usr = db.Users.Where(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword).FirstOrDefault();
                if(usr != null)
                {
                    Session["UserName"] = usr.UserName.ToString();
                    Session["UserPassword"] = usr.UserPassword.ToString();
                    Session["UserId"] = usr.UserId.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect");
                }
            
            return View();
        }

        public ActionResult LoggedIn()
        {
            //if (Session["UserName"].Equals("admin") && Session["UserPassword"].Equals("admin"))
            //{
            //    return RedirectToAction("ForAdmin");
            //}
            if (Session["UserName"] != null)
            {
                db = new Model1();
                ViewBag.Questions = db.Questions.ToList();
                    ViewBag.Answers = db.Answers.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

       
        [HttpGet]
        public ActionResult addAnswer(int? id)
        {
            db = new Model1();
            Session["id"] = id.ToString();
                return View();
        }

        [HttpPost]
        public ActionResult addAnswer(Answer ans)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db = new Model1();
                    int temp_id = Int32.Parse(Session["id"].ToString());
                        var ques = db.Questions.Where(u => u.QuestionId == temp_id).FirstOrDefault();
                        string name = Session["UserName"].ToString();
                        string pass = Session["UserPassword"].ToString(); 
                        var usr = db.Users.Where(u => u.UserName == name && u.UserPassword == pass).FirstOrDefault();

                        ans.CurrentUser = usr;
                        ans.CurrentQuestion = ques;
                        db.Answers.Add(ans);
                        db.SaveChanges();
                        return RedirectToAction("LoggedIn");
                
                }
                return View(ans);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult addQuestion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addQuestion(Question q)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                db = new Model1();
                string usrnm = Session["UserName"].ToString(), pass = Session["UserPassword"].ToString();
                    var usr = db.Users.Where(u => u.UserName.Equals(usrnm)
                                            && u.UserPassword.Equals(pass)).FirstOrDefault();

                    if(usr == null || q == null)
                    {
                        return RedirectToAction("LogIn");
                    }
                    string s = usr.UserId.ToString();

                    q.User1 = usr;
                    

                    //q.User1.UserId = Int32.Parse(s);
                    db.Questions.Add(q);
                    db.SaveChanges();
                return RedirectToAction("LoggedIn");
            }
        }

        public ActionResult quesEdit(int? id)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();
            Question q = db.Questions.Find(id);
            return View(q);
        }

        [HttpPost]
        public ActionResult quesEdit(Question q)
        {
            db = new Model1();
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LoggedIn");
        }

        public ActionResult ansEdit(int? id)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();
            Answer a = db.Answers.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult ansEdit(Answer a)
        {
            db = new Model1();
            db.Entry(a).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LoggedIn");
        }


        public ActionResult quesDelete(int? id)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();
            Question q = db.Questions.Find(id);
            return View(q);
        }

        [HttpPost]
        public ActionResult quesDelete(int?id, Question q)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();

            foreach(var ans in db.Answers)
            {
                if (ans.CurrentQuestion == null) continue;
                if(ans.CurrentQuestion.QuestionId.ToString().Equals(q.QuestionId.ToString()))
                    db.Answers.Remove(ans);
            }
            db.Questions.Remove(db.Questions.Find(id));
            db.SaveChanges();
            return RedirectToAction("LoggedIn");
        }

        public ActionResult ansDelete(int? id)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();
            Answer a = db.Answers.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult ansDelete(int? id, Answer a)
        {
            if (id == null)
                return RedirectToAction("LoggedIn");

            db = new Model1();

            db.Answers.Remove(db.Answers.Find(id));
            db.SaveChanges();
            return RedirectToAction("LoggedIn");
        }



        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "HomePage");
        }

        public ActionResult TimePass()
        {
            return View();
        }

    }
}