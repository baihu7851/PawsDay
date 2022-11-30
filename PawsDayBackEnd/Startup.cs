using ApplicationCore.Interfaces;
using Coravel;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using PawsDayBackEnd.Filters;
using PawsDayBackEnd.Helpers;
using PawsDayBackEnd.Interfaces;
using PawsDayBackEnd.Scheduler;
using PawsDayBackEnd.Services;
using PawsDayBackEnd.Services.SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PawsDayBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string MyCorsPolicy = "_MyCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PawsDayContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PawsDayConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<MemberServices>();
            services.AddScoped<MessageServices>();
            services.AddScoped<OrderServices>();
            services.AddScoped<SitterServices>();
            services.AddScoped<ProductServices>();
            services.AddScoped<SendGridServices>();
            services.AddSingleton<IAppPasswordHasher, SHA256Hasher>();
            services.AddScoped<AccountServices>();
            services.AddScoped<BlockTokenServices>();
            services.AddScoped<AdminAuthorize>();
            services.AddScoped<IMemberCountStatisticsService,RedisCacheMemberCountStatisticsService>();
            services.AddScoped<MemberCountStatisticsService>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<LineBotService>();
            services.AddScoped<UploadImageService>();

            services.AddScoped<ChartOrderServices>();
            services.AddScoped<ChartProductServices>();
            services.AddScoped<SitterChartService>();
            //Jwt驗證
            services.AddSingleton<JwtHelper>()
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //sub取User.Identtity.Name
                            NameClaimType = ClaimTypes.NameIdentifier,
                            //取Role判斷權限
                            RoleClaimType = ClaimTypes.Role,
                            //驗證Issuer
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),
                            ValidateAudience = false,
                            //驗證Token有效時間
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = false,
                            //檢查SignKey的真偽
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                        };
                    });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PawsDayBackEndAPI", Version = "v1" });
            });

            //跨域設定
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCorsPolicy,
                    policy =>
                    {
                        policy.AllowAnyOrigin() //TODO 依據需求調整Cors來源
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                    });
            });

            //排程設定         
            services.AddScheduler();
            services.AddScoped<OrderStatusScheduler>();
            services.AddScoped<BlockTokenScheduler>();
            services.AddScoped<LineBotHistoryScheduler>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "MyRedisCache";

            });




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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawsDayBackEndAPI v1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler.Schedule<OrderStatusScheduler>()
                    .Hourly();
                scheduler.Schedule<BlockTokenScheduler>()
                    .Hourly();
                    
                    
                scheduler.Schedule<LineBotHistoryScheduler>()
                    .Hourly();

            });


            app.UseRouting();
            app.UseCors(MyCorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "DashBoard",
                    pattern: "DashBoard",
                    defaults: new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "Login",
                    defaults: new { controller = "Auth", action = "Login" });
                endpoints.MapControllerRoute(
                    name: "AccessDenied",
                    pattern: "AccessDenied",
                    defaults: new { controller = "Auth", action = "AccessDenied" });
            });
        }
    }
}
