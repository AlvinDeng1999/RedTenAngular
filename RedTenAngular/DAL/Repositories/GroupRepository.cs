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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<Group> GetAllGroups(string userid)
        {
            //var groupusers = _appContext.GroupUsers.Where(gu => gu.userId == userid);
            //var groups =_appContext.Groups.Include(g=>g.Games.OrderByDescending(gm=>gm.Date).Take(10));
            var returnModel = from gu in _appContext.GroupUsers
                              join g in _appContext.Groups
                              on gu.GroupId equals g.id
                              where gu.userId == userid
                              select g;
            return returnModel.Include(g => g.Games.OrderByDescending(gm => gm.Date).Take(10));
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
