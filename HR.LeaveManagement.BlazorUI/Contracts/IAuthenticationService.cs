﻿namespace HR.LeaveManagement.BlazorUI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);

        Task<bool> RegisterAsync(string firstName, string lastName, string username, string email, string password);

        Task Logout();
    }
}
