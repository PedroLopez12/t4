using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace footballapp.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int Tournament_Id { get; set; }
        public int matchWeek { get; set; }
        [StringLength(50)]
        public string Team_1 { get; set; }
        [StringLength(50)]
        public string Team_2 { get; set; }
        public int Team_1_Score { get; set; }
        public int Team_2_Score { get; set; }
        [StringLength(50)]
        public string Winner { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}