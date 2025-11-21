using Booking.Core.EmailContracts;
using Booking.Core.Pero_Contract;
using Booking.Core.Rpo.Contract;
using Booking.Repository.Data.DBIdentity;
using Booking.Repository.Repo;
using Booking.Services;
using Job.Core.Entity;
using JOB_PORTALl_API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JOB_PORTALl_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configuration Services 
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDBContextIdentity>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>() // 👈 استخدم AppUser بدل IdentityUser
                .AddEntityFrameworkStores<AppDBContextIdentity>();



            // Allowing DEpendancy Injection 
            builder.Services.AddAutoMapper(typeof(Mapping));
            builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepoo<>));
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ISavedProducts, SavesProducts>();
            builder.Services.AddScoped<IOrdersRepo, OrderRepo>();
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddTransient<IEmailService, EmailService>();
            #endregion

            var app = builder.Build();
            // ... في Program.cs بعد app.Build()

            // ########## إضافة Logging و try-catch ##########
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // أضف هذه الخدمة

                try
                {
                    var _userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await UserSeeding.SeedUsersAsync(_userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "حدث خطأ أثناء عملية Seeding المستخدمين."); // سجل الخطأ
                }
            }
            // ########## نهاية try-catch ##########

            #region Configure Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
