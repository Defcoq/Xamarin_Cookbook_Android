using SQLite;
using SQLite.Net.Attributes;

namespace XamarinCookbook
{
	public class MyTable
	{
		[AutoIncrement, PrimaryKey]
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
