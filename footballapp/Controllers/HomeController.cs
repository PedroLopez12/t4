using footballapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace footballapp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            TournamentViewModel tournaments = new TournamentViewModel();
            tournaments.Tournaments = GetAll();
            return View(tournaments);
        }

        private List<Tournament> GetAll()
        {
            List<Tournament> tournaments = db.Tournaments.ToList(); ;
            return tournaments;
        }

    }
}