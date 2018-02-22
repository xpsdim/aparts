using System;
using System.Data;
using Aparts.Models;
using Aparts.Models.DLModels;
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

		public FbConnection SourceConnection => new FbConnection(_importSettings.Value.DatabaseConnectionString);

		public ImportService(IOptions<ImportSettings> importSettings, ApartService apartService)
		{
			_importSettings = importSettings;
			_apartService = apartService;
		}

		public void Import()
		{
			ClearData();
			LoadGroups();
			LoadSubGroups();
			LoadStoreItems();
		}

		private void ClearData()
		{
			var storeTables = new[] { "StoreItems", "SubGroups", "Groups" };
			foreach (var table in storeTables)
			{
				_apartService.Context.Database.ExecuteSqlCommand($"delete {table}");
			}
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
	}
}
