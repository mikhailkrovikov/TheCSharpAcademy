using Dapper;
using Flashcards.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Flashcards.Repositories
{
    public class CardStackRepository
    {
        private readonly string connectionString;
        private readonly string table = "stacks";

        public CardStackRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateCardStack(CardStackEntity cardStack)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"INSERT INTO {table}(Name) VALUES(@Name);";
                db.Execute(query, cardStack);
            }
        }

        public CardStackEntity? GetCardStack(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<CardStackEntity>($"SELECT * FROM {table} WHERE Id=@id", new { id }).FirstOrDefault();
            }
        }

        public List<CardStackEntity> ReadCardStacks()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<CardStackEntity>($"SELECT * FROM {table}").ToList();
            }
        }

        public void UpdateCardStack(CardStackEntity cardStack)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"UPDATE {table} SET Name=@Name WHERE Id=@Id";
                db.Execute(query, cardStack);
            }
        }

        public void DeleteCardStack(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = $"DELETE FROM {table} WHERE Id=@Id";
                db.Execute(query, new { id });
            }
        }
    }
}
