using CustomerManagement.Data;
using CustomerManagement.Data.Entities;
using CustomerManagement.Helpers;
using CustomerManagement.Interceptors;
using CustomerManagement.Services.AutoMapper;
using CustomerManagement.Services.Classes;
using CustomerManagement.Services.Interfaces;
using CustomerManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Text.Json.Serialization;

namespace CustomerManagement.Extensions
{
    public static class ServiceExtension
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string sqlConnectionString = configuration.GetValue<string>("AppSettings:DefaultConnection");
            services.AddDbContext<AppDbContext>((servicesProvider, options) =>
            {
                options.UseSqlServer(sqlConnectionString,
                    opts => { opts.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName); });
                options.AddInterceptors(servicesProvider.GetRequiredService<SoftDeleteInterceptor>());
            });
        }

        public static void AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User,IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Config password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = false; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

            });
        }

        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<SoftDeleteInterceptor>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IWardService, WardService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFileService, FileService>();

        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, CustomerManagement.UnitOfWork.UnitOfWork>();
        }

        public static void AddAppControllers(this IServiceCollection services)
        {
            services.AddControllers()
                 .ConfigureApiBehaviorOptions(options =>
                 {
                     options.InvalidModelStateResponseFactory = (actionContext) =>
                     {
                         return ModelStateHelper.ModelStateResponse(actionContext);
                     };
                 }).AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                 });
        }

        public static void AddAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }


    }
}
