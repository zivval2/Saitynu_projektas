using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Saitynu_projektas.Controllers;
using Saitynu_projektas.Models;

namespace Saitynu_projektas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    var resolver = options.SerializerSettings.ContractResolver;
                    if (resolver != null)
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                });


            services.AddDbContext<ClientContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            // services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
            //.AddOAuthValidation();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "Bearer";
            //    options.DefaultChallengeScheme = "Bearer";
            //}).AddJwtBearer("Bearer", jwtOptions => 
            //{
            //    jwtOptions.SaveToken = true;
            //    jwtOptions.TokenValidationParameters = new TokenValidationParameters
            //    {
            //       // ValidateIssuerSigningKey = true;

            //        //ValidIssuer = "http://localhost:57032/",
            //        //ValidAudience = "http://localhost:57032/",
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = TokenController.SIGNING_KEY,
            //        //ValidateLifetime = true,
            //        //ClockSkew = TimeSpan.FromMinutes(5)
            //    };
            //});

            Environment.SetEnvironmentVariable("KEY_FOR_SECRET", "kazkas123213das_ASdasdadasdasdsada");
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY_FOR_SECRET")));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            //    (o => { o.Audience = "8d708afe-2966-40b7-918c-a39551625958";
            //       o.Authority = "https://login.microsoftonline.com/a1d50521-9687-4e4d-a76d-ddd53ab0c668/"; });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        //ValidIssuer = "http://localhost:57032/",
                        //ValidAudience = "http://localhost:57032/",
                        IssuerSigningKey = symmetricSecurityKey
                    };
                });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    "Bearer");
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseMvc();

            

           // app.USeAuthorisation();
        }
    }
}
