// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        IGroupRepository _groups;
        IGameRepository _games;
        IPlayerRepository _players;
        IRoundRepository _rounds;
        IGroupUserRepository _groupusers;
        IRoundPlayerRepository _roundplayers;
        IPlayerGroupRepository _playergroups;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CurrentUserId => _context.CurrentUserId;

        public IGroupRepository Groups
        {
            get
            {
                if (_groups == null)
                    _groups = new GroupRepository(_context);
                return _groups;
            }
        }
        public IGameRepository Games
        {
            get
            {
                if (_games == null)
                    _games = new GameRepository(_context);
                return _games;
            }
        }
        public IPlayerRepository Players
        {
            get
            {
                if (_players == null)
                    _players = new PlayerRepository(_context);
                return _players;
            }
        }
        public IRoundRepository Rounds
        {
            get
            {
                if (_rounds == null)
                    _rounds = new RoundRepository(_context);
                return _rounds;
            }
        }

        public IGroupUserRepository GroupUsers
        {
            get
            {
                if (_groupusers == null)
                    _groupusers = new GroupUserRepository(_context);
                return _groupusers;
            }
        }

        public IRoundPlayerRepository RoundPlayers
        {
            get
            {
                if (_roundplayers == null)
                    _roundplayers = new RoundPlayerRepository(_context);
                return _roundplayers;
            }
        }

        public IPlayerGroupRepository PlayerGroups
        {
            get
            {
                if (_playergroups == null)
                    _playergroups = new PlayerGroupRepository(_context);
                return _playergroups;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
