using Aparts.Models.ApartModels;

namespace Aparts.Models.AccountViewModels
{
    public class UserRolesModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public StoreViewModel[] VisibleStores { get; set; }
    }
}
