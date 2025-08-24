using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Dream_Shop.API;
using Dream_Shop.API.Extensions;
using Dream_Shop.Core;
using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddKeyClient(new Uri($"https://authserver.vault.azure.net/"));
    // azureBuilder.AddCryptographyClient(new Uri($"https://authserver.vault.azure.net/"));
    azureBuilder.UseCredential(new ClientSecretCredential(
        tenantId: builder.Configuration["KeyVault:TenantId"],
        clientId: builder.Configuration["KeyVault:ClientId"], 
        clientSecret: builder.Configuration["KeyVault:Secret"]
    ));
});
await CryptographyClientExtension.RegisterCryptographyClient(builder);
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("admin", policy => policy.RequireRole("admin"));
    option.AddPolicy("user", policy => policy.RequireRole("user"));
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var keyClient = serviceProvider.GetRequiredService<KeyClient>();
        var publicKey = keyClient.GetKey(builder.Configuration["KeyVault:KeyName"]).Value;
        var rsaKey = new RsaSecurityKey(publicKey.Key.ToRSA());
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = rsaKey,
            ValidIssuer = "DS"
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddRoleStore<RoleStore<Role, AppDbContext, Guid>>()
    .AddUserStore<UserStore<User, IdentityRole<Guid>, AppDbContext, Guid>>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.RegisterServices();
builder.Services.AddScoped<DataPusher>();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataPusher>();
    await seeder.PushRoles();
}
app.Run();
