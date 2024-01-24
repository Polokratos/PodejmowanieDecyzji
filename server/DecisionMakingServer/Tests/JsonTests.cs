using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;

namespace DecisionMakingServer.Tests;

public static class JsonTests
{
    private static UserLoginDTO _userLoginDto = new()
    {
        Username = "aaa",
        Password = "bbb"
    };

    public static void Run()
    {
        
        (string sessionToken, Status _) = RequestManager.Login(_userLoginDto);
        (string? json, Status _) = RequestManager.GetJson(sessionToken, 23);
        
        Console.WriteLine(json);
    }
}