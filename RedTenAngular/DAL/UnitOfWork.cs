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

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;
        IGroupRepository _groups;
        IGameRepository _games;
        IPlayerRepository _players;
        IRoundRepository _rounds;
        IGroupUserRepository _groupusers;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CurrentUserId => _context.CurrentUserId;

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }



        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }



        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }

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

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
