


// Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;


using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;
using Microsoft.AspNetCore.Mvc;
using Controller = DecisionMakingServer.Controllers.Controller;

namespace DecisionMakingServer;

public class Program
{
    public static void Main(string[] args)
    {
        using var db = DbContextProvider.DbContext;
        
        RequestManager requestManager = new RequestManager();
        UserRepository userRepository = new();
        RankingRepository rankingRepository = new();
        
        userRepository.ListAll();

        (string st, Status s) = requestManager.Login(new UserLoginDTO
        {
            Username = "oilymacaroni",
            Password = "3bulkiminus1"
        });
        Console.WriteLine(st);
        int userId = requestManager.GetUserId(st);
        rankingRepository.ListUserRankings(userId);

        // RankingPostDTO dto = new RankingPostDTO
        // {
        //     RankingId = 5,
        //     Answers = new []
        //     {
        //         new RankingAnswerDTO
        //         {
        //             CriterionId = 5,
        //             LeftAlternativeId = 1,
        //             RightAlternativeId = 2,
        //             Value = 3
        //         },
        //         new RankingAnswerDTO
        //         {
        //             CriterionId = 5,
        //             LeftAlternativeId = 2,
        //             RightAlternativeId = 3,
        //             Value = -1
        //         }
        //     }
        // };
        // requestManager.AddRankingResults(dto, st);


        // var builder = WebApplication.CreateBuilder(args);
        // // Add services to the container.
        // builder.Services.AddControllers();
        // // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        // var app = builder.Build();
        // // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        // app.MapControllers();
        // app.Run();
    }
}
