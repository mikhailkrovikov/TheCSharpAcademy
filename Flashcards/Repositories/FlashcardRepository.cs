using Dapper;
using Flashcards.Entities;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Flashcards.Repositories
{
    public class FlashcardRepository
    {
        private readonly string connectionString;
        private readonly string table = "cards";

        public FlashcardRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool CreateDatabase()
        {
            return Execute(db =>
            {
                var query = $@"CREATE TABLE IF NOT EXISTS {table} 
                            (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Front TEXT,
                                Back TEXT,
                                CardStackId INTEGER,
                                FOREIGN KEY (CardStackId) REFERENCES stacks (Id) ON DELETE CASCADE
                            )";
                return db.Execute(query) > 0;
            });
        }

        private bool Execute(Func<IDbConnection, bool> action)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                return action(db);
            }
        }

        public bool CreateFlashcard(FlashcardEntity flashcard)
        {
            return Execute(db =>
            {
                var query = $"INSERT INTO {table}(Front, Back, CardStackId) VALUES(@Front, @Back, @CardStackId);";
                return db.Execute(query, flashcard) > 0;
            });
        }

        public FlashcardEntity? GetFlashcard(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                return db.Query<FlashcardEntity>($"SELECT * FROM {table} WHERE Id=@id", new { id }).FirstOrDefault();
            }
        }

        public List<FlashcardEntity> ReadFlashcards(int stackId)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                return db.Query<FlashcardEntity>($"SELECT * FROM {table} WHERE CardStackId=@stackId", new { stackId }).ToList();
            }
        }

        public bool UpdateFlashcard(FlashcardEntity flashcard)
        {
            return Execute(db =>
            {
                var query = $"UPDATE {table} SET Front=@Front, Back=@Back WHERE Id=@Id";
                return db.Execute(query, flashcard) > 0;
            });
        }

        public bool DeleteFlashcard(int id)
        {
            return Execute(db =>
            {
                var query = $"DELETE FROM {table} WHERE Id=@id";
                return db.Execute(query, new { id }) > 0;
            });
        }
    }
}
