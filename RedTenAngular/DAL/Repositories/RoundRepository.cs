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
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        public RoundRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void AddRound(Round round)
        {
            this._appContext.Rounds.Add(round);
            this._appContext.SaveChanges();
        }

        public IEnumerable<Round> GetAllRounds()
        {
            return _appContext.Rounds
                .ToList();
        }

        public Round GetRound(int id)
        {
            return this._appContext.Rounds.Where(r => r.id == id).Include(r => r.Players).FirstOrDefault();
        }
    }
}
