using System;
using System.Text;
using CollectionManager.WEB.Mapper;
using CollectionsManager.BLL.MapperProfiles;
using CollectionsManager.BLL.Services;
using CollectionsManager.BLL.Services.AuthService;
using CollectionsManager.BLL.Services.AuthService.Options;
using CollectionsManager.BLL.Services.ImageService;
using CollectionsManager.BLL.Services.ImageService.Options;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CollectionManager.WEB.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("LocalDb"));
                opts.EnableSensitiveDataLogging();
            });
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureImageStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleCloudStorageOptions>(configuration.GetSection(nameof(GoogleCloudStorageOptions)));
            services.AddScoped<IImageStorageService, CloudStorageService>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(CollectionsProfile),
                typeof(CommentsProfile),
                typeof(CustomFieldsProfile),
                typeof(ItemsProfile),
                typeof(TagsProfile),
                typeof(LikesProfile),
                typeof(WebProfile));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }

        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            var secretKey = Environment.GetEnvironmentVariable("SECRET", EnvironmentVariableTarget.Machine);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false,
                    ValidateIssuer = false,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true,
                };
            });
        }
    }
}
