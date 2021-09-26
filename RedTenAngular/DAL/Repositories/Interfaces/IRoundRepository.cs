using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRoundRepository : IRepository<Round>
    {
        IEnumerable<Round> GetAllRounds();
        void AddRound(Round round);
        Round GetRound(int id);
    }
}
