using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Habit.Tracker
{
    public static class DataProvider
    {
        private static string connectionString = "DataSource=fetching_tracker.db";

        public static void CreateDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var table = connection.CreateCommand();
                table.CommandText =
                    @"CREATE TABLE IF NOT EXISTS fetching 
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DateTime Data,
                        Count INTEGER
                    )";
                table.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<string> GetAllData()
        {
            var data = new List<string>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM fetching";
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var record = new Record
                        {
                            Id = reader.GetInt32(0),
                            DateTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            Count = reader.GetInt32(2)
                        };
                        data.Add(record.ToString());
                    }
                }
                else
                {
                    reader.Close();
                    throw new ArgumentException("No records :(");
                }
                reader.Close();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        public static void AddDate(Record habit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO fetching(DateTime, Count) VALUES('{habit.DateTime.ToString("dd-MM-yy")}', {habit.Count})";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
