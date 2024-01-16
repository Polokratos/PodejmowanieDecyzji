


// Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;


using System.Text;
using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Tests;


namespace DecisionMakingServer;

public class Program
{
    public static void Main(string[] args)
    {
        using var db = new DecisionDbContext();
        
        UserRepository userRepository = new();
        RankingRepository rankingRepository = new();
        // AAA BBB
        if (userRepository.GetUser("aaa") == null)
        {
            byte[] pass = Encoding.ASCII.GetBytes("bbb");
            userRepository.AddUser("aaa", pass);
        }
        // New ranking for AAA BBB
        int aaaid = userRepository.GetUser("aaa").UserId;
        if (rankingRepository.GetUserRankings(aaaid).ToList().Count == 0)
        {
            int rankingid = rankingRepository.AddRanking(DummyData.NoIdRanking);
            rankingRepository.AddUserRankingRole(aaaid, rankingid, UserRole.Owner);
        }
        
        userRepository.ListAll();
        rankingRepository.ListUserRankings(aaaid);

        //
        // UNCOMMENT BELOW TO RUN IN CONTINOUS MODE 
        //


        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();

    }
}
