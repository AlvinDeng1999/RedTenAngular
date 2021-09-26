using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PlayerGroup
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
