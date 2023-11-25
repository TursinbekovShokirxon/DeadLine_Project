using Application.CustomeAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StaticMethods
{
    public static  class ApplicationService
    {
       
        public static void AddApplicationService(this IServiceCollection service,IConfiguration configuration)
        {

            #region 

            service.AddAuthentication    /*(JwtBearerDefaults.AuthenticationScheme)*/
            (options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";


            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true, // Установите false, если не хотите проверять срок действия токена
                    //ValidAudience = configuration["JWTSettings:AudienceKey"], // Установите true, если требуется проверка аудитории
                    //ValidIssuer = configuration["JWTSettings:IssueKey"], // Установите true, если требуется проверка издателя
                    //ClockSkew = TimeSpan.Zero // Установите ноль, если не хотите учитывать разницу во времени
                };
                #region

                //jwtBearerOptions.Events = new JwtBearerEvents()
                //{
                //    OnAuthenticationFailed = (context) =>
                //    {
                //        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                //        {
                //            context.Response.Headers.Add("IsTokenExpired", "true");
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
                #endregion
            });
            service.AddAuthorization(options =>
            {
                options.AddPolicy("HasPermissionPolicy", policy =>
                    policy.Requirements.Add(new PermissionRequirement("Registration")));
            });

            //service.AddAuthorization();

            #region
            //service.AddAuthorization(option =>
            //{
            //    //option.AddPolicy(IdentityData.AdminUserPolicyName,
            //    //    p => p.RequireClaim(IdentityData.AdminUserClainName, "true"));
            //    option.AddPolicy("RequireLoggedIn", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //    });
            //});
            //service.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //                            .AllowAnyMethod()
            //                            .AllowAnyHeader()
            //                            .AllowCredentials());
            //});
            #endregion
            #endregion


        }
    }
}
