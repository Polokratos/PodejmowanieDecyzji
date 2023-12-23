


// Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;


using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Repositories;


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
