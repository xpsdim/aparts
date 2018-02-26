using System.Linq;
using Aparts.Models.ApartModels;
using Aparts.Models.DLModels;

namespace Aparts.Services
{
	public class ApartService
	{
		private readonly ApartsDataContext _partContext;

		public ApartService(ApartsDataContext partContext)
		{
			_partContext = partContext;
		}

		public ApartsDataContext Context => _partContext;

		public Store[] GetAllStores()
		{
			return _partContext.Stores.ToArray();
		}

		public StoreViewModel[] GetVisibleStores(string userId)
		{
			var user = _partContext.AspNetUsers.Single(u => u.Id == userId);
			return user.UserVisibleStores
					.Select(us => new StoreViewModel()
					{
						Id = us.Store.Id,
						Caption = us.Store.Caption,
						Storeman = us.Store.Storeman
					}).ToArray();
		}
	}
}
