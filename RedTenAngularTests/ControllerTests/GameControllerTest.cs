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
    }
}
