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
        public async Task Login()
        {
            await base.LoginAsync();
        }

        public async Task AddPlayerAsync()
        {
            
        }
    }
}
