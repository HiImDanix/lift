using System.Data;
using GuessingGame;
using GuessingGame.hubs;
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
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
        });
    });
    
    // SignalR
    builder.Services.AddSignalR();
    
    
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
    builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
    builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();
    
    // =============================
    // ======== Services ===========
    // =============================
    builder.Services.AddSingleton<ILobbyService, LobbyService>();
    builder.Services.AddSingleton<IPlayerService, PlayerService>();
    builder.Services.AddSingleton<IQuestionService, QuestionService>();

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
    app.UseCors("CorsPolicy");
    // handle errors
    app.UseExceptionHandler("/error");
    // handle 404
    app.UseStatusCodePagesWithReExecute("/error/{0}");



    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
    // signalr
    app.MapHub<GameHub>("/hubs/game");
    app.Run();
}