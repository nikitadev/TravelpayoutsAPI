using SQLite;

namespace AviaTicketsWpfApplication.Fundamentals.Interfaces
{
	public interface IDbConnection
	{
		SQLiteAsyncConnection SQLiteConnection { get; }
	}
}
