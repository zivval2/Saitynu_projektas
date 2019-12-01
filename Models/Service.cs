using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynu_projektas.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "varchar(12)")]
        public string Price { get; set; }
        [Required]
        [ForeignKey("UserId")]
         public int ArtistId { get; set; }
    }
}
