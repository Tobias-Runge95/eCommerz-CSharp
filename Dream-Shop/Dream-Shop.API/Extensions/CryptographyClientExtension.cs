using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;

namespace Dream_Shop.API.Extensions;

public static class CryptographyClientExtension
{
    public static async Task<WebApplicationBuilder> RegisterCryptographyClient(this WebApplicationBuilder builder)
    {
        var credential = new ClientSecretCredential(
            tenantId: builder.Configuration["KeyVault:TenantId"],
            clientId: builder.Configuration["KeyVault:ClientId"], 
            clientSecret: builder.Configuration["KeyVault:Secret"]
        );
        var keyClient = new KeyClient(new Uri("https://authserver.vault.azure.net/"), credential);
    
        var key = await keyClient.GetKeyAsync(builder.Configuration["KeyVault:KeyName"]);
        var cryptoClient = new CryptographyClient(key.Value.Id, credential);
        builder.Services.AddSingleton(cryptoClient);
        return builder;
    }
}