
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using iShop.Web.ClientAPI;

namespace iShop.Web.AdmidApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //var builder = new ConfigurationBuilder()
            // .AddJsonFile("appsettings.json")
            // .AddEnvironmentVariables();
            //Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddHttpClient();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => {
                    option.LoginPath = "/Login/Index/";
                    option.AccessDeniedPath = "/User/Forbidden";
                });
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ICategoryApiClient, CategoryApiClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
            //.AddJsonOptions(option => {
            //option.JsonSerializerOptions.PropertyNamingPolicy = null;
            //option.JsonSerializerOptions.DictionaryKeyPolicy = null;
            //});
            //services.AddControllers().AddNewtonsoftJson();
            
            IMvcBuilder builder = services.AddRazorPages();
           // services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // other configuration here
            //services.AddMvc()
            //  .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            // services.AddWebApiConventions();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//#if DEBUG
//            if (environment == Environments.Development) {
//                builder.AddRazorRunrimeComplation();
//            }
//#endif
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
            app.UseAuthentication();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}
