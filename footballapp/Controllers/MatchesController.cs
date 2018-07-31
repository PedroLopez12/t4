using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using footballapp.Models;

namespace footballapp.Controllers
{
    public class MatchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Matches
        public ActionResult Index()
        {
            return View(db.Matches.ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create([Bind(Include = "Id,matchWeek,Team_1,Team_2,tournament_Id,Team_1_Score,Team_2_Score")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
            }
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tournament_Id,Id,matchWeek,Team_1,Team_2,tournament_Id,Team_1_score,Team_2_score,Winner")] Match match)
        {
            if (ModelState.IsValid)
            {
                if (match.Team_1_Score > match.Team_2_Score)
                {
                    Team team = db.Teams.Where(x => x.Name == match.Team_1).Single();
                    team.Points = (team.Points) + 3;
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if(match.Team_2_Score > match.Team_1_Score)
                {
                    Team team = db.Teams.Where(x => x.Name == match.Team_2).Single();
                    team.Points = (team.Points) + 3;
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    Team local = db.Teams.Where(x => x.Name == match.Team_1).Single();
                    Team visitor = db.Teams.Where(x => x.Name == match.Team_2).Single();

                    local.Points = (local.Points) + 1;
                    visitor.Points = (visitor.Points) + 1;

                    db.Entry(local).State = EntityState.Modified;
                    db.Entry(visitor).State = EntityState.Modified;

                    db.SaveChanges();
                }
                
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Fixture", "Tournaments", new { id = match.Tournament_Id });
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
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

        [HttpPost]
        [Route ("Matches/UpdateMatch/")]
        public void UpdateMatch(int matchweek,string team1, string team2, int team1score, int team2score,string winner)
        {
            Match updatedMatch = new Match();

            updatedMatch.matchWeek = matchweek;
            updatedMatch.Team_1 = team1;
            updatedMatch.Team_2 = team2;
            updatedMatch.Team_1_Score = team1score;
            updatedMatch.Team_2_Score = team2score;
            updatedMatch.Winner = winner;

            db.Matches.Attach(updatedMatch);
            var entry = db.Entry(updatedMatch);
            entry.Property(e => e.Winner).IsModified = true;
            db.SaveChanges();
        }
    }
}
