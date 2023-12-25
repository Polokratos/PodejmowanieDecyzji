using System.Text;
using DecisionMakingServer.APIModels;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;

namespace DecisionMakingServer.Session;

public class SessionManager
{
    private readonly UserRepository _userRepository = new();
    private readonly Dictionary<string, int> _sessions = new();

    private string GenerateSessionToken()
    {
        return Guid.NewGuid().ToString();
    }

    public int GetUserId(string sessionToken)
    {
        if (!_sessions.ContainsKey(sessionToken))
            return -1;
        return _sessions[sessionToken];
    }
    
    public Status AddUser(string username, string password)
    {
        byte[] bytePassword = Encoding.ASCII.GetBytes(password);
        return _userRepository.AddUser(username, bytePassword);
    }
    
    public (string, Status) Login(string username, string password)
    {
        User? user = _userRepository.GetUser(username);
        if (user is null)
            return ("", Status.InvalidUsername);

        byte[] bytePassword = Encoding.ASCII.GetBytes(password);
        if (!bytePassword.SequenceEqual(user.Password))
            return ("", Status.InvalidPassword);

        string token = GenerateSessionToken();
        _sessions.Add(token, user.UserId);
        return (token, Status.Ok);
    }
}