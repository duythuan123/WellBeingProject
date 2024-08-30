using BusinessLayer.IServices;
using BusinessLayer.Services;
using BusinessLayer.Utilities;
using DataAccessLayer.Context;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace WellMeetAPI.AppStarts
{
    public static class DependencyInjection
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });

            services.AddDbContext<AppDbContext> (options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookingRepositoy, BookingRepository>();
            services.AddScoped<IPsychiatristRepository, PsychiatristRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<SendEmailSMTPServices>();
            
        }
    }
}
