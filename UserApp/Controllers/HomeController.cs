using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserApp.Models;

namespace UserApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(RegisterViewModel model)
        {
            if (Session["UserID"] == null)
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
            else
            {
                return RedirectToAction("UserDashBoard");
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

        public ActionResult Logoff()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                using (EFContext db = new EFContext())
                {
                    var obj = db.Products.Select(t => new ProductViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        VideoCard = t.VideoCard,
                        Screen = t.Screen,
                        RAM = t.RAM,
                        Processor = t.Processor,
                        HDD = t.HDD,
                        Photo = t.Photo
                    }).ToList();

                    return View(obj);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }




        [HttpPost]
        public ActionResult Details(ProductViewModel product)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {
                    Product p = context.Products.Find(product.Id);
                    return View(p);
                }
                //return RedirectToAction("UserDashBoard");
            }

            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult Details(int id)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {

                    var obj = context.Products.Find(id);


                    ProductViewModel p = new ProductViewModel()
                    {
                        Id = obj.Id,
                        Name = obj.Name,
                        Description = obj.Description,
                        VideoCard = obj.VideoCard,
                        Screen = obj.Screen,
                        RAM = obj.RAM,
                        Processor = obj.Processor,
                        HDD = obj.HDD,
                        Photo = obj.Photo


                    };
                    return View(p);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create(ProductViewModel model)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {

                    EFContext context = new EFContext();

                    Product product = new Product()
                    {
                        Name = model.Name,
                        Photo = model.Photo,
                        Description = model.Description,
                        HDD = model.HDD,
                        Processor = model.Processor,
                        RAM = model.RAM,
                        Screen = model.Screen,
                        VideoCard = model.VideoCard

                    };
                    context.Products.Add(product);
                    context.SaveChanges();

                    ViewBag.IsSuccessed = true;
                    return View(new ProductViewModel());
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {

                    var obj = context.Products.Find(id);

                    ProductViewModel p = new ProductViewModel()
                    {
                        Id = obj.Id,
                        Name = obj.Name,
                        Description = obj.Description,
                        VideoCard = obj.VideoCard,
                        Screen = obj.Screen,
                        RAM = obj.RAM,
                        Processor = obj.Processor,
                        HDD = obj.HDD,
                        Photo = obj.Photo

                    };
                    return View(p);
                }


            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {
                    var obj = context.Products.Find(product.Id);
                    obj.Name = product.Name;
                    obj.HDD = product.HDD;
                    obj.Photo = product.Photo;
                    obj.Processor = obj.Processor;
                    obj.Screen = product.Screen;
                    obj.RAM = product.RAM;
                    obj.Description = product.Description;
                    obj.VideoCard = product.VideoCard;
                    context.SaveChanges();
                }
                return RedirectToAction("UserDashBoard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Delete(int id)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {

                    var obj = context.Products.Find(id);

                    ProductViewModel p = new ProductViewModel()
                    {
                        Id = obj.Id,
                        Name = obj.Name,
                        Description = obj.Description,
                        VideoCard = obj.VideoCard,
                        Screen = obj.Screen,
                        RAM = obj.RAM,
                        Processor = obj.Processor,
                        HDD = obj.HDD,
                        Photo = obj.Photo
                    };
                    return View(p);
                }
            }

            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public ActionResult Delete(ProductViewModel product)
        {
            if (Session["UserID"] != null)
            {
                using (EFContext context = new EFContext())
                {
                    var obj = context.Products.Find(product.Id);
                    context.Products.Remove(obj);
                    context.SaveChanges();
                }
                return RedirectToAction("UserDashBoard");

            }

            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult CardView()
        {
            if (Session["UserID"] != null)
            {
                using (EFContext db = new EFContext())
                {
                    var obj = db.Products.Select(t => new ProductViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        VideoCard = t.VideoCard,
                        Screen = t.Screen,
                        RAM = t.RAM,
                        Processor = t.Processor,
                        HDD = t.HDD,
                        Photo = t.Photo
                    }).ToList();

                    return View(obj);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Search(string searching)
        {

            using (EFContext db = new EFContext())
            {
                //var obj = db.Products.Select(t => new ProductViewModel()
                //{
                //    Id = t.Id,
                //    Name = t.Name,
                //    Description = t.Description,
                //    VideoCard = t.VideoCard,
                //    Screen = t.Screen,
                //    RAM = t.RAM,
                //    Processor = t.Processor,
                //    HDD = t.HDD,
                //    Photo = t.Photo
                //}).ToList();
                var obj = db.Products.Where(p => p.Name.StartsWith(searching) || searching == null).ToList();

                var list = obj.Select(t => new ProductViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    VideoCard = t.VideoCard,
                    Screen = t.Screen,
                    RAM = t.RAM,
                    Processor = t.Processor,
                    HDD = t.HDD,
                    Photo = t.Photo
                }).ToList();

                return View(list);
            }
        }

    }
}






