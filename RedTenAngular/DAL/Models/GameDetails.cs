using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameDetails : Game
    {
        public ICollection<RoundPlayer> RoundPlayers { get; set; }
        public ICollection<PlayerGameScore> PlayerGameScores { get; set; }
    }
    public class PlayerGameScore
    {
        public Player Player { get; set; }
        public int PlayerScore { get; set; }
    }
}
