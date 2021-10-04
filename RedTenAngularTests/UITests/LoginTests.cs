using Atata;
using NUnit.Framework;
using RedTenAngularTests.Pages;
using System;
using System.Threading.Tasks;

namespace RedTenAngularTests.UITests
{
    public class LoginTests : UITestFixtureBase
    {
        [Test]
        public void LoginPositiveTest()
        {
            Login();
        }

        [Test]
        public void LoginWithInvalidUserNamePassowrd()
        {
            var page = Go.To<LoginPage>()
                .UserName.Wait(Until.Visible, new WaitOptions(90))
                .UserName.SetRandom()
                .Password.SetRandom()
                .Wait(0.5)
                .Login.Click()
                .Wait(2)
                .ToastError.Should.BeVisible();          
           
        }
    }
}
