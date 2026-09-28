using Dapper;
using Flashcards.Entities;
using Microsoft.Data.SqlClient;
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

        public void CreateFlashcard(FlashcardEntity flashcard)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"INSERT INTO {table}(Front, Back, CardStackId) VALUES(@Front, @Back, @CardStackId);";
                db.Execute(query, flashcard);
            }
        }

        public FlashcardEntity? GetFlashcard(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FlashcardEntity>($"SELECT * FROM {table} WHERE Id=@id", new { id }).FirstOrDefault();
            }
        }

        public List<FlashcardEntity> ReadFlashcards(int stackId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FlashcardEntity>($"SELECT * FROM {table} WHERE CardStackId=@stackId").ToList();
            }
        }

        public void UpdateFlashcard(FlashcardEntity flashcard)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"UPDATE {table} SET Front=@Front, Back=@Back WHERE Id=@Id";
                db.Execute(query, flashcard);
            }
        }

        public void DeleteFlashcard(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"DELETE FROM {table} WHERE Id=@Id";
                db.Execute(query, new { id });
            }
        }
    }
}
