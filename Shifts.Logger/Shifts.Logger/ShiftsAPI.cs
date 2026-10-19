using System.Net.Http.Json;

namespace Shifts.Logger
{
    public static class ShiftsAPI
    {
        private static HttpClient _client = new();

        public static async Task<List<Shift>?> GetShifts()
        {
            return await _client.GetFromJsonAsync<List<Shift>>("https://localhost:7093/api/Shift");
        }

        public static async Task<Shift?> GetShiftById(int id)
        {
            return await _client.GetFromJsonAsync<Shift>($"https://localhost:7093/api/Shift/{id}");
        }

        public static async Task<Shift> CreateShift(Shift shift)
        {
            var response = await _client.PostAsJsonAsync<Shift>("https://localhost:7093/api/Shift", shift);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Shift>();
            if (created == null)
            {
                throw new InvalidOperationException($"Error occures with creating shift {shift}");
            }
            return created;
        }

        public static async Task<Shift> Update(int id, Shift shift)
        {
            var response = await _client.PutAsJsonAsync($"https://localhost:7093/api/Shift/{id}", shift);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Shift>();
            if (created == null)
            {
                throw new InvalidOperationException($"Error occures with creating shift {shift}");
            }
            return created;
        }

        public static async Task DeleteShift(int id)
        {
            await _client.DeleteAsync($"https://localhost:7093/api/Shift/{id}");
        }
    }
}