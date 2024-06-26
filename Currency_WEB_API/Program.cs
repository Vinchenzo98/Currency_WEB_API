using Currency.API.DAL;
using Currency.API.Repo.Interfaces;
using Currency.API.Repo;
using Currency.API.Services;
using Currency.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
builder.Services.AddScoped<ICurrencyExchangeRepo, CurrencyExchangeRepo>();
builder.Services.AddScoped<IUserInformationServices, UserInformationServices>();
builder.Services.AddScoped<IUserInformationRepo, UserInformationRepo>();
builder.Services.AddScoped<ITransactionLogServices, TransactionLogServices>();
builder.Services.AddScoped<ITransactionLogRepo, TransactionLogRepo>();
builder.Services.AddHttpClient<ICurrencyExchangeServices, CurrencyExchangeServices>();

builder.Services.AddDbContext<CurrencyAPIContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CurrencyAPIContext")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
        policy.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader();
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
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("AdminJwt");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();
