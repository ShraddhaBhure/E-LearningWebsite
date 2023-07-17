using C_Data;
using C_Models;
using C_Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreHero.ToastNotification.Extensions;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;



namespace E_LearningMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            
            // Configure DbContext with connection string

     
            services.AddDbContext<myDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
.AddEntityFrameworkStores<myDbContext>(); // Replace YourDbContext with the appropriate DbContext class for your application
           
            ////services.AddIdentity<ApplicationUser, IdentityRole>()
            ////   .AddEntityFrameworkStores<myDbContext>()
            //   .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });



            services.AddControllersWithViews();
            //// Configure Identity
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<myDbContext>()
            //    .AddDefaultTokenProviders();

            //// Configure password policies
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = true;
            //});

            //// Configure lockout policies
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;
            //});



            //// Configure cookie settings
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
            //    options.LoginPath = "/Login/Index";
            //    options.AccessDeniedPath = "/Home/AccessDenied";
            //    options.SlidingExpiration = true;
            //});

            // services.AddScoped<C_Services.ICrudeRepository,C_Services.CrudeRepository>();
            // services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(ICrudeRepository<>), typeof(CrudeRepository<>));
            services.AddScoped<ICrudeRepository<Login>, CrudeRepository<Login>>();
            services.AddScoped<IBooksLibraryRepository, BooksLibraryRepository>();
            services.AddScoped<IHomeProjectsRepository, HomeProjectsRepository>();
            services.AddScoped<IOnlineClassRepository, OnlineClassRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<IIssueMasterRepository, IssueMasterRepository>();




            //  services.AddScoped<INotificationRepository, NotificationRepository>();
            //services.AddHostedService<Worker>();
            //services.AddSingleton<IEmailService, EmailService>(); // Add your email service implementation
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter/*TopLeft*//*BottomRight*/; });
          
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNotyf();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
               //   pattern: "{controller=Login}/{action=Userlogin}/{id?}");




        });
        }
    }

   
}
