using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynu_projektas.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Time Time { get; set; }
        [Required]
        public Service Service { get; set; }
    }
}
