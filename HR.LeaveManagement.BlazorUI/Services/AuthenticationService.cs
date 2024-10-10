using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationsStateProvider;

        public AuthenticationService(
            IClient client, 
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationsStateProvider) : base(client, localStorageService)
        {
            _authenticationsStateProvider = authenticationsStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new()
                {
                    Email = email,
                    Password = password
                };

                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty) 
                {
                    await _localStorageService.SetItemAsync("token", authenticationResponse.Token);

                    // Set claims in Blazor and login state
                    await ((ApiAuthenticationStateProvider) _authenticationsStateProvider).LoggedIn();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            // Remove claims in Blazor and invalid login state
            await ((ApiAuthenticationStateProvider)_authenticationsStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string username, 
            string email, string password)
        {
            RegistrationRequest registrationRequest1 = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = username,
                Password = password
            };
            RegistrationRequest registrationRequest = registrationRequest1;

            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId)) { 
                return true;
            }

            return false;
        }
    }
}
