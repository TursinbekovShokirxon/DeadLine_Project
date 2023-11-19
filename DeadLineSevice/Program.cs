
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
            builder.Services.AddSwaggerGen();
            var secretKey = builder.Configuration.GetSection("JWTSettings")["SecretKey"];
            var issuer = builder.Configuration.GetSection("JWTSettings")["Issuer"];
            var audience = builder.Configuration.GetSection("JWTSettings")["Audience"];
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));




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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseRateLimiter();

            app.MapControllers();

            app.Run();
        }
    }
}