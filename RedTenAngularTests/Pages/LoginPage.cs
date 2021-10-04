using Atata;
using _ = RedTenAngularTests.Pages.LoginPage;

namespace RedTenAngularTests.Pages
{
    [Url("login")]
    public class LoginPage : Page<_>
    {    
        [FindById("login-username")]
        public TextInput<_> UserName { get; private set; }

        [FindById("login-password")]
        public PasswordInput<_> Password { get; private set; }

        [FindByAttribute("type", "submit")]
        public Button<_>Login { get; private set; }

        [FindByClass("toasta-type-error")]
        public Control<_> ToastError { get; private set; }
    }
}
