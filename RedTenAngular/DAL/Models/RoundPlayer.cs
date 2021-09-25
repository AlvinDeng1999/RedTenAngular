using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RoundPlayer
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public int RoundId { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
