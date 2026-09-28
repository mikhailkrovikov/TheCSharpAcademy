using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Flashcards
{
    public class FlashcardService
    {
        private readonly string connectionString;
        private readonly string table = "cards";

        public FlashcardService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateFlashcard(FlashcardEntity flashcard, CardStackEntity cardStack)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                flashcard.CardStackId = cardStack.Id;
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

        public List<FlashcardEntity> ReadFlashcards()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FlashcardEntity>($"SELECT * FROM {table}").ToList();
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
