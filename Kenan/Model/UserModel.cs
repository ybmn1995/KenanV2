using System.Text.Json.Serialization;

public class UserModel
{
    [JsonPropertyName("__from_launcher__")]
    public int FromLauncher { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("machine_id")]
    public string MachineId { get; set; }

    [JsonPropertyName("os")]
    public string OS { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
