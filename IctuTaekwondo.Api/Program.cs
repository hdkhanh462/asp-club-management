using System.Text;
using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Shared.Filters;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Services;
using IctuTaekwondo.Shared.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using IctuTaekwondo.Api.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); 
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Tắt xử lý lỗi tự động
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ICTU Taekwondo API", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    //options.Password.RequireDigit = false;
    //options.Password.RequireLowercase = false;
    //options.Password.RequireUppercase = false;
    //options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<VietnameseIdentityErrorDescriber>();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Add Scoped Services
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IUploadFileService , UploadFileService>();
builder.Services.AddScoped<IAuthService ,AuthService>();
builder.Services.AddScoped<IAccountService ,AccountService>();
builder.Services.AddScoped<IEventRegisterationService, EventRegisterationService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

var uploadPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");

if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadPath),
    RequestPath = "/static"
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

// Seed dữ liệu Identity
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentityDataSeeder.SeedRolesAndAdminUser(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
