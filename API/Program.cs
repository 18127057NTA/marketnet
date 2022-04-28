using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        { 
            //Lúc đầu
            //CreateHostBuilder(args).Build().Run();

            //Đoạn này tạo migrations và áp dụng vào database khi khởi động nếu gặp trục trặc thì dùng Lúc Đầu
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();

                    //Phần này seed giữ liệu mẫu
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                    //Tạo seed người dùng
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
                    //Vnvc
                    var vnvcContext = services.GetRequiredService<VNVCContext>();
                    await vnvcContext.Database.MigrateAsync();
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
