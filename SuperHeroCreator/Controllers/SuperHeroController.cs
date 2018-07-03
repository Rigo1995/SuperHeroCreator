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
            var heroes = db.Heroes.ToList();
            return View(heroes);
        }

        // Create GET
        public ActionResult Create()
        {
            return View();
        }

        //CREATE POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="ID,HeroName,AlterEgo,PrimaryAblility,SecondaryAbility,Catchphrase")] Heroes heroes)
        {
            if (ModelState.IsValid)
            {
                db.Heroes.Add(heroes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(heroes);
            //ViewBag.HeroName = new SelectList(db.Heroes, "ID", "HeroName", "AlterEgo", "PrimaryAbility", "Catchphrase", heroes.HeroName);

            //Hero superman = new Hero();
            //add superhero to db here with LINQ
        }

        ////EDIT POST Action
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Include = "ID,HeroName,AlterEgo,PrimaryAblility,SecondaryAbility,Catchphrase")
        //{
        //    if ()
        //}
    }
}