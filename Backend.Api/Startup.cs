using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using Backend.Api.Hubs;
using Backend.Services.Services;
using Backend.Data.Context;
using Backend.Data.Models;
using Backend.Repositories.Interfaces;
using Backend.Repositories.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;
using Shared;

namespace Backend.Api
{
    public class Startup
    {
        private const string Secret =
            "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private IConfiguration _configuration;
        public Logger _Logger;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(Secret);
                    var signingKey = new SymmetricSecurityKey(key);
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddMvc();
            services.AddAuthorization();
            services.AddSignalR();

            services.AddSingleton<IUserRepository,UserRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IChatRepository, ChatRepository>();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IChatService, ChatService>();

            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=DATABASE_NAME;Integrated Security=True;",null
                ),ServiceLifetime.Singleton);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            TypeAdapterConfig<UserDto, UserModel>.NewConfig()
                .Map(dest => dest.Login, src => src.Login).TwoWays();
            TypeAdapterConfig<OrderDto, OrderModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id).TwoWays();
            TypeAdapterConfig<ProductDto, ProductModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id).TwoWays();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            _Logger = LoggerHelper.GetLogger(new SerilogConfig()
            {
                LogEventLevel = LogEventLevel.Debug,
                LogDirectory = "C:\\logs\\VTMS_E2E\\",
                OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                FileSizeLimitBytes = 209715200,
                RetainedFileCountLimit = 20
            });
        }
    }
}
