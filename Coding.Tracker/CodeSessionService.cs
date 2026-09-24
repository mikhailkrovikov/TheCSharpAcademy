using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Coding.Tracker
{
    public class CodeSessionService : ICodeSessionService
    {
        private readonly string connectionString = "DataSource=coding_tracker.db";
        private readonly string tableName = "sessions";

        public void CreateDatabase()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $@"CREATE TABLE IF NOT EXISTS {tableName} 
                            (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTime TEXT,
                                EndTime TEXT,
                                Duration TEXT
                            )";
                db.Execute(query);
            };
        }

        public void Create(CodeSession session)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $"INSERT INTO {tableName}(StartTime, EndTime, Duration) VALUES(@StartTime, @EndTime, @Duration)";
                db.Execute(query, session);
            }
        }

        public List<CodeSession> ReadAllData()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $"SELECT * FROM {tableName}";
                return db.Query<CodeSession>(query).ToList();
            }
        }

        public void Update(CodeSession session)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $"UPDATE {tableName} SET StartTime=@StartTime, EndTime=@EndTime, Duration=@Duration";
                db.Execute(query, session);
            }
        }

        public void Delete(int id)
        {
            using(IDbConnection db = new SqliteConnection(connectionString))
            {
                var query = $"DELETE FROM {tableName} WHERE Id=@Id";
                db.Execute(query, new { id });
            }
        }
    }
}
