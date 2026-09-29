using Dapper;
using Flashcards.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
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
        public bool CreateDatabase()
        {
            return Execute(db =>
            {
                var query = $@"CREATE TABLE IF NOT EXISTS {table} 
                            (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT
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

        public bool CreateCardStack(CardStackEntity cardStack)
        {
            return Execute(db =>
            {
                var query = $"INSERT INTO {table}(Name) VALUES(@Name);";
                return db.Execute(query, cardStack) > 0;
            });
        }

        public CardStackEntity? GetCardStack(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                return db.Query<CardStackEntity>($"SELECT * FROM {table} WHERE Id=@id", new { id }).FirstOrDefault();
            }
        }

        public List<CardStackEntity> ReadCardStacks()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                return db.Query<CardStackEntity>($"SELECT * FROM {table}").ToList();
            }
        }

        public bool UpdateCardStack(CardStackEntity cardStack)
        {
            return Execute(db =>
            {
                var query = $"UPDATE {table} SET Name=@Name WHERE Id=@Id";
                return db.Execute(query, cardStack) > 0;
            });
        }

        public bool DeleteCardStack(int id)
        {
            return Execute(db =>
            {
                var query = $"DELETE FROM {table} WHERE Id=@id";
                return db.Execute(query, new { id }) > 0;
            });
        }
    }
}
