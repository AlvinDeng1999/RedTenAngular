using Atata;
using _ = RedTenAngularTests.Pages.NewPlayerPage;

namespace RedTenAngularTests.Pages
{
    public class NewPlayerPage : Page<_>
    {
        [FindById("playerFirst")]
        public TextInput<_> FirstName { get; private set; }

        [FindById("playerLast")]
        public TextInput<_> lastName { get; private set; }

        [FindById("playerNick")]
        public TextInput<_> NickName { get; private set; }

        [FindById("playerEmail")]
        public EmailInput<_> Email { get; private set; }

        [FindById("btnSavePlayer")]
        public Button<_> SaveButton { get; private set; }

        [FindById("btnCancelPlayer")]               
        public Button<_> CancelButton { get; private set; }

    }
}
