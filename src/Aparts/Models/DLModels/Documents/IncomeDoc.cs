using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Aparts.Models.DLModels.Documents
{
	public class IncomeDoc : ICommitableDoc
	{
		public int Id { get; set; }

		public DateTime DocDate { get; set; }

		public string DocNumber { get; set; }

		public int IdStore { get; set; }

		public string Comment { get; set; }

		public virtual Store Store { get; set; }

		public virtual ICollection<IncomeDocDetail> Incomes { get; set; } = new HashSet<IncomeDocDetail>();

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int DocNumberInt { get; set; }

		public bool IsCommited { get; set; }

		public void Commit(ApartsDataContext context)
		{
			if (IsCommited)
			{
				return;
			}
			foreach (var docItem in Incomes)
			{
				var itemAmount =
					context.CurrentAmounts.SingleOrDefault(a => a.IdStoreItem == docItem.IdStoreItem && a.IdStore == IdStore);
				if (itemAmount == null)
				{
					itemAmount = new CurrentAmount() { IdStore = IdStore, IdStoreItem = docItem.IdStoreItem };
					context.CurrentAmounts.Add(itemAmount);
				}
				if (docItem.Amount != null)
				{
					itemAmount.Amount = itemAmount.Amount + docItem.Amount.Value;
				}
				if ((double)Math.Abs(itemAmount.Amount) <= 0.01)
				{
					context.CurrentAmounts.Remove(itemAmount);
				}
			}
			IsCommited = true;
		}

		public void Rollback(ApartsDataContext context)
		{
			if (!IsCommited)
			{
				return;
			}
			foreach (var docItem in Incomes)
			{
				var itemAmount =
					context.CurrentAmounts.SingleOrDefault(a => a.StoreItem.Id == docItem.IdStoreItem && a.IdStore == IdStore);
				if (itemAmount == null)
				{
					itemAmount = new CurrentAmount() { IdStore = IdStore, IdStoreItem = docItem.IdStoreItem };
					context.CurrentAmounts.Add(itemAmount);
				}
				if (docItem.Amount != null)
				{
					itemAmount.Amount = itemAmount.Amount - docItem.Amount.Value;
				}
				if ((double)Math.Abs(itemAmount.Amount) <= 0.01)
				{
					context.CurrentAmounts.Remove(itemAmount);
				}
			}
			IsCommited = false;
		}
	}
}
