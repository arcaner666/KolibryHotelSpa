using BusinessLayer.Utilities.Security.Encryption;
using BusinessLayer.Utilities.Security.JWT;
using Entities.ConfigurationModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class ServiceExtensions
{
    //Production CORS
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(
                    "https://www.kolibryhotelspa.com",
                    "https://kolibryhotelspa.com"
                )
                .WithMethods("GET", "POST", "DELETE")
                .AllowAnyHeader());
        });

    // Development CORS
    //public static void ConfigureCors(this IServiceCollection services) =>
    //    services.AddCors(options =>
    //    {
    //        options.AddPolicy("CorsPolicy", builder => builder
    //            .AllowAnyOrigin()
    //            .AllowAnyMethod()
    //            .AllowAnyHeader());
    //    });

    public static void ConfigureFileTransferOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var fileTransferOptions = configuration.GetSection("FileTransferOptions").Get<FileTransferOptions>();
        services.Configure<FormOptions>(options =>
        {
            // 1MB dosya yükleme limiti koydum. 1024 X 1024 = 1048576 b = 1024 kb = 1 mb
            options.ValueLengthLimit = fileTransferOptions.UploadLimit;
            options.MultipartBodyLengthLimit = fileTransferOptions.UploadLimit;
            options.MemoryBufferThreshold = fileTransferOptions.UploadLimit;
        });
    }

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
    }
}
