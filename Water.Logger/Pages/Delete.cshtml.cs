using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water.Logger.Models;

namespace Water.Logger.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel? DrinkingWater { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetRecordById(id);
            if (DrinkingWater == null)
            {
                return NotFound();
            }
            return Page();
        }

        public DrinkingWaterModel? GetRecordById(int id)
        {
            DrinkingWaterModel record = null;
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM DrinkingWater WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        record = new DrinkingWaterModel
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.Parse(reader.GetString(1)),
                            Quantity = reader.GetInt32(2)
                        };
                    }
                }
            }
            return record;
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM DrinkingWater WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            return RedirectToPage("/Index");
        }
    }
}
