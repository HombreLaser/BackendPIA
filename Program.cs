using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
    // using System.IdentityModel.Tokens.Jwt;
    // using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BackendPIA;
using BackendPIA.Models;
using BackendPIA.Jobs;
using BackendPIA.Policies;
using BackendPIA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => { 
                                    options.InputFormatters.Insert(0, JPIF.GetJsonPatchInputFormatter());
                                }).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext")));
builder.Services.AddIdentity<UserAccount, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
// Automapper configuration.
builder.Services.AddAutoMapper(typeof(Program));

// Hosted services.
builder.Services.AddHostedService<LoggerJob>();

// Custom services configuration.
builder.Services.AddSingleton<ITokenGenerator>(s => new TokenGenerator(builder.Configuration["Jwt:Key"]));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IRaffleService, RaffleService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IPrizeService, PrizeService>();
// End of custom services configuration.
// Register the custom authorization handler.
builder.Services.AddScoped<IAuthorizationHandler, CorrectTokenHandler>();

// Swagger configuration.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityAPI", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header }
                    );

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        { 
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new String[]{}
                        }
                    }
                    );
                }
            );
// End of swagger configuration.

// Authentication configuration.
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
	    ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero }
    );

builder.Services.AddAuthorization(options => {
    options.AddPolicy("ValidToken", policy => policy.Requirements.Add(new CorrectTokenRequirement()));
});
// End of authentication configuration.

// Identity configuration.
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
// End of identity configuration.

// CORS configuration
builder.Services.AddCors(options => {
                            options.AddDefaultPolicy(builder => {
                                builder.WithOrigins("https://apirequest.io").AllowAnyMethod().AllowAnyHeader(); 
                            }
                        );
});
// End of cors configuration.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
