using System;
using System.Data;
using System.Linq;
using Aparts.Models.DLModels;
using Aparts.Models.DLModels.Documents;
using Aparts.Models.Settings;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Aparts.Services
{
	public class ImportService
	{
		private readonly IOptions<ImportSettings> _importSettings;

		private readonly ApartService _apartService;

		public bool AllowImport => _importSettings.Value.AllowImport;
		private readonly IOptions<DocSettings> _docSettings;

		public FbConnection SourceConnection => new FbConnection(_importSettings.Value.DatabaseConnectionString);

		public ImportService(IOptions<ImportSettings> importSettings, IOptions<DocSettings> docSettings, ApartService apartService)
		{
			_importSettings = importSettings;
			_apartService = apartService;
			_docSettings = docSettings;
		}

		public void Import()
		{
			ClearData();
			/*LoadGroups();
			LoadSubGroups();
			LoadStoreItems();		*/
			LoadCurrentBalance();
		}

		private void ClearData()
		{
			var storeTables = new[] { "DocIncomeDetails", "DocIncomeHeader", "СurrentAmounts"/*, "StoreItems", "SubGroups", "Groups"*/ };
			foreach (var table in storeTables)
			{
				_apartService.Context.Database.ExecuteSqlCommand($"delete {table}");
			}
		}

		private void LoadCurrentBalance()
		{
			// bunch of incoming documents creating
			var stores = _apartService.Context.Stores.ToArray();
			var incomeDocs = new IncomeDoc[stores.Count()];
			foreach (var store in stores)
			{
				var newIncomeDoc = CreateIncomeDocWithNumber();
				newIncomeDoc.Comment = $"Import of current amount from legacy store '{store.Caption}'";
				newIncomeDoc.DocDate = DateTime.Today;
				newIncomeDoc.IdStore = store.Id;
				incomeDocs[store.Id] = newIncomeDoc;
				_apartService.Context.IncomeDocs.Add(newIncomeDoc);
				_apartService.Context.SaveChanges();
			}

			// filling of incoming documents
			var connection = SourceConnection;
			connection.Open();
			var dt = new DataTable();

			try
			{
				var da = new FbDataAdapter(@"select 
					pd.T1, pd.T2, pd.T3, pd.T4, pd.T5, pd.T6, pd.T7, pd.T8, pd.T9, pd.T10,
					pd.T11, pd.T12, pd.T13, pd.T14, pd.T15, pd.T16, pd.T17, pd.T18, pd.T19, pd.T20,
					pd.PRICE_NUM, pd.PRICEIN, pd.PRICEOUT 
					from PRICE_DETAL pd
					join PRICE p on p.ID = pd.ID_MASTER
					where pd.PRICE_NUM > 0
					order by pd.PRICE_NUM", connection);
				da.Fill(dt);
			}
			finally
			{
				connection.Close();
			}

			foreach (DataRow row in dt.Rows)
			{
				foreach (var doc in incomeDocs)
				{
					if ((int)row.ItemArray[doc.IdStore] != 0)
					{
						var newIncomeDocItem = new IncomeDocDetail()
						{
							IncomeDoc = doc,
							IdStoreItem = (int)row.ItemArray[20],
							Amount = (int)row.ItemArray[doc.IdStore]
						};

						if (!(row.ItemArray[21] is DBNull))
						{
							newIncomeDocItem.PriceIn = (int)row.ItemArray[21];
						}

						if (!(row.ItemArray[22] is DBNull))
						{
							newIncomeDocItem.PriceOut = (int)row.ItemArray[22];
						}
						_apartService.Context.IncomeDocDetails.Add(newIncomeDocItem);
					}
				}
			}

			/* TODO this code cause error inserting of DocNumberInt column 
			foreach (var incomeDoc in incomeDocs)
			{
				incomeDoc.Commit(_apartService.Context);
			}
			*/
			_apartService.Context.SaveChanges();
		}

		private void LoadSubGroups()
		{
			var connection = SourceConnection;
			connection.Open();
			var dt = new DataTable();

			try
			{
				var da = new FbDataAdapter("select ID, ID_GROUP, NAME, REPLDATE from PRICE", connection);
				da.Fill(dt);
			}
			finally
			{
				connection.Close();
			}

			try
			{
				var da = new FbDataAdapter("select ID, ID_GROUP, NAME, REPLDATE from PRICE", connection);
				da.Fill(dt);
			}
			finally
			{
				connection.Close();
			}

			foreach (DataRow row in dt.Rows)
			{
				var newSubgroup = new SubGroup
				{
					Id = (int)row.ItemArray[0],
					IdGroup = (int)row.ItemArray[1],
					Name = (string)row.ItemArray[2]
				};

				if (!(row.ItemArray[3] is DBNull))
				{
					newSubgroup.ReplDate = (DateTime)row.ItemArray[3];
				}

				_apartService.Context.SubGroups.Add(newSubgroup);
			}
			_apartService.Context.SaveChanges();
		}

		private void LoadStoreItems()
		{

			var connection = SourceConnection;
			connection.Open();
			var dt = new DataTable();

			try
			{
				var da = new FbDataAdapter(@"select PRICE_NUM, ID_MASTER, KATCODE, PRICEOUT, PRICE_DETAL.REPLDATE 
					from PRICE_DETAL join PRICE on PRICE_DETAL.ID_MASTER = PRICE.ID
					where PRICE_NUM > 0", connection);
				da.Fill(dt);
			}
			finally
			{
				connection.Close();
			}

			foreach (DataRow row in dt.Rows)
			{
				var newStoreItem = new StoreItem { Id = (int)row.ItemArray[0], IdSubGroup = (int)row.ItemArray[1] };

				if (!(row.ItemArray[2] is DBNull))
				{
					newStoreItem.CodesByCatalog = (string)row.ItemArray[2];
				}

				if (!(row.ItemArray[3] is DBNull))
				{
					newStoreItem.Price = (int)row.ItemArray[3];
				}

				if (!(row.ItemArray[4] is DBNull))
				{
					newStoreItem.ReplDate = (DateTime)row.ItemArray[4];
				}

				_apartService.Context.StoreItems.Add(newStoreItem);
			}
			_apartService.Context.SaveChanges();

		}

		private void LoadGroups()
		{
			var connection = SourceConnection;
			connection.Open();
			var dt = new DataTable();

			try
			{
				var da = new FbDataAdapter("select ID, NAME, REPLDATE from GROUPS", connection);
				da.Fill(dt);
			}
			finally
			{
				connection.Close();
			}

			foreach (DataRow row in dt.Rows)
			{
				var newGroup = new Group
				{
					Id = (int)row.ItemArray[0],
					Name = (string)row.ItemArray[1]
				};

				if (!(row.ItemArray[2] is DBNull))
				{
					newGroup.ReplDate = (DateTime)row.ItemArray[2];
				}
				_apartService.Context.Groups.Add(newGroup);
			}
			_apartService.Context.SaveChanges();
		}

		private int GetIncomeDocumentNextNumber()
		{
			var today = DateTime.Today;
			var startOfYear = new DateTime(today.Year, 1, 1);
			var endOfYear = new DateTime(today.Year, 12, 31);
			return
				_apartService.Context.IncomeDocs.Where(doc => doc.DocDate >= startOfYear && doc.DocDate <= endOfYear)
					.Max(doc => doc.DocNumberInt) + 1;

		}

		private IncomeDoc CreateIncomeDocWithNumber()
		{
			var intNum = GetIncomeDocumentNextNumber();
			return new IncomeDoc()
			{
				DocNumber = $"{_docSettings.Value.ImputDocNumberPrefix}{intNum}"
			};

		}
	}
}
