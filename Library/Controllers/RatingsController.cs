using Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RatingsController : Controller
    {
        DataContext db = new DataContext();
        // GET: Ratings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SetRating(string GameId, decimal rank)
        {
            Rating rating = new Rating();
            rating.Rank = rank;
            rating.GameId = GameId;
            rating.UserId = User.Identity.GetUserId();

            System.Diagnostics.Debug.WriteLine("Rank: " + rating.Rank);
            System.Diagnostics.Debug.WriteLine("GameId: " + rating.GameId);
            System.Diagnostics.Debug.WriteLine("UserId: " + rating.UserId);
            db.Ratings.Add(rating);
            db.SaveChanges();

            return RedirectToAction("Details", "Games", new { id = GameId });            
        }
    }
}