using Atata;
using _ = RedTenAngularTests.Pages.GroupPage;

namespace RedTenAngularTests.Pages
{
   
    public class GroupPage : Page<_>
    {
        [FindByClass("float-right")]
        public Button<_> AddGroupButton { get; private set; }

        [FindByClass("float-left")]
        public Button<_> SaveButton { get; private set; }

        [FindByClass("btn-secondary")]
        public Button<_> CancelButton { get; private set; }

        [FindById("groupName")]
        public TextInput<_> GroupName { get; set; }
    }
}
