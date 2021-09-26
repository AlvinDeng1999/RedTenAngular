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
    public class RoundControllerTest : ControllerTestBase
    {
        [Test]   
        public async Task AddRoundAsync()
        {
            Round round = new Round()
            {
                Time = DateTime.Now
            };
            var createRound = await PostAsync<Round>("api/Rounds", round);
        }
    }
}
