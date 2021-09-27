using DAL.Models;
using Insight.Database;
using NUnit.Framework;
using RedTenAngularTests.ControllerTests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RedTenAngularTests.ControllerTests
{

    [TestFixture]
    public class PlayerControllerTest : ControllerTestBase
    {
        Player _testAddedPlayer;
        [Test]
        public async Task AddPlayerAsync()
        {
            Player player = new Player()
            {
                FirstName = "SecondTest",
                LastName = "SecondTest",
                Email = "SecondTest2@gmail.com",
            };
            _testAddedPlayer = await PostAsync<Player>("api/Players", player);
        }
        [Test]
        public async Task GetPlayers()
        {
            await AddPlayerAsync();
            var players = await GetAsync<IEnumerable<Player>>("api/Players");
            Assert.IsNotNull(players);
        }
        [Test]
        public async Task AddPlayerToRound()
        {

        }

        [TearDown]
        public async Task Teardown()
        {
            if (_testAddedPlayer != null)
            {
                using (var conn = new SqlConnection(_databaseCoonnection))
                {
                    await conn.ExecuteSqlAsync("delete from player where id=@id", new { id = _testAddedPlayer.id });
                }
            }
        }
    }
}
