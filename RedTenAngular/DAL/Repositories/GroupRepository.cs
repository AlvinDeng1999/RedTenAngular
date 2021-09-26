﻿using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<Group> GetAllGroups()
        {
            return _appContext.Groups
                .ToList();
        }

        public void AddGroup(Group group)
        {
            this._appContext.Groups.Add(group);
            this._appContext.SaveChanges();
        }

        public Group GetGroup(int id)
        {
            return this._appContext.Groups.Find(id);
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}