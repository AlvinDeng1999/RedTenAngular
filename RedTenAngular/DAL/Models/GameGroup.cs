using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameGroup
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
