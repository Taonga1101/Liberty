using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Liberty
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "LibertyCookie";
            });   
            
            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                    options.LoginPath = _configuration.GetValue<string>("ApplicationCredentials:LoginPath");
                    options.Cookie.Name = _configuration.GetValue<string>("ApplicationCredentials:CookieName");
                    options.AccessDeniedPath = _configuration.GetValue<string>("ApplicationCredentials:AccessDeniedPath");
                });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });
            

            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped(sp => sp.GetService<IHttpContextAccessor>().HttpContext.Session);

            var connectionString = _configuration.GetConnectionString("LibertyDb");
            services.AddDbContext<LIBERTYContext>(options => options.UseSqlServer(connectionString),
                ServiceLifetime.Transient);
            
            services.AddScoped<LeaveService>();
            services.AddScoped<EmployeeService>();
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
            
            
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

            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite != SameSiteMode.None) return;
            if (options.SameSite == SameSiteMode.None && DisallowsSiteNone(httpContext))
            {
                options.SameSite = SameSiteMode.None;
            }
        }

        private static bool DisallowsSiteNone(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"];
            if (string.IsNullOrEmpty(userAgent))
            {
                return false;
            }

            return ((ICollection<string>)userAgent).Contains("BrokenUserAgent") || ((ICollection<string>)userAgent).Contains("BrokenUserAgent2");
        }
    }
}
