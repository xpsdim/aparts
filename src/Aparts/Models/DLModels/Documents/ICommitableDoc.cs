namespace Aparts.Models.DLModels.Documents
{
	public interface ICommitableDoc
	{
		bool IsCommited { get; set; }

		void Commit(ApartsDataContext context);

		void Rollback(ApartsDataContext context);
	}
}
