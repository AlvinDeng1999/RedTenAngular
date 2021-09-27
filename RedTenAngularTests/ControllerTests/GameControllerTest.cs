using DAL.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTenAngularTests.ControllerTests
{
    [TestFixture]
    public class GameControllerTest : ControllerTestBase
    {
        [Test]
        public async Task AddGame()
        {
            Game game = new Game()
            {
                Location = "Home",
                Date = DateTime.Now,
                Status = "Open"
            };
            var status = await PostAsync<Game>("api/Games", game);
        }
        [Test]
        public async Task GetGame()
        {
            await AddGame();
            var games = await GetAsync<IEnumerable<Game>>("api/Games");
            Assert.IsNotNull(games);

            var game = await GetAsync<Game>($"api/Games/{games.First().id}");
            Assert.IsNotNull(game);  
        }
        
    }
}
