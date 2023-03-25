using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.EntityFrameworkCore.SqlServer;
using System.Threading.Tasks;
using iShop.Data.Entities.EF;
using Microsoft.EntityFrameworkCore;
using iShop.Application.Domain.Catalog.Products;
using Microsoft.OpenApi.Models;
using iShop.Application.Domain.Common;
using iShop.Application.Domain.System.Users;
using iShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using iShop.Application.Domain.System.Roles;
using iShop.Application.Domain.Catalog.Category;
using iShop.Application.Domain.Unitities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace iShop.Web.BackendApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables();
            Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<iShopDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddPaging();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<iShopDbContext>()
                .AddDefaultTokenProviders();
            // Register ServiceProduct
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISlideService, SlideService>();
            // services.AddTransient<IPbulicProductService, PublicProductService>();
            services.AddTransient<IManagerProductService, ManagerProductService>();
            services.AddControllers();
            services.AddSwaggerGen();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
               options => {
                  options.SwaggerDoc("v1", new OpenApiInfo {
                      Version = "v1",
                      Title = "ToDo API",
                      Description = "An ASP.NET Core Web API for managing ToDo items",
                      TermsOfService = new Uri("https://example.com/terms"),
                      Contact = new OpenApiContact {
                          Name = "Example Contact",
                          Url = new Uri("https://example.com/contact")
                      },
                      License = new OpenApiLicense {
                          Name = "Example License",
                          Url = new Uri("https://example.com/license")
                      }
                  });
              });
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

//            var jwtSettings = configuration.GetSection("JwtSettings");
//var secretKey = Environment.GetEnvironmentVariable("SECRET");
            ////string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            //string signingKey = Configuration.GetValue<string>("Jwt:Key");
            //byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(
            opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters() {
                     ValidateIssuer = true,
                     ValidIssuer = issuer,
                     ValidateAudience = true,
                     ValidAudience = issuer,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ClockSkew = System.TimeSpan.Zero,
                     IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                 };
             });
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                    app.UseSwagger();
                    app.UseSwaggerUI();
                
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
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
