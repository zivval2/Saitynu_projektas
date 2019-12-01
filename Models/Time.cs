using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynu_projektas.Models
{
    public class Time
    {
        [Key]
        public int TimeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Boolean IsWorking { get; set; }
        [Required]
        public Boolean IsUsed { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int ArtistId { get; set; }
        //[Required]
        //public User Artist { get; set; }
    }
}
