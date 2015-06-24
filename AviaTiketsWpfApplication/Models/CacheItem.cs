using SQLite;
using System;

namespace AviaTicketsWpfApplication.Models
{
    [Table("Cache")]
	public class CacheItem : IDbEntity
	{
        [PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Info { get; set; }
        [Indexed]
		public string Tag { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
        public bool IsTemporary { get; set; }
	}
}
