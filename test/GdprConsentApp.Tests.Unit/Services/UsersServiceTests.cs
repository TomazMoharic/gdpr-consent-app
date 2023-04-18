using System.Net;
using Bogus;
using FluentAssertions;
using GdprConsentApp.Database;
using GdprConsentApp.Exceptions;
using GdprConsentApp.Logging;
using GdprConsentApp.Models;
using GdprConsentApp.Repository.Interfaces;
using GdprConsentApp.RequestModels;
using GdprConsentApp.Services;
using MySqlConnector;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;


namespace GdprConsentApp.Tests.Unit.Services;

public class UsersServiceTests
{
    private readonly UsersService _sut;
    private readonly IUsersRepository _userRepository = Substitute.For<IUsersRepository>();
    private readonly ILoggerAdapter<UsersService> _logger = Substitute.For<ILoggerAdapter<UsersService>>();

    private readonly Faker<UserSimpleModel> _usersGenerator = new Faker<UserSimpleModel>()
        .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
        .RuleFor(x => x.LastName, faker => faker.Person.LastName)
        .RuleFor(x => x.Email, faker => faker.Person.Email);

    private static readonly Faker<ConsentSimpleModel> _consentsGenerator = new Faker<ConsentSimpleModel>()
        .RuleFor(x => x.IdConsent, faker => faker.IndexFaker)
        .RuleFor(x => x.Name, faker => faker.Lorem.Word())
        .RuleFor(x => x.Code, faker => faker.Lorem.Word())
        .RuleFor(x => x.ConsentDurationDays, faker => faker.Random.Int(100));

    private readonly Faker<UserModel> _userGenerator = new Faker<UserModel>()
        .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
        .RuleFor(x => x.LastName, faker => faker.Person.LastName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.IdUser, faker => faker.IndexFaker)
        .RuleFor(x => x.CreatedAt, faker => faker.Date.Past(1, DateTime.Today))
        .RuleFor(x => x.UpdatedAt, faker => faker.Date.Past(1, DateTime.Today))
        .RuleFor(x => x.Consents, faker =>  _consentsGenerator.Generate(3).ToList());

    public UsersServiceTests()
    {
        _sut = new UsersService(_userRepository, _logger);
    }

    [Fact]
    public void GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        GetUsersRequestModel requestModel = new()
        {
            Query = "string"
        };

        _userRepository.GetUsersFromDb(Arg.Any<string>()).Returns(new List<UserSimpleModel>());

        // Act
        List<UserSimpleModel> result = _sut.GetUsers(requestModel);

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void GetUsers_ShouldReturnAllUsers_WhenSomeUsersExist()
    {
        // Arrange
        GetUsersRequestModel requestModel = new()
        {
            Query = "string"
        };
        
        List<UserSimpleModel> users = Enumerable.Range(1, 1).Select(num => _usersGenerator.Generate()).ToList();

        _userRepository.GetUsersFromDb(Arg.Any<string>()).Returns(users);

        // Act
        List<UserSimpleModel> result =  _sut.GetUsers(requestModel);

        // Assert
        result.Should().BeEquivalentTo(users);
    }
    
    [Fact]
    public void GetUsers_ShouldLogMessages_WhenInvoked()
    {
        // Arrange
        GetUsersRequestModel requestModel = new()
        {
            Query = "string"
        };
        
        _userRepository.GetUsersFromDb(Arg.Any<string>()).Returns(new List<UserSimpleModel>());

        // Act
         _sut.GetUsers(requestModel);

        // Assert
        _logger.Received(1).LogInformation(Arg.Is("Retrieving all users."));
        _logger.Received(1).LogInformation(Arg.Is("Users retrieved in {0}ms."), Arg.Any<long>());
    }

    [Fact]
    public void GetUsers_ShouldLogMessageAndException_WhenExceptionIsThrown()
    {
        // Arrange
        GetUsersRequestModel requestModel = new()
        {
            Query = "string"
        };
        
        Exception exception = new ("Something went wrong.");
        _userRepository.GetUsersFromDb(Arg.Any<string>()).Throws(exception);

        // Act
        var func = () => _sut.GetUsers(requestModel);

        // Assert
        func.Should().Throw<InternalServerException>().WithMessage("Something went wrong.");
        _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong while retrieving users."));
    }

    [Fact]
    public void GetById_ShouldReturnASpecificUser_WhenThatUserExists()
    {
        // Arrange
        int idUser = new Random().Next(1, 1000);
        
        UserModel user = _userGenerator.Generate();

        _userRepository.GetUserWithConsentsFromDb(Arg.Any<int>()).Returns(user);
        
        // Act 
        UserModel response = _sut.GetById(idUser);
        
        // Assert
        response.Should().BeEquivalentTo(user);
    }

    [Fact]
    public void GetById_ShouldLogInformation_WhenSpecificUserExists()
    {
        // Arrange  
        int idUser = new Random().Next(1, 1000);
        
        UserModel user = _userGenerator.Generate();

        _userRepository.GetUserWithConsentsFromDb(Arg.Any<int>()).Returns(user);
        
        // Act
        _sut.GetById(idUser);
        
        // Assert
        _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}."), idUser);
        _logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms."), idUser, Arg.Any<long>());
    }

    [Fact]
    public void GetById_ShouldLogAndThrowNotFoundException_WhenTheUserDoesNotExist()
    {
        // Arrange
        int idUser = new Random().Next(1, 1000);
        
        _userRepository.GetUserWithConsentsFromDb(Arg.Any<int>()).ReturnsNull();
        // Act
        var func = () => _sut.GetById(idUser);

        // Assert
        func.Should().Throw<EntityNotFoundException>().WithMessage("User with this id does not exist.");
        _logger.Received(1)
            .LogError(
            Arg.Is<EntityNotFoundException>(e => e.Message == "User with this id does not exist."), 
            Arg.Is("User with id {0} does not exist in the Db."), 
            Arg.Is(idUser));
    }

    [Fact]
    public void GetById_ShouldLogAndThrowException_WhenExceptionIsThrown()
    {
        // Arrange
        int idUser = new Random().Next(1, 1000);

        Exception exception = new("Something went wrong.");
        
        _userRepository.GetUserWithConsentsFromDb(Arg.Any<int>()).Throws(exception);
        // Act
        var func = () => _sut.GetById(idUser);

        // Assert
        func.Should().Throw<InternalServerException>()
            .WithMessage("Something went wrong.");
        _logger.Received(1)
            .LogError(
                Arg.Is(exception), 
                Arg.Is("Something went wrong while retrieving user with id {0}."), 
                Arg.Is(idUser));
    }
}