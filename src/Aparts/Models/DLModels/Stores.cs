using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
    public partial class Stores
    {
        public Stores()
        {
            UserVisibleStores = new HashSet<UserVisibleStores>();
        }

        public int Id { get; set; }
        public string Caption { get; set; }
        public string Storeman { get; set; }

        public virtual ICollection<UserVisibleStores> UserVisibleStores { get; set; }
        public virtual AspNetUsers StoremanNavigation { get; set; }
    }
}
