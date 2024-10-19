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

            services.AddDbContext<WellMeetDbContext> (options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfileExtension));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPsychiatristRepository, PsychiatristRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPsychiatristService, PsychiatristService>();
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<SendEmailSMTPServices>();
            services.AddScoped<IAppointmentService, AppointmentService>();
        }
    }
}
