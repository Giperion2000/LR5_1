using LR5_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LR5_1.Controllers
{
    public class AddPhoneController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: AddPhone
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            if (phone.Mark.Length > 5)
            {
                ModelState.AddModelError("Mark", "Неприпустима довжина");
            }
            if (ModelState.IsValid)
            {

                context.Phones.Add(phone);
                context.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(phone);
        }
    }
}