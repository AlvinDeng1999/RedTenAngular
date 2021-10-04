using Atata;
using NUnit.Framework;
using RedTenAngularTests.Pages;
using System;
using System.Data.SqlClient;
using Insight.Database;

namespace RedTenAngularTests.UITests
{
    public class GroupUITests : UITestFixtureBase
    {

        string groupName;
        string userid;
        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            base.OneTimeSetup();
            using (var conn = new SqlConnection(this._databaseCoonnection))
            {
                userid = conn.ExecuteScalarSql<string>("select id from aspNetUsers where userName=@userName", new { _testSettings.UserName });
            }
        }
        [SetUp]
        public void Init()
        {
            DeleteGroup();
        }
        [TearDown]
        public void Teardown()
        {
            DeleteGroup();
        }
        
        [Test]
        public void AddGroupTest()
        {
            Login();
            Go.To<GroupPage>()
                .Wait(0.5)
                .AddGroupButton.Click()
                .Wait(1)
                .GroupName.Wait(Until.Visible)
                .GroupName.SetRandom(out groupName)
                .SaveButton.Click();
        }

        /// <summary>
        /// delete the group before and after tests if any for this user
        /// </summary>
        private void DeleteGroup()
        {
            using (var conn = new SqlConnection(this._databaseCoonnection))
            {
                conn.ExecuteSql("delete g from groups g join groupUsers gu on gu.groupId=g.id and gu.userid=@userid", new { userid });
            }
        }
    }
}
