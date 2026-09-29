using AdoPet.Exception;
using AdoPet.Infrastructure.DataAccess;
using CommonTestUtilities.ClassDataGenerator;
using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.User.Register;

public class RegisterUserAccountTests : IClassFixture<AdoPetApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly string REQUEST_URI = "/users";
    private readonly AdoPetDbContext _dbContext;

    public RegisterUserAccountTests(AdoPetApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();

        var scope = factory.Services.CreateAsyncScope();

        _dbContext = scope.ServiceProvider.GetRequiredService<AdoPetDbContext>();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("userName").GetString().ShouldBe(request.UserName);

        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();

        responseData.RootElement.GetProperty("tokens").GetProperty("refreshToken").GetString().ShouldBeEmpty();

        var userExists = await _dbContext.Users.AnyAsync(user => user.Active && user.UserName.Equals(request.UserName) && user.Email.Equals(request.Email));

        userExists.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Validate_ShouldBeAnErrorResponse_When_UserNameIsEmpty(string culture)
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.UserName = string.Empty;

        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd(culture);

        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var expectedErrorMessage = ResourceMessagesException.ResourceManager.GetString("VALIDATION_USERNAME_REQUIRED", new CultureInfo(culture));
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldSatisfyAllConditions(errorList =>
        {
            errorList.Count().ShouldBe(1);
            errorList.ShouldContain(error => error.GetString()!.Equals(expectedErrorMessage));
        });

        var userExists = await _dbContext.Users.AnyAsync(user => user.Active && user.UserName.Equals(request.UserName) && user.Email.Equals(request.Email));

        userExists.ShouldBeFalse();
    }
}
