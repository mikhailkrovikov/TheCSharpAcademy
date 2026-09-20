using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Habit.Tracker
{
    public class DataProvider
    {
        private readonly string connectionString = "DataSource=fetching_tracker.db";

        private void ExecuteCommand(Action<SqliteCommand> action)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            action(command);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void CreateDatabase()
        {
            ExecuteCommand(c =>
            {
                c.CommandText =
                    @"CREATE TABLE IF NOT EXISTS fetching 
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DateTime Data,
                        Count INTEGER
                    )";
            });
        }

        public void Create(Record record)
        {
            ExecuteCommand(c => c.CommandText = $"INSERT INTO fetching(DateTime, Count) VALUES('{record.DateTime:dd-MM-yy}', {record.Count})");
        }

        public List<string> ReadAllData()
        {
            var data = new List<string>();
            ExecuteCommand(c =>
            {
                c.CommandText = "SELECT * FROM fetching";
                var reader = c.ExecuteReader();
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
            });
            return data;
        }

        public void Update(int id, Record record)
        {
            ExecuteCommand(c => c.CommandText = $"UPDATE fetching SET DateTime='{record.DateTime:dd-MM-yy}', Count={record.Count} WHERE Id={id}");
        }

        public void Delete(int id)
        {
            ExecuteCommand(c => c.CommandText = $"DELETE FROM fetching WHERE Id = {id}");
        }
    }
}
