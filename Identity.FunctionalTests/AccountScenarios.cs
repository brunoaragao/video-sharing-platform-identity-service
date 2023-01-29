namespace AluraChallenge.VideoSharingPlatform.Services.Identity.FunctionalTests;

[Collection(nameof(TestServerFixtureCollection))]
public class AccountScenarios
{
    private const string LoginUri = "/api/v1/account/login";
    private const string RegisterUri = "/api/v1/account/register";

    public AccountScenarios(TestServerFixture fixture)
    {
        Client = fixture.Client;
    }

    public HttpClient Client { get; }

    [Fact]
    public async Task post_register_should_return_ok_status_code()
    {
        var command = GetRegisterUserCommandWithValidValues();
        var response = await Client.PostAsJsonAsync(RegisterUri, command);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task post_resgister_with_invalid_password_should_return_bad_request_status_code()
    {
        var command = GetRegisterUserCommandWithInvalidPassword();
        var response = await Client.PostAsJsonAsync(RegisterUri, command);

        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task post_register_with_email_already_registered_should_return_bad_request_status_code()
    {
        var command = GetRegisterUserCommandWithRegisteredEmail();
        var response = await Client.PostAsJsonAsync(RegisterUri, command);

        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task post_login_should_return_ok_status_code()
    {
        var command = GetLoginUserCommandWithRegisteredEmail();
        var response = await Client.PostAsJsonAsync(LoginUri, command);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task post_login_with_wrong_password_should_return_bad_request_status_code()
    {
        var command = GetLoginUserCommandWithWrongPassword();
        var response = await Client.PostAsJsonAsync(LoginUri, command);

        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task post_login_with_email_not_registered_should_return_bad_request_status_code()
    {
        var command = GetLoginUserCommandWithNotRegisteredEmail();
        var response = await Client.PostAsJsonAsync(LoginUri, command);

        Assert.Equal(BadRequest, response.StatusCode);
    }

    private RegisterUserCommand GetRegisterUserCommandWithValidValues()
    {
        return new() { Email = "example@email.com", Password = "P@ssw0rd" };
    }

    private RegisterUserCommand GetRegisterUserCommandWithInvalidPassword()
    {
        return new() { Email = "example@email.com", Password = "invalid" };
    }

    private RegisterUserCommand GetRegisterUserCommandWithRegisteredEmail()
    {
        return new() { Email = "registered@email.com", Password = "P@ssw0rd" };
    }

    private RegisterUserCommand GetRegisterUserCommandWithNotRegisteredEmail()
    {
        return new() { Email = "not_registered@email.com", Password = "P@ssw0rd" };
    }

    private LoginUserCommand GetLoginUserCommandWithRegisteredEmail()
    {
        return new() { Email = "registered@email.com", Password = "P@ssw0rd" };
    }

    private LoginUserCommand GetLoginUserCommandWithNotRegisteredEmail()
    {
        return new() { Email = "not_registered@email.com", Password = "P@ssw0rd" };
    }

    private LoginUserCommand GetLoginUserCommandWithWrongPassword()
    {
        return new() { Email = "registered@email.com", Password = "wrong_P@ssw0rd" };
    }
}
