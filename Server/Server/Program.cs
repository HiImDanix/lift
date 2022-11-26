using GuessingGame.Repositories;
using GuessingGame.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // inject repositories & pass in connection string
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddTransient<IPlayerRepository, PlayerRepository>(provider => new PlayerRepository(connectionString));
    builder.Services.AddTransient<IRoomRepository, RoomRepository>(provider => new RoomRepository(connectionString));
    
    // Inject services
    builder.Services.AddSingleton<IRoomService, RoomService>();
    
}



var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}