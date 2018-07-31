using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using footballapp.Models;

namespace footballapp.Controllers
{
    public class TournamentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tournaments
        public ActionResult Index()
        {
            return View(db.Tournaments.ToList());
        }

        // GET: Tournaments/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentDetailsViewModel tournamentDetailsViewModel = new TournamentDetailsViewModel();

            Tournament tournament = db.Tournaments.Find(id);
            
            if (tournament == null)
            {
                return HttpNotFound();
            }
            tournamentDetailsViewModel.id = tournament.Id;
            tournamentDetailsViewModel.Leaderboard = this.GetLeaderboard(id);
            tournamentDetailsViewModel.Name = tournament.Name;

            return View(tournamentDetailsViewModel);
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Tournaments.Add(tournament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournament);
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

        public ActionResult Fixture (int id)
        {
            TournamentFixtureViewModel tournamentFixtureViewModel = new TournamentFixtureViewModel();
            Tournament tournament = db.Tournaments.Find(id);
            List<Match> PreviousMatches = db.Matches.Where(x => x.Tournament_Id == id).ToList();
            List<Match> Matches = new List<Match>();
            tournamentFixtureViewModel.Fixture = PreviousMatches.OrderBy(x => x.matchWeek).ToList();
            if (PreviousMatches.Count == 0)
            {
                Matches = this.GenerateFixture(id);
                tournamentFixtureViewModel.Fixture = Matches.OrderBy(x => x.matchWeek).ToList();
            }
            tournamentFixtureViewModel.Id = tournament.Id;
            tournamentFixtureViewModel.Name = tournament.Name;

            return View(tournamentFixtureViewModel);
        }

        [HttpGet]
        public List<Team> GetLeaderboard(int tournamentId)
        {
            List<Team> leaderBoard = new List<Team>();
            Tournament Tournament = db.Tournaments.Where(x => x.Id == tournamentId).Single();
            leaderBoard = Tournament.Teams.OrderByDescending(x => x.Points).ToList();

            return (leaderBoard);
        }

        [HttpPost]
        public List<Match> GenerateFixture(int tournamentId)
        {
        List<Match> Fixture = new List<Match>();
            Tournament tournament = db.Tournaments.Where(x => x.Id == tournamentId).Single();
            Team [] teams  = tournament.Teams.ToArray();
            

            for(int i = 0; i < tournament.Teams.Count; i ++)
            {
                int matchWeek = 1;
                for (int j = 0; j < tournament.Teams.Count; j++)
                {
                    if (teams[i] != teams[j])
                    {
                        Fixture.Add(new Match() { Team_1 = teams[i].Name, Team_2 = teams[j].Name, Tournament_Id = tournamentId, Team_1_Score = 0, Team_2_Score = 0, matchWeek = matchWeek });
                        matchWeek++;
                    }
                }
            }

            for(int i = 0; i < Fixture.Count; i++)
            {
                db.Matches.Add(Fixture[i]);
                db.SaveChanges();
            }
            return Fixture;
        }
    }
}
