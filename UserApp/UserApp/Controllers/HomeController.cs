using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserApp.Models;

namespace UserApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Register");
        }

        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (EFContext context = new EFContext())
                {
                    Address address = new Address()
                    {
                        Country = model.Country,
                        Apartement = model.Apartement,
                        City = model.City,
                        Street = model.Street,
                    };
                    User user = new User()
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Password = model.Password,
                        Phone = model.Phone,
                        DateOfBirth = model.DateOfBirth,
                        AddressOf = address
                    };

                    for (int i = 0; i < 5; i++)
                    {
                    Product product = new Product()
                    {
                        Name = "asp.net"
                    };
                        context.Products.Add(product);
                    }
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                ViewBag.IsSuccessed = true;
                return View(new RegisterViewModel());
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (EFContext db = new EFContext())
                {
                    var obj = db.Users.Where(a => a.Name.Equals(objUser.Name) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.Name.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
