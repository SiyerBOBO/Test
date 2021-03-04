using System;
using System.Data.SQLite;
using System.Threading;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			using(SQLiteConnection _sql_conn = new SQLiteConnection())
			{
				_sql_conn.ConnectionString += "data source = test.db;";
				_sql_conn.ConnectionString += "version = 3;";
				try
				{
					_sql_conn.Open();
					using(SQLiteCommand _sql_cmd = _sql_conn.CreateCommand())
					{
						_sql_cmd.CommandText = "create table if not exists \"tab\" (\"tmsp\" text);";
						_sql_cmd.ExecuteNonQuery();

						while (true)
						{
							Thread.Sleep(1000);
							_sql_cmd.CommandText = "insert into tab values(@tmsp);";
							_sql_cmd.Parameters.AddWithValue("@tmsp", DateTime.Now.ToString());
							_sql_cmd.ExecuteNonQuery();
							Console.WriteLine("Success!");
						}
					}
				}
				catch(Exception e)
				{
					Console.WriteLine(e.ToString());
				}
			}
		}
	}
}
