using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water.Logger.Models;

namespace Water.Logger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }


        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO DrinkingWater (Date, Quantity) VALUES (@Date, @Quantity)";
                command.Parameters.AddWithValue("@Date", DrinkingWater.Date);
                command.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
                command.ExecuteNonQuery();
            }
            return RedirectToPage("/Index");
        }
    }
}
