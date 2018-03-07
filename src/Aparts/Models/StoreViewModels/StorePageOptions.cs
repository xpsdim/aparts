using Aparts.Models.ApartModels;
using System.Collections.Generic;
using System.Linq;

namespace Aparts.Models.StoreViewModels
{
    public class StorePageOptions
    {
        public IEnumerable<StoreViewModel> VisibleStores { get; set; }
        public string VisibleStoresAsJsArray =>
           $"[{string.Join(",", VisibleStores.Select(s => $"{{field: 't{s.Id + 1}', title: '{s.Caption}'}}"))}]";
    }
    
}
