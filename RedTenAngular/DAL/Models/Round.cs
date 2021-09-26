using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Round
    {
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
