using System.Linq;
using Aparts.Models.ApartModels;
using Aparts.Models.DLModels;
using Aparts.Models.DLModels.Documents;

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

		public void CommitDocument(ICommitableDoc doc)
		{
			doc.Commit(Context);
		}

		public void RollbackDocument(ICommitableDoc doc)
		{
			doc.Rollback(Context);
		}

		public StoreViewModel[] GetVisibleStores(string userId)
		{
			var visibleStoreIds =
				_partContext.UserVisibleStores.Where(vs => vs.UserId == userId).Select(vs => vs.StoreId);
			return _partContext.Stores.Where(s => visibleStoreIds.Contains(s.Id))
					.Select(us => new StoreViewModel()
					{
						Id = us.Id,
						Caption = us.Caption,
						Storeman = us.Storeman
					}).ToArray();
			
		}
	}
}
