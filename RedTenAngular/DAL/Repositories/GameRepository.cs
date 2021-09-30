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

        public void UpdateGame(Game game)
        {
            var gameInDb = this._appContext.Games.Find(game.id);
            gameInDb.Status = game.Status;
            gameInDb.Date = game.Date;
            gameInDb.Location = game.Location;
            gameInDb.Rounds = game.Rounds;
            this._appContext.Games.Add(gameInDb).State = EntityState.Modified; //Update(game);
            this._appContext.SaveChanges();
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
