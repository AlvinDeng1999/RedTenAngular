using Atata;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RedTenAngularTests.ControllerTests;
using RedTenAngularTests.Pages;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngularTests.UITests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class UITestFixtureBase
    {
        protected TestSettings _testSettings;
        protected string _databaseCoonnection;
        
        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .AddJsonFile("Atata.local.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var baseUrl = configuration.GetValue<string>("baseUrl");
            _testSettings = configuration.GetSection("AppSettings").Get<TestSettings>();
            _testSettings.WebAppRootUrl = baseUrl;
            _databaseCoonnection = configuration.GetConnectionString("DefaultConnection");
        }

        [SetUp]
        public void SetUp()
        {

           
            AtataContext.Configure().Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        protected void Login()
        {
            Go.To<LoginPage>()
                .UserName.Wait(Until.Visible, new WaitOptions(90))
                .UserName.Set(_testSettings.UserName)
                .Password.Set(_testSettings.Password)
                .Wait(0.5)
                .Login.Click();
        }
    }
}
