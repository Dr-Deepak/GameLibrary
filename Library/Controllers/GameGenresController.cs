using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class GameGenresController : Controller
    {
        private DataContext db = new DataContext();

        // GET: GameGenres
        public ActionResult Index()
        {
            var gameGenres = db.GameGenres.Include(g => g.Game).Include(g => g.Genre);
            return View(gameGenres.ToList());
        }

        // GET: GameGenres/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameGenre gameGenre = db.GameGenres.Find(id);
            if (gameGenre == null)
            {
                return HttpNotFound();
            }
            return View(gameGenre);
        }

        // GET: GameGenres/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: GameGenres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,GenreId")] GameGenre model)
        {
            if (ModelState.IsValid)
            {
                GameGenre tmpgamegenre = db.GameGenres
                                    .SingleOrDefault(x => x.GameId == model.GameId && x.GenreId == model.GenreId);
                if(tmpgamegenre == null)
                {
                    model.GameGenreId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.EditDate = model.CreateDate;

                    db.GameGenres.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate key found!");
                }
            }

            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", model.GameId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", model.GenreId);
            return View(model);
        }

        // GET: GameGenres/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameGenre gameGenre = db.GameGenres.Find(id);
            if (gameGenre == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", gameGenre.GameId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", gameGenre.GenreId);
            return View(gameGenre);
        }

        // POST: GameGenres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameGenreId,CreateDate,EditDate,GameId,GenreId")] GameGenre gameGenre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameGenre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", gameGenre.GameId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", gameGenre.GenreId);
            return View(gameGenre);
        }

        // GET: GameGenres/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameGenre gameGenre = db.GameGenres.Find(id);
            if (gameGenre == null)
            {
                return HttpNotFound();
            }
            return View(gameGenre);
        }

        // POST: GameGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GameGenre gameGenre = db.GameGenres.Find(id);
            db.GameGenres.Remove(gameGenre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
