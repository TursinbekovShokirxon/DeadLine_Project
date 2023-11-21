
using Application.ModelServices;
using Domain.Models;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using Infrastructure.Handlers.ForAuthentication;
using Infrastructure.Handlers.ForUser;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Application.StaticMethods;
using System.Text;
using Application.InterfacesModelServices;
using Application.Clases;
using Infrastructure.Handlers.ForTask;

namespace DeadLineSevice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddApplicationService(builder.Configuration);

            var secretKey = builder.Configuration.GetSection("JWTSettings")["SecretKey"];
            var issuer = builder.Configuration.GetSection("JWTSettings")["Issuer"];
            var audience = builder.Configuration.GetSection("JWTSettings")["Audience"];
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddScoped<ITaskService,TaskService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            builder.Services.AddScoped<IUserAuthService, Infrastructure.Services.AuthenticationService>();
            builder.Services.AddTransient<IRequestHandler<UserRegirstrationModel, string>, UserRegirstrationHandler>();
            builder.Services.AddTransient<IRequestHandler<UserLoginModel, UserAuth>, UserLoginHandler>();
            builder.Services.AddTransient<IRequestHandler<UserLoginModel, UserAuth>, UserLoginHandler>();
            builder.Services.AddTransient<IRequestHandler<UserCreateModel, User>, UserCreateHandler>();
            builder.Services.AddTransient<IRequestHandler<UserGetByIdModel, User>, UserGetByIdHandler>();
            builder.Services.AddTransient<IRequestHandler<UserGetAllModel, IEnumerable<User>>, UserGetAllHandler>();
            builder.Services.AddTransient<IRequestHandler<UserUpdateModel, string>, UserUpdateHandler>();
            builder.Services.AddTransient<IRequestHandler<UserDeleteModel, string>, UserDeleteHandler>();

            builder.Services.AddTransient<IRequestHandler<TaskCreateModel, Domain.Models.Task>, TaskCreateHandler>();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("ShokirsDatabase"));
            });

            builder .Services.AddRateLimiter(_=>
            _.AddFixedWindowLimiter("FixedWindowPolicy",opt=>
            {
                opt.Window=TimeSpan.FromSeconds(5);
                opt.PermitLimit = 5;
                opt.QueueLimit = 10;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            }));
            builder.Services.AddRateLimiter(option =>
            {
                option.AddSlidingWindowLimiter("SlidingWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(5);
                    opt.PermitLimit = 5;
                    opt.QueueLimit = 3;
                    opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    opt.AutoReplenishment = true;
                    opt.SegmentsPerWindow = 5;
                });
            });
            
            builder.Services.AddRateLimiter(option =>
            {
                option.AddTokenBucketLimiter("BucketWindowPolicy", opt =>
                {
                    opt.TokenLimit = 5;
                    opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                    opt.ReplenishmentPeriod=TimeSpan.FromSeconds(60);
                    opt.AutoReplenishment=true;
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRateLimiter();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}