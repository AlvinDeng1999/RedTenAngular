using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void AddPlayer(Player player)
        {
            this._appContext.Players.Add(player);
            this._appContext.SaveChanges();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _appContext.Players
                .ToList();
        }

        public Player GetPlayer(int id)
        {
            return this._appContext.Players.Find(id);
        }
    }
}
