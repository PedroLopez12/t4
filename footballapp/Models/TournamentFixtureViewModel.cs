using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace footballapp.Models
{
    public class TournamentFixtureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Match> Fixture { get; set; }
    }
}