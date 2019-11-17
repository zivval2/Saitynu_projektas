using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynu_projektas.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
       // [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Role { get; set; }

    }
}
