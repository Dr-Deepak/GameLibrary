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
    public class GamesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games;
            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            //Game game = db.Games.Include(x => x.Genres.Select(m => m.Genre)).SingleOrDefault(y => y.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            Game model = new Game();
            model.Name = String.Format("Game - {0}", DateTime.Now.Ticks);
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x => x.GenreId).ToArray());
            return View(model);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsMultiplayer")] Game model, string[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                Game checkmodel = db.Games.SingleOrDefault(x => x.Name== model.Name && x.IsMultiplayer == model.IsMultiplayer);
                if (checkmodel == null)
                {
                    //model.GameId = Guid.NewGuid().ToString();
                    //model.CreateDate = DateTime.Now;
                    //model.EditDate = model.CreateDate;
                    db.Games.Add(model);
                    db.SaveChanges();
                    
                    if(GenreIds!= null)
                    {
                        
                        foreach (string genreid in GenreIds)
                        {
                            System.Diagnostics.Debug.WriteLine(genreid);
                            GameGenre gameGenre = new GameGenre();

                             gameGenre.GameId = model.GameId;
                             gameGenre.GenreId = genreid;
                            model.Genres.Add(gameGenre);
                        }
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }                  
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Name,IsMultiplayer")] Game game)
        {
            if (ModelState.IsValid)
            {
                Game tmpgame = db.Games.Find(game.GameId);
                if(tmpgame != null)
                {
                    tmpgame.Name = game.Name;
                    tmpgame.IsMultiplayer = game.IsMultiplayer;
                    tmpgame.EditDate = DateTime.Now;

                    db.Entry(tmpgame).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Game model = db.Games.Find(id);

            if(model == null)
            {
                return HttpNotFound();
            }

            foreach (var item in model.Genres.ToList())
            {
                db.GameGenres.Remove(item);
            }

            db.Games.Remove(model);

            var deleted = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
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
