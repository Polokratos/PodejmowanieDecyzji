using System.ComponentModel.DataAnnotations;

namespace DecisionMakingServer.APIModels;
using Newtonsoft.Json;

public class UserLoginDTO
{
    [JsonProperty("username")] public string Username { get; set; } = null!;
    [JsonProperty("password")] public string Password { get; set; } = null!;
}