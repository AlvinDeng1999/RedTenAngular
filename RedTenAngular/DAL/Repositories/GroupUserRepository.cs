using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupUserRepository : Repository<GroupUser>, IGroupUserRepository
    {
        public GroupUserRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void AddGroupUser(GroupUser groupuser)
        {
            this._appContext.GroupUsers.Add(groupuser);
            this._appContext.SaveChanges();
        }

        public bool UserHasGroup(string userId)
        {
            return this._appContext.GroupUsers.Any(gu => gu.userId == userId);
        }

        public int? GetGroupId(string userId)
        {
            return this._appContext.GroupUsers.Where(gu => gu.userId == userId).FirstOrDefault()?.GroupId;
        }
    }
}
