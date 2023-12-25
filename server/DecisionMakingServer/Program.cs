


// Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;


using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Tests;


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

        // (string st, Status s) = requestManager.Login(new UserLoginDTO
        // {
        //     Username = "oilymacaroni",
        //     Password = "3bulkiminus1"
        // });
        (string st, Status s) = requestManager.Login(new UserLoginDTO
        {
            Username = "aaa",
            Password = "bbb"
        });
        Console.WriteLine(st);
        int userId = requestManager.GetUserId(st);
        rankingRepository.ListUserRankings(userId);

        // var tester = new ControllerTest();
        // //tester.AddRankingTest(DummyData.NoIdRanking);
        // var ranking = tester.GetRankingTest(9);
        // if (ranking == null) return;
        // Console.WriteLine(ranking.Alternatives.Count);
        // tester.AddAnswersTest(DummyData.PostDto(ranking));

        //
        // UNCOMMENT BELOW TO RUN IN CONTINOUS MODE 
        //


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
        //
        // app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        // app.MapControllers();
        // app.Run();

    }
}
