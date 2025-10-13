using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text;
using WallpaperApi.Data;
using WallpaperApi.Repository;
using WallpaperApi.Services;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi.Services.Repository;
using WallpaperApi.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var jwtCfg = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtCfg["Key"];
var jwtIssuer = jwtCfg["Issuer"];
var jwtAudience = jwtCfg["Audience"];

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<ICreatorRepository, CreatorRepository>();
builder.Services.AddScoped<ICreatorService, CreatorService>();
builder.Services.AddScoped<IWallpaperRepository, WallpaperRepository>();
builder.Services.AddScoped<IWallpaperService, WallpaperService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IResolutionRepository, ResolutionRepository>();

// ✅ Add CORS here (before Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // or restrict with .WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };

    // Read token from cookie named "AuthToken"
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // If token is in cookie, use it
            var cookieToken = context.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(cookieToken))
            {
                context.Token = cookieToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    //RequestPath = "/Resources"
});

app.UseHttpsRedirection();

// ✅ Use CORS here
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
