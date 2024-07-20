using Bloggie_Web.Data;
using Bloggie_Web.Repository.Contract;
using Bloggie_Web.Repository.Implement;
using Microsoft.AspNetCore.Identity;

namespace Bloggie_Web.Configure
{
    public static class DependencyInjection //Microsoft also use this way to inject their code.
    {

        //Like the normal way, remember to inject.

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {

            services.AddDbContext<BloggieContext>();
            services.AddDbContext<AuthDbContext>();

            return services;
        }

        public static IServiceCollection InjectRepository(this IServiceCollection services)
        {

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IBlogLikeRepository, BlogLikeRepository>();
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddUserIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true; //who just created the account but has not yet established a history of usage within the system
            })
              .AddEntityFrameworkStores<AuthDbContext>();

            return services;
        }

        public static IServiceCollection AddIdentityUserOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }

        public static IServiceCollection AddConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/access-denied";

                options.SlidingExpiration = true;
                options.Cookie.MaxAge = TimeSpan.FromDays(30);
            });

            return services;
        }
    }
}
