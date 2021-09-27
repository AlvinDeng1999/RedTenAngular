using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoundPlayerRepository : Repository<RoundPlayer>, IRoundPlayerRepository
    {
        public RoundPlayerRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void AddRoundPlayers(IEnumerable<RoundPlayer> roundplayers)
        {
            this._appContext.RoundPlayers.AddRange(roundplayers);
            this._appContext.SaveChanges();
        }
    }
}
