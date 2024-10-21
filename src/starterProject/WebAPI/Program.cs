using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Persistence;
using Application;
using Infrastructure;
using Core.Mailing;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Security.JWT;
using Core.CrossCuttingConcerns.Exception.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Encryption;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddApplicationServices(mailSettings:
            builder.Configuration.GetSection("MailSettings").Get<MailSettings>()
            ?? throw new InvalidOperationException("MailSettings section cannot found in configuration."),
            mongoDbConfiguration:
            builder.Configuration.GetSection("SeriLogConfigurations:MongoDbConfiguration").Get<MongoDbConfiguration>()
            ?? throw new InvalidOperationException("MongoDbConfiguration section cannot found in configuration."),
            tokenOptions:
            builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>()
            ?? throw new InvalidOperationException("TokenOptions section cannot found in configuration."));

        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddInfrastructureServices();
        builder.Services.AddHttpContextAccessor();

        const string tokenOptionsConfigurationSection = "TokenOptions";
        TokenOptions tokenOptions = builder.Configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new InvalidOperationException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration.");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
        });

        builder.Services.AddDistributedMemoryCache();
        //builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(opt => opt.AddDefaultPolicy(p =>
        {
            p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));

        const string openApiSecurityScheme = "oauth2", openApiSecurityName = "Bearer";
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                + "`Enter your token in the text input below.`"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = openApiSecurityName
                        },
                        Scheme = openApiSecurityScheme,
                        Name = openApiSecurityName,
                        In = ParameterLocation.Header,
                    },
                    Array.Empty<string>()
                }
            });
        });

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.DocExpansion(DocExpansion.None);
            });
        }

        if (app.Environment.IsProduction())
            app.ConfigureCustomExceptionMiddleware();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        const string webApiConfigurationSection = "WebAPIConfiguration";
        WebApiConfiguration webApiConfiguration =
            app.Configuration.GetSection(webApiConfigurationSection).Get<WebApiConfiguration>()
            ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\" section cannot found in configuration.");

        app.UseCors(opt =>
        opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

        app.Run();
    }
}
