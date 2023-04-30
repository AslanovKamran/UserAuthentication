using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserAuth.Models;
using UserAuth.Repository.Interfaces;
using UserAuth.Tokens;
using UserAuth.Repository.EFCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
	#region Jwt Bearer Section
	var securityScheme = new OpenApiSecurityScheme
	{
		Name = "Jwt Authentication",
		Description = "Type in a valid JWT Bearer",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "Jwt",
		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};
	options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{securityScheme,Array.Empty<string>() }
				});

	#endregion
});

#region Jwt Options
var jwtOptions = builder.Configuration.GetSection("Jwt");
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["Key"]!));

const double AccessTokenLifeTime = 3;
const double RefreshokenLifeTime = 30;

builder.Services.Configure<JwtOptions>(options =>
{
	options.Issuer = jwtOptions["Issuer"]!;
	options.Audience = jwtOptions["Audience"]!;
	options.AccessValidFor = TimeSpan.FromMinutes(AccessTokenLifeTime);
	options.RefreshValidFor = TimeSpan.FromDays(RefreshokenLifeTime);
	options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
});
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,

		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

		ClockSkew = TimeSpan.Zero // Removes default additional time to the tokens
	};
});
#endregion

var connectionString = builder.Configuration.GetConnectionString("Default");

#region EF Core
builder.Services.AddDbContext<UserAuthSampleContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepositoryEFCore>();

#endregion

builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
