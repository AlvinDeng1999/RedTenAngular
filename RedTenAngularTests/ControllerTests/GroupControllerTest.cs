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
    public class GroupControllerTest : ControllerTestBase
    {
        [Test]
        public async Task AddGroupAsync()
        {
            Group group = new Group()
            {
                Name = "TestGroup"
            };
            var createGroup = await PostAsync<Group>("api/Groups", group);
        }
    }
}
