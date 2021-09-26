using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGroupUserRepository : IRepository<GroupUser>
    {
        bool UserHasGroup(string userId);
        void AddGroupUser(GroupUser groupuser);
        public int? GetGroupId(string userId);
    }
}
