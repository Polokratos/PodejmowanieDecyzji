using System.Collections.ObjectModel;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Repositories;

public class UserRepository : AbstractDbRepository
{
    public Status AddUser(string username, byte[] password)
    {
        User? user = DbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user is not null)
            return Status.AlreadyExistsInDb;
        
        DbContext.Users.Add(new User
        {
            Username = username,
            Password = password
        });

        return SaveChanges() == 1 
            ? Status.Ok 
            : Status.DatabaseAddError;
    }

    public User? GetUser(string username)
    {
        return DbContext.Users.FirstOrDefault(u => u.Username == username);
    }

    public IEnumerable<int> GetUsersByNames(IEnumerable<string> names)
    {
        return DbContext.Users.Where(u => names.Contains(u.Username)).Select(u => u.UserId).ToList();
    }

    public void ListAll()
    {
        Console.WriteLine("All users in database");
        var users = DbContext.Users.AsQueryable();
        foreach (var user in users)
        {
            Console.WriteLine($"{user.UserId, 5} | {user.Username, 20} | {user.Password[0]}...");
        }
    }
}