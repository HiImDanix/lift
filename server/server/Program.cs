using System.Data;
using GuessingGame;
using GuessingGame.Repositories;
using GuessingGame.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
    
    
    // =============================
    // ============ DB =============
    // =============================
    
    // Get connection string from appsettings.json
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // Inject connection per HTTP request
    // TODO: Inject factory instead
    builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
    
    // DB helper for transactions
    builder.Services.AddTransient<IDbHelper, DbHelper>();
    
    // =============================
    // ======== Automapper =========
    // =============================
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    

    // =============================
    // ======= Repositories ========
    // =============================
    builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
    builder.Services.AddTransient<IRoomRepository, RoomRepository>();
    
    // =============================
    // ======== Services ===========
    // =============================
    builder.Services.AddSingleton<ILobbyService, LobbyService>();
    
}



var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Cors
    app.UseCors("AllowAll");
    // handle errors
    app.UseExceptionHandler("/error");
    // handle 404
    app.UseStatusCodePagesWithReExecute("/error/{0}");



    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}