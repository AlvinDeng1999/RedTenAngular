using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        { }
        public void AddGame(Game game)
        {
            this._appContext.Games.Add(game);
            this._appContext.SaveChanges();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _appContext.Games
                 .ToList();
        }

        public Game GetGame(int id)
        {
            return _appContext.Games.Where(g => g.id == id).Include(g => g.Rounds).FirstOrDefault();
        }

        public int? GetGameId(string userId)
        {
            int? groupid = this._appContext.GroupUsers.Where(gu=>gu.userId==userId).FirstOrDefault()?.GroupId;
            if (groupid == null) return null;
            return this._appContext.Games.Where(g => g.GroupId == groupid && g.Status == "Open").OrderByDescending(g => g.Date).FirstOrDefault()?.id;
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
