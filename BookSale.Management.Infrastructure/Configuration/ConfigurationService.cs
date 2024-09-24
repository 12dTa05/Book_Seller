using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.Services;
using BookSale.Management.DataAccess.Dapper;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Management.DataAccess.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BookSaleManagementCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/Admin/Authentication/Login";
                options.SlidingExpiration = true;
                //options.AccessDeniedPath = "/";
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;

            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationUser>>();

            services.AddTransient<ISQLQueryHandler, SQLQueryHandler>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IGenreService, GenreService>();

            services.AddTransient<IBookService, BookService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<ICommonService, CommonService>();

        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddAuthorizationGlobal(this IServiceCollection services)
        {

            var autherizeAdmin = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddAuthorization(options => {
                options.AddPolicy("AuthorizedAdminPolicy", autherizeAdmin);
            });
        }
    }
}
