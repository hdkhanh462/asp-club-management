using System.Text;
using Htmx;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.Shared.Services.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
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

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
                {
                    context.Token = context.Request.Cookies[GlobalConst.CookieAuthTokenKey];
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Htmx", policy =>
    {
        policy.WithOrigins("http://localhost:5000", "http://fronttaekwondo-001-site1.anytempurl.com")
                .WithHeaders(HtmxRequestHeaders.Keys.All)
                .WithExposedHeaders(HtmxResponseHeaders.Keys.All);
    });
});

// Add Scoped Services
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IApiService>(new ApiService(new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiUrl"]!),
    Timeout = TimeSpan.FromMinutes(1),
}));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<IRegisterationService, RegisterationService>();
builder.Services.AddScoped<IAchievementsService, AchievementsService>();
builder.Services.AddScoped<IFinancesService, FinancesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
