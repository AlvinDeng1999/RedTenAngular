using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetAllGames();
        void AddGame(Game game);
        Game GetGame(int id);
        int? GetGameId(string userId);
    }
}
