using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using Xunit;

namespace GdprConsentApp.Tests.Integration.UsersController;

public class GetUsersControllerTests : IClassFixture<GdprConsentAppFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CreateUserRequestModel> _createUsersRequestsGenerator = new Faker<CreateUserRequestModel>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
        .RuleFor(x => x.LastName, faker => faker.Person.LastName);

    public GetUsersControllerTests(GdprConsentAppFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ReturnsUsers_WhenUsersExist()
    {
        // Arrange
        List<CreateUserRequestModel> createUsersRequestsModels = Enumerable.Range(1, 1).Select(num => _createUsersRequestsGenerator.Generate()).ToList();

        List<UserSimpleModel> createdUsers = new();
        
        foreach (CreateUserRequestModel createUserRequestModel in createUsersRequestsModels)
        {
            HttpResponseMessage userCreatedResponse = await _client.PostAsJsonAsync("Users", createUserRequestModel);

            EntityCreatedResponseModel? responseModel =
                await userCreatedResponse.Content.ReadFromJsonAsync<EntityCreatedResponseModel>();

            if (responseModel is not null)
            {
                UserSimpleModel userModel = new()
                {
                    IdUser = responseModel.IdCreatedEntity,
                    FirstName = createUserRequestModel.FirstName,
                    LastName = createUserRequestModel.LastName,
                    Email = createUserRequestModel.Email,
                };
                
                createdUsers.Add(userModel);
            }
        }
        
        // Act
        HttpResponseMessage response = await _client.GetAsync("Users");

        // Assert
        List<UserSimpleModel>? returnedUsers =
            await response.Content.ReadFromJsonAsync<List<UserSimpleModel>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        returnedUsers!.Should().BeEquivalentTo(createdUsers);
        
        // For some reason, for me the unhappy path happens first... Maybe it is faster? But just in case...
        
        // Cleanup
        _client.Dispose();
    }
    //
    // [Fact]
    // public async Task GetAll_ReturnsEmptyResult_WhenNoCustomersExist()
    // {
    //     // Arrange  
    //     
    //     // Act
    //     HttpResponseMessage response = await _client.GetAsync("customers");
    //     
    //     // Assert
    //     GetAllCustomersResponse? returnedCustomers =
    //         await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
    //     
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //
    //     returnedCustomers!.Customers.Should().BeEquivalentTo(Enumerable.Empty<CustomerResponse>());
    // }
}