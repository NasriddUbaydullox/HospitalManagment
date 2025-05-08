using HospitalManagment_V2.classes;
using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.Examples;
using HospitalManagment_V2.JwtSetting;
using HospitalManagment_V2.MapperProfile;
using HospitalManagment_V2.Mediator.Doctors.CreateDoctor;
using HospitalManagment_V2.Mediator.Doctors.GetDoctorById;
using HospitalManagment_V2.Middleware;
using HospitalManagment_V2.Repository;
using HospitalManagment_V2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sats.PostgreSqlDistributedCache;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Very Good Thing",
        Description = "Very Very Good"
    });
    options.ExampleFilters();
});

builder.Services.Configure<FileStorage>(builder.Configuration.GetSection("MedicalRecordsPath"));
builder.Services.Configure<FileStorage>(builder.Configuration.GetSection("ReportsPath"));

builder.Services.Configure<AppointmentSettings>(builder.Configuration.GetSection("CancellationDeadlineHours"));
builder.Services.Configure<AppointmentSettings>(builder.Configuration.GetSection("NotificationReminderHours"));

builder.Services.AddSwaggerExamplesFromAssemblyOf<DoctorExample>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetDoctorByIdQueryHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<ICorroletionId, CorreletionId>();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

builder.Services.AddAuthorization();

// RateLimiter

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    //rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    //{
    //    options.PermitLimit = 10; // 10 ta request
    //    options.Window = TimeSpan.FromSeconds(10);  // 10 sekund ichida
    //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;  // 1 chi kirgan 1 chi chiqadi
    //    options.QueueLimit = 5;  // agar 10 ta request kelsa ularning 5 tasi ishlaydi qogani kutadi 5 tani ignore qmedi
    //});
    //rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>

    //    RateLimitPartition.GetFixedWindowLimiter(
    //        partitionKey: httpContext.Connection.RemoteIpAddress.ToString(),
    //        factory: _ => new FixedWindowRateLimiterOptions
    //        {
    //            //AutoReplenishment = true,
    //            PermitLimit = 5,
    //            //QueueLimit = 0,
    //            Window = TimeSpan.FromSeconds(10)
    //        }));

    rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.SegmentsPerWindow = 2;
    });

    //rateLimiterOptions.AddTokenBucketLimiter("tokenBucket", options =>
    //{
    //    options.TokenLimit = 5;
    //    options.ReplenishmentPeriod = TimeSpan.FromSeconds(3);
    //    options.TokensPerPeriod = 3;
    //    options.AutoReplenishment = true;
    //});
});

//builder.Services.AddPostgresDistributedCache(options =>
//{
//    options.ConnectionString = "Server=localhost;Port=5432;Database=HospitalManagmentV2;User Id=postgres;Password=postgres;";
//});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "test";
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("frontend",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:3000") //URL
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<Context>(options =>
{
    options
        .UseNpgsql("Server=localhost;Port=5432;Database=HospitalManagmentV2;User Id=postgres;Password=postgres;")
        .LogTo(Console.WriteLine, LogLevel.Information);
});


#region Serilog
// Serilog konfiguratsiyasi
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Logs to console
    .WriteTo.Seq("http://localhost:5341")  // Logs to Seq
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

// Replace default logging with Serilog
builder.Host.UseSerilog();
#endregion

#region JWT

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //options.Authority = "cafe.uz";
        options.Audience = "cafe.uz";

        string signInKey = builder.Configuration["Jwt:Key"];

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidAudiences = ["cafe.uz", "mobile.cafe.uz"],
            ValidIssuers = ["cafe.uz"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey))
        };
    });

builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration("Jwt");

builder.Services.AddScoped<IAuthService,AuthService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseRateLimiter();

app.UseSerilogRequestLogging();

app.UseMiddleware<CorreletionIdMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true; // Serialize as Swagger 2.0
    });
}   

//app.UseCors("frontend");  //Cors

app.MapControllers();

app.Run();
