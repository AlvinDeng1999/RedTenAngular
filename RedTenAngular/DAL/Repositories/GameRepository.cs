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

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
