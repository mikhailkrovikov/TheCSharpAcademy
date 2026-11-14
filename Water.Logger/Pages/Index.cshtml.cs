using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water.Logger.Models;

namespace Water.Logger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<DrinkingWaterModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            var records = new List<DrinkingWaterModel>();
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM DrinkingWater";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new DrinkingWaterModel
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.Parse(reader.GetString(1)),
                            Quantity = reader.GetInt32(2)
                        };
                        records.Add(record);
                    }
                }
            }
            Records = records;
            return records;
        }
    }
}
