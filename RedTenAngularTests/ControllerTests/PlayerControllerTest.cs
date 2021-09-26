using DAL.Models;
using NUnit.Framework;
using RedTenAngularTests.ControllerTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedTenAngularTests
{

    [TestFixture]
    public class PlayerControllerTest : ControllerTestBase
    {
        [Test]
        public async Task AddPlayerAsync()
        {
            Player player = new Player()
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                Email = "TestEmail@gmail.net",
            };
            var player2 = await PostAsync<Player>("api/Players", player);
        }
    }
}
