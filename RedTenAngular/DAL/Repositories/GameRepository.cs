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

        public GameDetails GetGame(int id, string userid)
        {
            var game = _appContext.Games.Where(g => g.id == id).Include(g => g.Rounds).FirstOrDefault();

            var groupPlayers =
                (
                    from g in _appContext.Groups
                    join gu in _appContext.GroupUsers.Where(x=>x.userId==userid)
                    on g.id equals gu.GroupId
                    join gp in _appContext.PlayerGroups
                    on g.id equals gp.GroupId
                    join p in _appContext.Players
                    on gp.PlayerId equals p.id
                    select p
                ).ToList();

            var scores = (from rp in _appContext.RoundPlayers.Where(rp=>game.Rounds.Select(r=>r.id).Contains(rp.RoundId))              
                         group rp by rp.PlayerId into playerScore
                         select new
                         {
                             PlayerId = playerScore.Key,
                             PlayerScore = playerScore.Sum(x => x.Score)
                         }).ToList();
            var playerScores = from p in groupPlayers
                               join s in scores
                               on p.id equals s.PlayerId
                               select new PlayerGameScore()
                               {
                                   Player = p,
                                   PlayerScore = s.PlayerScore
                               };
            var gameDetails = new GameDetails()
            {
                GroupId = game.GroupId,
                PlayerGameScores = playerScores.ToList(),
                Date = game.Date,
                id = game.id,
                Location = game.Location,
                Rounds = game.Rounds,
            };
            return gameDetails;
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
