using Dapper;
using Flashcards.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Flashcards.Repositories
{
    public class StudySessionRepository
    {
        private readonly string connectionString;
        private readonly string table = "sessions";
        public StudySessionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private bool Execute(Func<IDbConnection, bool> action)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return action(db);
            }
        }

        public bool CreateTable()
        {
            return Execute(db =>
            {
                var query =
                @$"IF NOT EXISTS 
                    (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{table}') AND type in (N'U'))
                    CREATE TABLE {table}
                    (
                        Id INT IDENTITY(1, 1) PRIMARY KEY,
                        CardStackId INT,
                        Time DATETIME,
                        Score INT,
                        FOREIGN KEY (CardStackId) REFERENCES stacks (Id) ON DELETE CASCADE
                    );";
                return db.Execute(query) != 0;
            });
        }

        public bool CreateSession(StudySessionEntity session)
        {
            return Execute((db) =>
            {
                var query = 
                @$"INSERT INTO {table}(CardStackId, Time, Score) 
                    VALUES(@CardStackId, @Time, @Score);";
                return db.Execute(query, session) > 0;
            });
        }
    }
}