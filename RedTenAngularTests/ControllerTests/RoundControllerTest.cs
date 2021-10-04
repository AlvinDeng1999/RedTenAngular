using DAL.Models;
using NUnit.Framework;
using RedTenAngular.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTenAngularTests.ControllerTests
{
    [TestFixture]
    public class RoundControllerTest : ControllerTestBase
    {
        [Test]   
        [Ignore("To Be Done due to data setup is incomplete")]
        public async Task AddRoundAsync()
        {
            List<PlayerViewModel> playerlist = new List<PlayerViewModel>();
            IEnumerable<Player> players = await GetAsync<IEnumerable<Player>>("api/Players");
            playerlist = players.Select(p => new PlayerViewModel() {
                PlayerId = p.id,
                Score = 2
            }).ToList();
            playerlist.Last().Score = 0;
            RoundViewModel round = new RoundViewModel()
            {
                Time = DateTime.Now,
                Players = playerlist
            };
            var createRound = await PostAsync<RoundViewModel>("api/Rounds", round);
            Assert.IsNotNull(createRound);


        }
    }
}
