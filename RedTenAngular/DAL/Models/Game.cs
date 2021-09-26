using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Game
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        public ICollection<Round> Rounds { get; set; }
    }
}
