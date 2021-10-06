using Atata;
using _ = RedTenAngularTests.Pages.PlayerPage;

namespace RedTenAngularTests.Pages
{
    public class PlayerPage : Page<_>
    {
        [FindById("btnViewPlayers")]
        public Button<_> ViewPlayersButton { get; private set; }
        [FindById("btnHidePlayers")]
        public Button<_> HidePlayersButton { get; private set; }

        [FindById("btnAddPlayer")]
        public Button<_> AddPlayersButton { get; private set; }

    }
}
