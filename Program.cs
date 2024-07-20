using Bloggie_Web.Configure;

namespace Bloggie_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddDbContext()
                .InjectRepository()
                .AddUserIdentityConfig()
                .AddIdentityUserOptions()
                .AddConfigureApplicationCookie();

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.Run();
        }
    }
}