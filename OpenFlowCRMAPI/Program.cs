
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenFlowCRMAPI.Services;
using OpenFlowCRMModels.Models;
using OpenFlowCRMModels.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

string? connection_string = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");
if (connection_string== null) { throw new Exception("Set the DEFAULT_CONNECTION_STRING environment variable"); }

builder.Services.AddDbContext<SQL_TESTContext>(options => options.UseSqlServer(connection_string));
builder.Services.AddTransient<IAuthService, AuthService>();


// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
})

.AddCookie("OpenFlowCRMCookie", options =>
{
    options.Cookie.Name = "OpenFlowCRMCookie";
    options.TicketDataFormat = new CustomJwtDataFormat(builder.Configuration["JWTKey:Secret"]);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Open");
app.MapControllers();

app.Run();
