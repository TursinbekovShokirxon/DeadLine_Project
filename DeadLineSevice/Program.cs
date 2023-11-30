
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
using Infrastructure.Handlers.ForTaskStatuses;
using Infrastructure.Handlers.ForTask;
using Infrastructure.Handlers.ForPermission;
using Infrastructure.Handlers.ForRoles;
using Application.CustomeAuth;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.AspNetCore.Authorization;

namespace DeadLineSevice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://pdp.com")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
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

            builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


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
            builder.Services.AddScoped<ITaskStatusService, TaskStatusService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IPermissionForRoleService, PermissionForRoleService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();


            builder.Services.AddTransient<IRequestHandler<UserRegirstrationModel, string>, UserRegirstrationHandler>();
            builder.Services.AddTransient<IRequestHandler<UserLoginModel, UserAuth>, UserLoginHandler>();
            builder.Services.AddTransient<IRequestHandler<UserLoginModel, UserAuth>, UserLoginHandler>();
            builder.Services.AddTransient<IRequestHandler<UserCreateModel, User>, UserCreateHandler>();
            builder.Services.AddTransient<IRequestHandler<UserGetByIdModel, User>, UserGetByIdHandler>();
            builder.Services.AddTransient<IRequestHandler<UserGetAllModel, IEnumerable<User>>, UserGetAllHandler>();
            builder.Services.AddTransient<IRequestHandler<UserUpdateModel, string>, UserUpdateHandler>();
            builder.Services.AddTransient<IRequestHandler<UserDeleteModel, string>, UserDeleteHandler>();
            builder.Services.AddTransient<IRequestHandler<UserDeleteModel, string>, UserDeleteHandler>();
            builder.Services.AddTransient<IRequestHandler<PerMissionGetAllModel, IEnumerable<Permission>>, PermissionGetAllHandler>();


            builder.Services.AddTransient<IRequestHandler<CreateRoleModel, Role>, CreateRoleHandler>();
            builder.Services.AddTransient<IRequestHandler<AddPermissionInRoleModel, string>, AddPermissionInRoleHandler>();
            builder.Services.AddTransient<IRequestHandler<AddUserInRoleModel, string>, AddUserInRoleHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdRoleModel, Role>, GetByIdRoleHandler>();
            builder.Services.AddScoped<Application.ModelServices.IUserAuthService, Infrastructure.Services.AuthenticationService>();


            builder.Services.AddTransient<IRequestHandler<TaskStatusCreateModel, bool>, TaskStatusCreateHandler>();
            builder.Services.AddTransient<IRequestHandler<TaskStatusDeleteModel, bool>, TaskStatusDeleteHandler>();
            builder.Services.AddTransient<IRequestHandler<TaskStatusGetAllModel, IEnumerable<Domain.Models.TaskStatus>>, TaskStatusGetAllHandler>();
            builder.Services.AddTransient<IRequestHandler<TaskStatusGetByIdModel, Domain.Models.TaskStatus>, TaskStatusGetByIdHandler>();
            builder.Services.AddTransient<IRequestHandler<TaskStatusUpdateModel, bool>, TaskStatusUpdateHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllRoleModel, IEnumerable<Role>>,GetAllHandler>();


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


            //app.UseCors(builder =>
            //    builder
            //    .WithOrigins("https://pdp.uz")
            //    .WithMethods("GET")
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //);

            //app.Use((ctx, next) =>
            //{
            //    ctx.Response.Headers["Access-Control-Allow-Origin"] = "https://pdp.uz";
            //    return next();
            //});


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRateLimiter();

            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}