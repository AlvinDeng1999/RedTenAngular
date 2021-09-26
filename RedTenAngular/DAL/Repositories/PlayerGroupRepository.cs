using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PlayerGroupRepository : Repository<PlayerGroup>, IPlayerGroupRepository
    {
        public PlayerGroupRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public void AddPlayerGroup(PlayerGroup playergroup)
        {
            this._appContext.PlayerGroups.Add(playergroup);
            this._appContext.SaveChanges();
        }
    }
}
