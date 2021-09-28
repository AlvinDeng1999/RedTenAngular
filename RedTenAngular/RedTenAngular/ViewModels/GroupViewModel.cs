using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngular.ViewModels
{
    public class GroupViewModel : Group
    {
        public IEnumerable<Player> Players { get; set; }
        public GroupViewModel(Group group) { 
            this.id = group.id; 
            this.Name = group.Name; 
            this.Games = group.Games; 
        }
        public GroupViewModel()
        {

        }
    }
}
