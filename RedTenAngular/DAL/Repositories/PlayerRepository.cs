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

        public IEnumerable<Player> GetAllPlayers(string userId)
        {
            var gid = _appContext.GroupUsers.Where(gu => gu.userId == userId).FirstOrDefault()?.GroupId;

            var players = from playerGroup in _appContext.PlayerGroups
                          join player in _appContext.Players
                          on playerGroup.PlayerId equals player.id
                          where playerGroup.GroupId == gid
                          select player;

            return players;
        }

        public Player GetPlayer(int id)
        {
            return this._appContext.Players.Find(id);
        }
    }
}
