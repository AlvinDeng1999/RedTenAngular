using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        IEnumerable<Player> GetAllPlayers();
        void AddPlayer(Player player);
        public Player GetPlayer(int id);
    }
}
