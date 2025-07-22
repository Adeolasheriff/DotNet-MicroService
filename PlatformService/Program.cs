using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Service.Implemenatation;
using PlatformService.Service.Interface;
using PlatformService.Helper;
using PlatformService.SyncDataService.Http;

var builder = WebApplication.CreateBuilder(args);
// builder.WebHost.UseUrls("http://+:80");

var env = builder.Environment;
if (env.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PlatformsConn")));
    Console.WriteLine("--> Using PostgreSQL Db");
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));
    Console.WriteLine("--> Using InMemoryDb");

}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
Console.WriteLine($"--> Command Service Endpoint: {builder.Configuration["CommandService"]}");
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

PrepDb.PrepPopulation(app, env.IsProduction());

app.MapControllers();
// app.Urls.Add("http://*:80");

app.Run();

