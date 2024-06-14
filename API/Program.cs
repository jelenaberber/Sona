using Application;
using API;
using DataAccess;
using Implementation;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using API.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = new AppSettings();

builder.Configuration.Bind(settings);

builder.Services.AddSingleton(settings.Jwt);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Context>(x => new Context(settings.ConnectionString));
builder.Services.AddScoped<IDbConnection>(x => new SqlConnection(settings.ConnectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtTokenCreator>();
builder.Services.AddUseCases();
builder.Services.AddTransient<IExceptionLogger, DbExceptionLogger>();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();


builder.Services.AddScoped<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<Context>();

    return new JwtApplicationActorProvider(authHeader);
});
builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.Jwt.Issuer,
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    cfg.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            //Token dohvatamo iz Authorization header-a

            Guid tokenId = context.HttpContext.Request.GetTokenId().Value;

            var storage = builder.Services.BuildServiceProvider().GetService<ITokenStorage>();

            if (!storage.Exists(tokenId))
            {
                context.Fail("Invalid token");
            }


            return Task.CompletedTask;

        }
    };
});

var app = builder.Build();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
