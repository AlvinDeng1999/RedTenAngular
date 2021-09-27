using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRoundPlayerRepository : IRepository<RoundPlayer>
    {
        void AddRoundPlayers(IEnumerable<RoundPlayer> roundplayers);
    }
}
