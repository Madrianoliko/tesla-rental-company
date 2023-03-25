using Microsoft.EntityFrameworkCore;
using Serilog;
using TeslaRentalCompany.API;
using TeslaRentalCompany.API.DbContexts;
using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.API.Services;
using TeslaRentalCompany.Data;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/reservationInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers(controllerOptions =>
{
    //controllerOptions.ReturnHttpNotAcceptable = true;
})
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    })
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMailService, LocalMailService>();
builder.Services.AddSingleton<ISeedDataService, SeedDataService>();

builder.Services.AddDbContext<TeslaRentalCompanyContext>(
    dbContextOptions => dbContextOptions.UseSqlServer( 
        builder.Configuration["ConnectionStrings:TeslaCarCompanyConnectionString"]));

builder.Services.AddScoped<ITeslaRentalCompanyRepository, TeslaRentalCompanyRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
