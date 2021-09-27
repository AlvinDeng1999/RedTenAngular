using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngular.ViewModels
{
    public class RoundViewModel : Round
    {
        public ICollection<PlayerViewModel> Players { get; set; }
    }
    public class PlayerViewModel 
    {
        public int PlayerId { get; set; }
        public int Score { get; set; }
    }
}
