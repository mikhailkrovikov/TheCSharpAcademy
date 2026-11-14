using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water.Logger.Models;

namespace Water.Logger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel? DrinkingWater { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetRecordById(id);
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
            if(!ModelState.IsValid)
            {
                return Page();
            }
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE DrinkingWater SET Date = @Date, Quantity = @Quantity WHERE Id = @Id";
                command.Parameters.AddWithValue("@Date", DrinkingWater.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            return RedirectToPage("/Index");
        }
    }
}
