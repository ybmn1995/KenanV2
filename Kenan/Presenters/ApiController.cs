using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kenan.Controller
{
    public class ApiController
    {
        private readonly string apiUrl;

        public ApiController(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        public async Task<string> SendLoginAsync(string email, string machineId, string os, string password, string serverIp)
        {
            var payload = new
            {
                email,
                machine_id = machineId,
                os,
                password,
                server_ip = serverIp
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using var client = new HttpClient();
                var response = await client.PostAsync(apiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
