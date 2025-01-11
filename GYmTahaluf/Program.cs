using GYmTahaluf.Models;
using Microsoft.EntityFrameworkCore;

namespace GYmTahaluf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ModelContext>(options =>
               options.UseOracle(builder.Configuration.GetConnectionString("RestaurantConnection")));
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddSession(options => {

                options.IdleTimeout = TimeSpan.FromMinutes(60);

            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}