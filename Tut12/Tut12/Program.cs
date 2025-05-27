using Microsoft.EntityFrameworkCore;
using Tut12.Data;
using Tut12.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApbdContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IClientService, ClientService>();
var app = builder.Build();



app.UseAuthorization();
app.MapControllers();
app.Run();