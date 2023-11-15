


// Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;


namespace DecisionMakingServer;

public class Program
{
    public static void Main(string[] args)
    {
        using var db = new DecisionDbContext();
        
        var query = from u in db.Users select u.Username;
        Console.WriteLine("All users in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item);
        }
        
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
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }
}
