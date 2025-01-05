using FishyFlip.Models;
using FishyFlip;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Extensions.Logging;
using BlueSkyApp.Framework;

namespace BlueSkyApp.Services.Implementation;

public class BlueSkyService(ISecureStorage credentialsService, ILogger<BlueSkyService> logger) : IBlueSkyService
{
    record Credentials(string Handle, string AppPassword);

    private readonly ISecureStorage _credentialsService = credentialsService;
    private readonly ATProtocol _client = new ATProtocolBuilder().EnableAutoRenewSession(true).Build();

    public bool IsLoggedIn { get; private set; }

    public ATProtocol Client => _client;

    public async Task<bool> InitializeAsync()
    {
        if (IsLoggedIn)
        {
            return true;
        }

        var credentials_json = await _credentialsService.GetAsync("credentials");

        if (credentials_json != null)
        {
            var credentials = System.Text.Json.JsonSerializer.Deserialize<Credentials>(credentials_json);
            if (credentials is not null &&
                credentials.Handle is not null &&
                credentials.AppPassword is not null)
            {
                var result = await _client.AuthenticateWithPasswordResultAsync(credentials.Handle, credentials.AppPassword);

                IsLoggedIn = result.Match(result => true, result => false);

                return IsLoggedIn;
            }
        }

        return false;
    }

    public async Task<bool> LoginAsync(string handle, string appPassword, bool remember)
    {
        var result = await _client.AuthenticateWithPasswordResultAsync(handle, appPassword);

        IsLoggedIn = result.Match(result => true, result => false);

        if (IsLoggedIn && remember)
        {
            var credentials_json = System.Text.Json.JsonSerializer.Serialize(new Credentials(handle, appPassword));
            await _credentialsService.SetAsync("credentials", credentials_json);
        }

        if (!IsLoggedIn)
        {
            var resultMessage = result.Match(_ => "successful", _ => $"unsuccessful ({_.StatusCode}, {_.Detail?.Error} - {_.Detail?.Message})");
 
            logger.LogInformation("Login attempt for {Handle} was {resultMessage}", 
                handle,
                resultMessage);

           await AppUtils.DisplaySnackbarAsync(resultMessage);
        }

        return IsLoggedIn;
    }
}