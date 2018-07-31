using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace footballapp.Models
{
    public class TournamentDetailsViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<Team> Leaderboard { get; set; }
    }
}