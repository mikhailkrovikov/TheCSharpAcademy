using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Habit.Tracker
{

    public class SQLiteDataProvider : IDataProvider<Record>
    {
        private readonly string connectionString = "DataSource=fencing_tracker.db";
        private readonly string tableName = "fencing";

        private bool ExecuteCommand(Action<SqliteCommand> action)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            action(command);
            var commands = command.ExecuteNonQuery();
            connection.Close();
            return commands > 0;
        }

        public bool CreateDatabase()
        {
            return ExecuteCommand(c =>
            {
                c.CommandText =
                   $@"CREATE TABLE IF NOT EXISTS {tableName} 
                   (
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                       DateTime Data,
                       Count INTEGER
                   )";
            });
        }

        public bool Create(Record record)
        {
            return ExecuteCommand(c => c.CommandText = $"INSERT INTO {tableName}(DateTime, Count) VALUES('{record.DateTime:dd-MM-yy}', {record.Count})");
        }

        public List<Record> ReadAllData()
        {
            var data = new List<Record>();
            ExecuteCommand(c =>
            {
                c.CommandText = $"SELECT * FROM {tableName}";
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
                        data.Add(record);
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

        public bool Update(int id, Record record)
        {
            return ExecuteCommand(c => c.CommandText = $"UPDATE {tableName} SET DateTime='{record.DateTime:dd-MM-yy}', Count={record.Count} WHERE Id={id}");
        }

        public bool Delete(int id)
        {
            return ExecuteCommand(c => c.CommandText = $"DELETE FROM {tableName} WHERE Id = {id}");
        }
    }
}
