using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PawsDay.Services.SitterCenter;
using PawsDay.Services.Product;
using PawsDay.Services.Home;
using PawsDay.Services.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PawsDay.Services.ShoppingCart;
using PawsDay.Services.BecomePetSitter;
using PawsDay.Services.MemberCenter;
using Microsoft.OpenApi.Models;
using PawsDay.Services.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Services;
using PawsDay.Services.StaticWeb;
using PawsDay.Interfaces.Account;
using PawsDay.Services.SendGridServices;
using PawsDay.Services;
using PawsDay.Interfaces.Index;
using PawsDay.Services.LineBot;
using Flow.FCL.Config;
using Flow.FCL.WalletProvider;
using Flow.Net.Sdk.Core.Client;
using System.Net.Http;
using Flow.FCL.Utility;
using Microsoft.Extensions.Logging;
using Flow.Net.Sdk.Client.Http;
using Flow.FCL.Models;
using Flow.FCL;
using Blocto.SDK.Flow;
using Blocto.Sdk.Flow.Utility;

namespace PawsDay
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
            services.AddDbContext<PawsDayContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PawsDayConnection"));
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "MyRedisCache";
            });
            //註冊repository
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            //註冊service
            services.AddScoped<IIndexServices,RedisCacheIndexServices>();
            services.AddScoped<IndexServices>();
            services.AddScoped<SitterCenterServices>();
            services.AddScoped<SitterCenterBasicServices>();
            services.AddScoped<SitterCenterOrderServices>();
            services.AddScoped<ProductServices>();
            services.AddScoped<MenuServices>();
            services.AddScoped<BookingServices>();
            services.AddScoped<CartServices>();
            services.AddScoped<OrderPlacedServices>();
            services.AddScoped<ShoppingCartRepository>();
            services.AddScoped<BecomePetSitterService>();
            services.AddScoped<CollectViewModelServices>();
            services.AddScoped<HistoryEvaluationViewModelService>();
            services.AddScoped<MemberCenterOrderServices>();
            services.AddScoped<MemberCenterSidebarServices>();
            services.AddScoped<MemberPetInfoService>();
            services.AddScoped<ChatroomViewModelService>();
            services.AddScoped<PersonInfoServices>();
            services.AddScoped<IAccountManager, LoginViewModelService>();
            services.AddTransient<IRegisterViewModelService, RegisterViewModelService>();
            services.AddSingleton<IAppPasswordHasher, SHA256Hasher>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<MemberCenterCalenderService>();
            services.AddScoped<ContactUsService>();
            services.AddScoped<DBService>();
            services.AddTransient<ILineLoginService, LineLoginService>();
            services.AddTransient<IGoogleLoginService, GoogleLoginService>();
            services.AddScoped<LineBotSearchService>();
            services.AddScoped<LineBotService>();

            services.AddScoped<UploadImageService>();
            services.AddScoped<SendGridService>();
            services.AddScoped<SitterCenterAptitudeService>();
            services.AddControllers();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PawsDayAPI", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddSingleton<Config>(provider => {
                var config = new Config();
                config.Put("discovery.wallet", "https://flow-wallet-testnet.blocto.app/api/flow/authn")
                      .Put("accessNode.api", "https://rest-testnet.onflow.org/v1")
                      .Put("flow.network", "testnet");
                return config;
            });

            services.AddScoped<IWalletProvider>(provider => {
                var flowClient = provider.GetRequiredService<IFlowClient>();
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var resolveUtility = provider.GetRequiredService<IResolveUtility>();
                var logger = provider.GetRequiredService<ILogger<BloctoWalletProvider>>();

                var bloctoWalletProvider = new BloctoWalletProvider(logger, httpClientFactory, flowClient, resolveUtility, "testnet", Guid.Parse("5dab3bf2-90ea-4a40-922c-cd93991f5409"));
                return bloctoWalletProvider;
            });

            services.AddScoped<IResolveUtility>(provider => new ResolveUtility());
            services.AddScoped<IEncodeUtility, EncodeUtility>();
            services.AddScoped<IFlowClient>(provider => {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                var flowClient = new FlowHttpClient(httpClient, new FlowClientOptions
                {
                    ServerUrl = "https://rest-testnet.onflow.org/v1"
                });
                return flowClient;
            });
            services.AddScoped<Flow.FCL.Transaction>();
            services.AddScoped<CurrentUser>();
            services.AddScoped<AppUtility>();
            services.AddScoped<FlowClientLibrary>(provider => {
                var config = provider.GetRequiredService<Config>();
                FlowClientLibrary.SetConfig(config);

                var walletProvider = provider.GetRequiredService<IWalletProvider>();
                var transaction = provider.GetRequiredService<Flow.FCL.Transaction>();
                var currentUser = provider.GetRequiredService<CurrentUser>();
                var flowClient = provider.GetRequiredService<IFlowClient>();
                var fcl = new FlowClientLibrary(walletProvider, transaction, currentUser, flowClient);

                return fcl;
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

                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawsDayAPI v1");
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors(MyCorsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //會員聊天室
                endpoints.MapControllerRoute(
                 name: "Member",
                 pattern: "Member", new { controller = "MemberCenter", action = "PersonInformation" });
                endpoints.MapControllerRoute(
                  name: "Member/MessageList-Sitter",
                  pattern: "Member/MessageList-Sitter", new { controller = "MemberCenter", action = "ChatroomSister" });
                endpoints.MapControllerRoute(
                   name: "Member/MessageList-Official",
                   pattern: "Member/MessageList-Official", new { controller = "MemberCenter", action = "ChatRoomPawsDay" });
                endpoints.MapControllerRoute(
                   name: "Member/MessageBox-Sitter/{id}",
                   pattern: "Member/MessageBox-Sitter/{id?}", new { controller = "MemberCenter", action = "ChatroomSisterDetail" });
                endpoints.MapControllerRoute(
                   name: "Member/MessageBox-Official/{id}",
                   pattern: "Member/MessageBox-Official/{id?}", new { controller = "MemberCenter", action = "OrderContact" });
                //保母聊天室
                endpoints.MapControllerRoute(
                   name: "Sitter/MessageList-Customer",
                   pattern: "Sitter/MessageList-Customer", new { controller = "SitterCenter", action = "ChatRoomCustomerList" });
                endpoints.MapControllerRoute(
                   name: "Sitter/MessageList-Official",
                   pattern: "Sitter/MessageList-Official", new { controller = "SitterCenter", action = "ChatRoomPawsDayList" });
                endpoints.MapControllerRoute(
                   name: "Sitter/MessageBox-Customer/{id?}",
                   pattern: "Sitter/MessageBox-Customer/{id?}", new { controller = "SitterCenter", action = "ChatRoomCustomer" });
                endpoints.MapControllerRoute(
                   name: "Sitter/MessageBox-Official/{id?}",
                   pattern: "Sitter/MessageBox-Official/{id?}", new { controller = "SitterCenter", action = "ChatRoomPawsDay" });
                //商品管理
                endpoints.MapControllerRoute(
                  name: "Sitter",
                  pattern: "Sitter", new { controller = "SitterCenter", action = "Basic" });
                endpoints.MapControllerRoute(
                  name: "ProductSetting/On",
                  pattern: "ProductSetting/On", new { controller = "SitterCenter", action = "ServiceIndex" });
                endpoints.MapControllerRoute(
                  name: "ProductSetting/Off",
                  pattern: "ProductSetting/Off", new { controller = "SitterCenter", action = "ServiceClose" });
                endpoints.MapControllerRoute(
                  name: "ProductSetting/{id?}",
                  pattern: "ProductSetting/{id?}", new { controller = "SitterCenter", action = "GetServiceWithDetail" });
                endpoints.MapControllerRoute(
                  name: "DiscountSetting",
                  pattern: "DiscountSetting", new { controller = "SitterCenter", action = "ServiceSales" });
                endpoints.MapControllerRoute(
                  name: "PromoteSetting",
                  pattern: "PromoteSetting", new { controller = "SitterCenter", action = "ServiceAdvertise" });
                //搜尋頁
                endpoints.MapControllerRoute(
                    name: "Search",
                    pattern: "Search/{sort=Pawsday}/{searchinput=Recommend}/{county?}/{district?}/{day?}/{time?}/{service?}/{pet?}",
                    defaults: new { controller = "Product", action = "SearchProduct" });
                //商品頁
                endpoints.MapControllerRoute(
                    name: "Product",
                    pattern: "Product/{id}",
                    defaults: new { controller = "Product", action = "Product" });
                // 註冊 - 輸入基本資料
                endpoints.MapControllerRoute(
                    name: "Register",
                    pattern: "Register/{accountInfoId}/{intputExpirationTime}", new { controller = "Account", action = "Register" });
                // 忘記密碼 - 帶 AccountInfoId & 失效時間  ForgotPassword
                endpoints.MapControllerRoute(
                    name: "ForgotPassword",
                    pattern: "ForgotPassword/{accountInfoId}/{intputExpirationTime}", new { controller = "Account", action = "ForgotPassword" });
               
            });
        }
    }
}