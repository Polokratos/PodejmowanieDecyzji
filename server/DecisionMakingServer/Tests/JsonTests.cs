using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;

namespace DecisionMakingServer.Tests;

public static class JsonTests
{
    private static UserLoginDTO _userLoginDto = new UserLoginDTO
    {
        Username = "aaa",
        Password = "bbb"
    };

    public static void Run()
    {
        
        (string sessionToken, Status _) = RequestManager.Login(_userLoginDto);
        (string? json, Status _) = RequestManager.GetJson(sessionToken, 18);
        
        Console.WriteLine(json);
    }
}