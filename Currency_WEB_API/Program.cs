using Currency.API.DAL;
using Currency.API.Repo;
using Currency.API.Repo.Interfaces;
using Currency.API.Services;
using Currency.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("Authorization");
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserLoginServices, UserLoginServices>();
builder.Services.AddScoped<IUserLoginRepo, UserLoginRepo>();
builder.Services.AddScoped<IUserRegisterServices, UserRegisterServices>();
builder.Services.AddScoped<IUserRegisterRepo, UserRegisterRepo>();
builder.Services.AddScoped<IUserRegisterServices, UserRegisterServices>();
builder.Services.AddScoped<IAdminLoginServices, AdminLoginServices>();
builder.Services.AddScoped<IAdminLoginRepo, AdminLoginRepo>();
builder.Services.AddScoped<IAdminRegisterServices, AdminRegisterServices>();
builder.Services.AddScoped<IAdminRegisterRepo, AdminReigsterRepo>();
builder.Services.AddScoped<IAccountTypeServices, AccountTypeServices>();
builder.Services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
builder.Services.AddScoped<IGetUserFromTokenService, GetUserFromTokenService>();
builder.Services.AddScoped<IGetAdminTokenFromService, GetAdminFromTokenService>();
builder.Services.AddScoped<ICurrencyExchangeRepo, CurrencyExchangeRepo>();
builder.Services.AddScoped<IUserInformationServices, UserInformationServices>();
builder.Services.AddScoped<IUserInformationRepo, UserInformationRepo>();
builder.Services.AddScoped<ITransactionLogServices, TransactionLogServices>();
builder.Services.AddScoped<ITransactionLogRepo, TransactionLogRepo>();
builder.Services.AddScoped<IBlockedTransactionServices, BlockedTransactionServices>();
builder.Services.AddScoped<IAdminBlockTransactionsRepo, AdminBlockTransactionsRepo>();
builder.Services.AddScoped<IBlockUserServices, BlockUserServices>();
builder.Services.AddScoped<IBlockUserRepo, BlockUserRepo>();
builder.Services.AddScoped<IDeniedTransactionServices, DeniedTransactionServices>();
builder.Services.AddHttpClient<ICurrencyExchangeServices, CurrencyExchangeServices>();

builder.Services.AddDbContext<CurrencyAPIContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CurrencyAPIContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:35842")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Auth using Bearer scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}
);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("UserJwt", options =>
{
    options.IncludeErrorDetails = true;
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            var claims = context.Principal.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("Token validation challenge triggered");
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["UserJwt:Issuer"],
        ValidAudience = builder.Configuration["UserJwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["UserJwt:Key"]))
    };
})
.AddJwtBearer("AdminJwt", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AdminJwt:Issuer"],
        ValidAudience = builder.Configuration["AdminJwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AdminJwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("UserJwt");
        policy.RequireClaim("userId");
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("AdminJwt");
        policy.RequireClaim("adminId");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();