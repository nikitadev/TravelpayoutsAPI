using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;

namespace AviaTicketsWpfApplication.Fundamentals
{
	public class DbConnection : IDbConnection
	{
		public SQLiteAsyncConnection SQLiteConnection { get; private set; }

		public DbConnection(string dbName)
		{
			SQLiteConnection = new SQLiteAsyncConnection(dbName);
        }
	}
}
