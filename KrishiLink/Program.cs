using KrishiLink.DBContext;
using KrishiLink.Models.Auth;
using KrishiLink.Repository.Auth;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Repository.Broker;
using KrishiLink.Repository.Broker.Interface;
using KrishiLink.Repository.Farmer;
using KrishiLink.Repository.Farmer.Interface;
using KrishiLink.Repository.Transport;
using KrishiLink.Repository.Transport.Interface;
using KrishiLink.Service.Auth;
using KrishiLink.Service.Auth.Interface;
using KrishiLink.Service.Broker;
using KrishiLink.Service.Broker.Interface;
using KrishiLink.Service.Farmer;
using KrishiLink.Service.Transport;
using KrishiLink.Service.Transport.Interface;
using KrishiLink.Services.Farmer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

builder.Services.AddScoped<IFarmerSaleRepository, FarmerSaleRepository>();
builder.Services.AddScoped<IFarmerSaleService, FarmerSaleService>();

builder.Services.AddScoped<IBrokerDataRepository, BrokerDataRepository>();
builder.Services.AddScoped<IBrokerDataService, BrokerDataService>();

builder.Services.AddScoped<IVehTransportRepository, VehTransportRepository>();
builder.Services.AddScoped<IVehTransportService, VehTransportService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<Register>, PasswordHasher<Register>>();

var jwtSettings = builder.Configuration.GetSection("Jwt");

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Swagger with JWT auth
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTAuth API", Version = "v1" });

    // JWT Authorization in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowUi",
//        policy =>
//        {
//            // Allow http://localhost:4200 for local dev
//            // and anything that ends with .app.github.dev
//            policy
//                .SetIsOriginAllowed(origin =>
//                {
//                    var uri = new Uri(origin);

//                    // Codespaces preview URLs look like
//                    //  https://<slug>-<port>.app.github.dev
//                    bool isCodespace = uri.Host.EndsWith(".app.github.dev",
//                                                          StringComparison.OrdinalIgnoreCase);

//                    bool isLocalAngular = origin.Equals("http://localhost:4200",
//                                                         StringComparison.OrdinalIgnoreCase);

//                    return isCodespace || isLocalAngular;
//                })
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();  // only if you really need cookies
//        });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUi", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Optional: use only if your frontend sends cookies or credentials
    });
});



builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowUi");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
