// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        string CurrentUserId { get; }
        IPlayerRepository Players { get; }
        IRoundRepository Rounds { get; }
        IGameRepository Games { get; }
        IGroupRepository Groups { get; }
        IGroupUserRepository GroupUsers { get; }
        IRoundPlayerRepository RoundPlayers { get; }
        IPlayerGroupRepository PlayerGroups { get; }
        int SaveChanges();
    }
}
