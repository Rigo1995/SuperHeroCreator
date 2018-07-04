using SuperHeroCreator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Create([Bind(Include = "ID,HeroName,AlterEgo,PrimaryAbility,SecondaryAbility,Catchphrase")] Heroes heroes)
        {
            if (ModelState.IsValid)
            {
                db.Heroes.Add(heroes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(heroes);
            
        }
        //get
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heroes heroes = db.Heroes.Find(id);
            if (heroes == null)
            {
                return HttpNotFound();
            }

            //LINQ querty that selects the id I passed in the parameter
            // pass that query to the view ()
            return View(heroes);
        }

        //EDIT POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HeroName,AlterEgo,PrimaryAbility,SecondaryAbility,Catchphrase")] Heroes heroes)
        {
           if (ModelState.IsValid)
            {
                db.Entry(heroes).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");            
        }

        //get 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heroes heroes = db.Heroes.Find(id);
            if (heroes == null)
            {
                return HttpNotFound();
            }
            return View(heroes);
        }

        //DELETE post action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Heroes heroes = db.Heroes.Find(id);
                db.Heroes.Remove(heroes);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}