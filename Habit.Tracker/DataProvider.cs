using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Habit.Tracker
{
    public class DataProvider
    {
        private readonly string connectionString = "DataSource=fetching_tracker.db";

        public void CreateDatabase()
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



        public void Create(Record record)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO fetching(DateTime, Count) VALUES('{record.DateTime.ToString("dd-MM-yy")}', {record.Count})";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<string> ReadAllData()
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

        public void Update(int id, Record record)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE fetching SET DateTime='{record.DateTime.ToString("dd-MM-yy")}', Count={record.Count} WHERE Id={id}";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM fetching WHERE Id = {id}";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
