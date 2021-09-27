using DAL.Models;
using Insight.Database;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            if (_testCreatedGroup != null)
            {
                using (var conn = new SqlConnection(_databaseCoonnection))
                {
                    await conn.ExecuteSqlAsync("delete from groups where id=@id", new { id = _testCreatedGroup.id });
                }
            }
            Group group = new Group()
            {
                Name = "TestGroup"
            };
            _testCreatedGroup = await PostAsync<Group>("api/Groups", group);

            var r = await PostAsync<Group>("api/Groups", group, successExpected:false);
            Assert.IsNull(r);
        }
        [Test]
        public async Task GetGroupAsync()
        {
            var groups = await GetAsync<IEnumerable<Group>>("api/Groups");
            Assert.IsNotNull(groups);

            var group = await GetAsync<Group>($"api/Groups/{groups.First().id}");
            Assert.IsNotNull(group);
        }
    }
}
