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

        [Test]
        public void FirstGroupPlayers()
        {
            AddGroupTest();
            var page = Go.To<PlayerPage>()
                .ViewPlayersButton.Should.BeVisible()
                .HidePlayersButton.Should.Not.BeVisible()
                .AddPlayersButton.Should.Not.BeVisible()
                .ViewPlayersButton.Click()
                .Wait(1);

                page.HidePlayersButton.Should.BeVisible()
                .AddPlayersButton.Should.BeVisible()
                .ViewPlayersButton.Should.Not.BeVisible()
                .HidePlayersButton.Click()
                .Wait(1)
                .ViewPlayersButton.Click()
                .Wait(1)
                .AddPlayersButton.Click();
        }

        [Test]
        public void AddPlayers()
        {
            string name;
            FirstGroupPlayers();
            Go.To<NewPlayerPage>()
                .Wait(1)
                .SaveButton.Should.BeDisabled()
                .CancelButton.Should.Not.BeDisabled()
                .FirstName.SetRandom()
                .Wait(1)
                .SaveButton.Should.BeDisabled()
                .lastName.SetRandom()
                .NickName.SetRandom()
                .Wait(1)
                .SaveButton.Should.BeDisabled()
                .Email.SetRandom(out name)
                .Wait(1)
                .SaveButton.Should.BeDisabled()
                .Email.Set($"{name}@yahoo.net")
                .SaveButton.ClickAndGo<PlayerPage>()
                .AddPlayersButton.ClickAndGo<NewPlayerPage>();
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
