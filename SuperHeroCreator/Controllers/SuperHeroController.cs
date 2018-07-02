using SuperHeroCreator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperHeroCreator.Controllers
{
    public class SuperHeroController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: SuperHero
        public ActionResult Index()
        {
            return View();
        }

        // Create GET
        public ActionResult Create()
        {
            return View();
        }

        //CREATE POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="ID,HeroName,AlterEgo,PrimaryAblility,SecondaryAbility,Catchphrase")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                db.Heroes.Add(hero);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.personID = new SelectList(db.Heroes, "ID", "HeroName", "AlterEgo", "PrimaryAbility", "Catchphrase", hero);
            return View(hero);
            //Hero superman = new Hero();
            //add superhero to db here with LINQ
        }
    }
}