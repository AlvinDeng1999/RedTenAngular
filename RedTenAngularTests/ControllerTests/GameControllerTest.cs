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
        Game _game;
        [Test]
        public async Task AddGame()
        {
            _game = new Game()
            {
                Location = "Home",
                Date = DateTime.Now,
                Status = "Open"
            };
            var status = await PostAsync<Game>("api/Games", _game);
            _game = status;
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
        
        [Test]
        public async Task UpdateGame()
        {
            await AddGame();
            int badGroupId = this._game.GroupId - 1;
            _game.Status = "Closed";
            var game = await PutAsync<Game>("api/Games/", _game);
            Assert.IsNotNull(game);
            Assert.AreEqual("Closed", game.Status);
        }
    }
}
