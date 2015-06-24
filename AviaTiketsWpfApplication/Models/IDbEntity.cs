using SQLite;

namespace AviaTicketsWpfApplication.Models
{
	public interface IDbEntity
	{
		int Id { get; set; }

		string Tag { get; set; }
	}
}
