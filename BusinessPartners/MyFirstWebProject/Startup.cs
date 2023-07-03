using BL_;
using DL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;

namespace MyFirstWebProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("http://aaa.com", "http://bbb.com");
                });
            });
       

            services.AddControllers();
            services.AddScoped<IuserBL,UserBL>();
            services.AddScoped<IuserDL, UserDL>();
            services.AddScoped<IitemDL,IteamDL>();
            services.AddScoped<IitemBL,ItemBL>();
            services.AddScoped<IBP_Dl, BP_DL>();
            services.AddScoped<IBP_BL,BP_BL>();
            services.AddScoped<IDocumentBL,DocumentBL>();
            services.AddScoped<IDocumentDL, DocumentDL>();

            services.AddScoped<IPasswordHashHelper,PasswordHashHelper>();
            services.AddDbContext<BusinessPartnersContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BusinessPartners")));
            services.AddResponseCaching();
            services.AddAutoMapper(typeof(Startup));
            //srv2\\pupils  //LAPTOP - ODKQSO4A
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusinessPartners", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",

                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}

                //    }
                //});
            });


            var Tokenkey = Configuration.GetSection("AppSettings:TokenKey").Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Tokenkey))
                };
            });
            services.AddAuthorization();
     

          
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BusinessPartnersContext context,ILogger<Startup> logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BusinessPartners v1"));
            }
            
           
            logger.LogInformation("server is up"); 


            app.UseErrorMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // It sets the policy to restrict the sources from which certain content can be loaded, such as scripts and images
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "default-src 'self';" +
                    "style-src 'self';" +
                    "img-src 'self'");
                await next();
            });

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(60)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
           
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);
            app.UseAuthentication(); // Add this line before UseAuthorization()

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



//dotnet ef dbcontext scaffold "Server=DESKTOP-9IDNILH;Database=BusinessPartners;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Yes;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models

//"Server=DESKTOP-E0FAPSB\SQLEXPRESS;Database=BusinessPartners;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Yes;"