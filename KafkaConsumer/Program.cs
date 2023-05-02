using KafkaConsumer.DAL;
using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Helper;
using KafkaConsumer.Interfaces;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
//swager config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//swager config

//database config
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<MyHub>();
//database config


/* kafka service config*/
//builder.Services.AddSingleton<IHostedService, KafkaConsumerService>();
//builder.Services.AddHostedService<KafkaConsumerService>();
/* kafka service config*/


builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IFactoryRepository, FactoryRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
builder.Services.AddScoped<IStatusRecordRepository, StatusRecordRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddScoped<JwtHelper>();
builder.Services.AddSignalR();
//Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"])),
            ValidateIssuer = false,
            //ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidateAudience = false,
            //ValidAudience = builder.Configuration["JwtConfig:Audience"],
        };
    });
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kafka consumer API");
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(options => options
                        .WithOrigins("http://localhost:3000", "http://localhost:8080", "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        );
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MyHub>("/myhub");

    //endpoints.MapRazorPages();
});

app.Run();
