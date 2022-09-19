using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.ViewModels;

namespace TaskManager.IntegrationTests;

public class AccountTest : IntegrationTest
{
    protected async Task<HttpResponseMessage> CreateUserAsync()
    {
        var registerForm = new RegisterViewModel
        {
            Email = "testAccount@gmail.com",
            Password = "strongPass123",
            Username = "testUser",
            Roles = new List<string> { "user" }
        };

        return await RegisterUserAsync(registerForm);
    }
    protected async Task<HttpResponseMessage> LoginAsync()
    {
        var registerForm = new RegisterViewModel
        {
            Email = "testAccountLogin@gmail.com",
            Password = "strongPass123",
            Username = "testUser",
            Roles = new List<string> { "user" }
        };

        HttpResponseMessage responseFromRegisterUser = await RegisterUserAsync(registerForm);
        CheckStatusCodeIfNotValidFailTest(responseFromRegisterUser);

        string json = JsonSerializer.Serialize<RegisterViewModel>(registerForm);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        return await _httpClient.PostAsync(_apiUrl + "Account" + "/Login", payload);
    }
}
