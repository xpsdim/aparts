using Aparts.Models.ApartModels;
using Aparts.Models.DLModels;
using System.Collections.Generic;
using System.Linq;


namespace Aparts.Services
{
    public class ApartService
    {
        private readonly ApartsDataContext _partContext;
        public ApartService(ApartsDataContext partContext)
        {            
            _partContext = partContext;
        }

        public ApartsDataContext Context { get { return _partContext; } }

        public Stores[] GetAllStores()
        {
            return _partContext.Stores.ToArray();
        }

        public StoreViewModel[] GetVisibleStores(string userId)
        {
            var user = _partContext.AspNetUsers.Where(u => u.Id == userId).Single();
            return user.UserVisibleStores                
                    .Select(us => new StoreViewModel() {
                        Id = us.Store.Id,
                        Caption = us.Store.Caption,
                        Storeman = us.Store.Storeman
                    }).ToArray();            
        }
    }
}
