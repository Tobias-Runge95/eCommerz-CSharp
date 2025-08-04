using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Roll20Helper.Core.Auth;

public class AzureKeyVaultCryptoProviderFactory : CryptoProviderFactory
{
    private readonly CryptographyClient _cryptoClient;

    public AzureKeyVaultCryptoProviderFactory(CryptographyClient cryptoClient)
    {
        _cryptoClient = cryptoClient;
    }

    public override SignatureProvider CreateForSigning(SecurityKey key, string algorithm)
    {
        return new AzureKeyVaultSignatureProvider(_cryptoClient, key, algorithm, true);
    }
}