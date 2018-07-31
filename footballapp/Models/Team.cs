using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace footballapp.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int Points { get; set; }
        [StringLength(50)]
        public virtual List<Player> Players { get; set; }

    }
}