using SecureVotingSystem.Infrastructure.Data;
using SecureVotingSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using log4net;
using log4net.Config;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);
var getEntry = Assembly.GetEntryAssembly();
if (getEntry == null)
{
    throw new Exception("No entry assembly");
}

//add scope
builder.Services.AddScoped<IVoterRepository, VoterRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
//Entity framework
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var logRepository = LogManager.GetRepository(getEntry);
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// Retrieve the configured port
var port = builder.Configuration.GetValue<int>("AppConfig:Port", 5000); // 5000 is a default fallback
builder.WebHost.UseUrls($"http://localhost:{port}"); 



var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started at port :" + port);
//logging
string startupArt = @"
  ___ ___         _____                      .__  ._____.
 /   |   \ __ ___/ _________ _____    ____   |  | |__\_ |______________ _______ ___.__.
/    ~    |  |  \   __/     \\__  \  /    \  |  | |  || __ \_  __ \__  \\_  __ <   |  |
\    Y    |  |  /|  ||  Y Y  \/ __ \|   |  \ |  |_|  || \_\ |  | \// __ \|  | \/\___  |
 \___|_  /|____/ |__||__|_|  (____  |___|  / |____|__||___  |__|  (____  |__|   / ____|
       \/                  \/     \/     \/               \/           \/       \/
   _____             .___       ___.                   .__.__ ___.                  __
  /     \ _____    __| _/____   \_ |__ ___.__.    ____ |__|  |\_ |__   ____________/  |_
 /  \ /  \\__  \  / __ _/ __ \   | __ <   |  |   / ___\|  |  | | __ \_/ __ \_  __ \   __\
/    Y    \/ __ \/ /_/ \  ___/   | \_\ \___  |  / /_/  |  |  |_| \_\ \  ___/|  | \/|  |
\____|__  (____  \____ |\___  >  |___  / ____|  \___  /|__|____|___  /\___  |__|   |__|
        \/     \/     \/    \/       \/\/      /_____/             \/     \/
    ";


logger.LogInformation(Environment.NewLine + startupArt + Environment.NewLine);
logger.LogInformation("Welcome to System voting Compressor App!");
logger.LogInformation("--------------------------------------");
logger.LogInformation("Application starting up...");
logger.LogDebug("This is a debug message during startup.");
logger.LogInformation("Application environment: {EnvironmentName}", app.Environment.EnvironmentName);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();