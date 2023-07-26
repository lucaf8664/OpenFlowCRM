using OpenFlowCRMAPI.Repository;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using OpenFlowCRMModels.Repository;

namespace OpenFlowCRMAPI
{
    public class Startup
    {
        public IConfiguration Configuration { set; get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string? connection_string = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");
            if (connection_string== null) { throw new Exception("Set the DEFAULT_CONNECTION_STRING environment variable"); }

            services.AddDbContext<SQL_TESTContext>(options =>
                options.UseSqlServer(connection_string));

            string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (secret== null)
            {
                secret = "hbUx0XI3Fb1dNi+XMugJW/Oe1VKERBErXYc/HmngAhU="; //TODO:
                //throw new Exception("Set the JWT_SECRET environment variable"); 
            }
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(secret);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            })
            .AddCookie("OpenFlowCRMCookie", options =>
            {
                options.Cookie.Name = "OpenFlowCRMCookie";
                options.TicketDataFormat = new CustomJwtDataFormat(secret);
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
