using System.Data;

using Aparts.Models;
using Aparts.Models.DLModels;

using FirebirdSql.Data.FirebirdClient;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

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
		}

		private void ClearData()
		{
			var storeTables = new[] { "SubGroups", "Groups" };
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
				_apartService.Context.SubGroups.Add(
					new SubGroup
					{
						Id = (int)row.ItemArray[0],
						IdGroup = (int)row.ItemArray[1],
						Name = (string)row.ItemArray[2],
						ReplDate = (DateTime)row.ItemArray[3]
					});
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
				_apartService.Context.Groups.Add(
					new Group
					{
						Id = (int)row.ItemArray[0],
						Name = (string)row.ItemArray[1],
						ReplDate = (DateTime)row.ItemArray[2]
					});
			}
			_apartService.Context.SaveChanges();
		}
	}
}
