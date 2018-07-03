using SuperHeroCreator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            
        }
        public ActionResult Edit(int id)
        {
            db.Heroes.ToList();
            return View();
        }

        //EDIT POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HeroName,AlterEgo,PrimaryAblility,SecondaryAbility,Catchphrase")] Heroes heroes)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    db.Entry(heroes).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

             catch(DataMisalignedException /*dex */)
            {
                ModelState.AddModelError("", "No!.");
            }
            return View(heroes);
        }
    }
}