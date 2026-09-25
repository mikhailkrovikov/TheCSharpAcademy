using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Coding.Tracker
{
    public class CodeSessionService : ICodeSessionService
    {   
        private readonly string table = "sessions";
        private readonly string connectionString;
        public CodeSessionService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateDatabase()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $@"CREATE TABLE IF NOT EXISTS {table} 
                            (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTime TEXT,
                                EndTime TEXT,
                                Duration TEXT
                            )";
                return db.Execute(query) > 0;
            };
        }

        public bool Create(CodeSession session)
        {
            using IDbConnection db = new SqliteConnection(connectionString);
            var query = $"INSERT INTO {table}(StartTime, EndTime, Duration) VALUES(@StartTime, @EndTime, @Duration)";
            return db.Execute(query, session)  > 0;
        }

        public List<CodeSession> ReadAllData()
        {
            using IDbConnection db = new SqliteConnection(connectionString);
            var query = $"SELECT * FROM {table}";
            return db.Query<CodeSession>(query).ToList();
        }

        public bool Update(CodeSession session)
        {
            using IDbConnection db = new SqliteConnection(connectionString);
            var query = $"UPDATE {table} SET StartTime=@StartTime, EndTime=@EndTime, Duration=@Duration";
            return  db.Execute(query, session) > 0;
        }

        public bool Delete(int id)
        {
            using IDbConnection db = new SqliteConnection(connectionString);
            var query = $"DELETE FROM {table} WHERE Id=@id";
            return db.Execute(query, new { id }) > 0;
        }
    }
}
