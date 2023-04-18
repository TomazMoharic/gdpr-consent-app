using System.Diagnostics;
using GdprConsentApp.Database;
using GdprConsentApp.Exceptions;
using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using GdprConsentApp.Logging;
using GdprConsentApp.Mappers;
using GdprConsentApp.Repository.Interfaces;

namespace GdprConsentApp.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILoggerAdapter<UsersService> _logger;
    
    public UsersService(IUsersRepository usersRepository, ILoggerAdapter<UsersService> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }
    
    public List<UserSimpleModel> GetUsers(GetUsersRequestModel model)
    {
            _logger.LogInformation("Retrieving all users.");
            Stopwatch stopWatch = Stopwatch.StartNew();
            
            try
            {
                return _usersRepository.GetUsersFromDb(model.Query);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving users.");
                throw new InternalServerException(e.Message);
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("Users retrieved in {0}ms.", stopWatch.ElapsedMilliseconds);
            }
    }

    public UserModel GetById(int idUser)
    {
        _logger.LogInformation("Retrieving user with id: {0}.", idUser);
        Stopwatch stopWatch = Stopwatch.StartNew();
        
        try
        {
            UserModel? user = _usersRepository.GetUserWithConsentsFromDb(idUser);

            if (user is null)
            {
                throw new EntityNotFoundException("User with this id does not exist.");
            }

            return user;
        }
        catch (EntityNotFoundException e)
        {
            _logger.LogError(e, "User with id {0} does not exist in the Db.", idUser);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while retrieving user with id {0}.", idUser);
            throw new InternalServerException(e.Message);
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id {0} retrieved in {1}ms.", idUser, stopWatch.ElapsedMilliseconds);
        }
    }

    public EntityCreatedResponseModel Create(CreateUserRequestModel model)
    {
        _logger.LogInformation("Creating new user.");
        Stopwatch stopWatch = Stopwatch.StartNew();

        try
        {
            bool userWithThisEmailAlreadyExists = _usersRepository.CheckIfUserWithThisEmailExistsInDb(model.Email);

            if (userWithThisEmailAlreadyExists)
            {
                throw new BadRequestException("User with this email already exists.");
            }

            int insertedUserId = _usersRepository.InsertUserToDb(model.ToUserDbModel());

            return new EntityCreatedResponseModel(insertedUserId, "User successfully created.");
        }
        catch (BadRequestException e)
        {
            _logger.LogError(e, "User with email {0} already exists.", model.Email);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while creating the new user.");
            throw new InternalServerException(e.Message);
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User created in {0}ms.", stopWatch.ElapsedMilliseconds);
        }
    }

    public EntityUpdatedResponseModel Update(int idUser, UpdateUserRequestModel model)
    {
        _logger.LogInformation("Updating user with id {0}.", idUser);
        Stopwatch stopWatch = Stopwatch.StartNew();
        
        try
        {
            TUsers? user = _usersRepository.GetUserFromDb(idUser);

            if (user is null)
            {
                throw new EntityNotFoundException("User with this id does not exist.");
            }

            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName = model.FirstName ?? user.LastName;

            _usersRepository.UpdateUserInDb(user);

            return new EntityUpdatedResponseModel("User successfully updated.");
        }
        catch (EntityNotFoundException e)
        {
            _logger.LogError(e, "User with id {0} does not exist in the Db.", idUser);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while updating the user with id {0} in the Db.", idUser);
            throw new InternalServerException(e.Message);
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id {0} updated in {1}ms.", idUser, stopWatch.ElapsedMilliseconds);
        }
    }

    public EntityDeletedResponseModel Delete(int idUser)
    {
        _logger.LogInformation("Deleting user with id {0}.", idUser);
        Stopwatch stopWatch = Stopwatch.StartNew();
        
        try
        {
            TUsers? user = _usersRepository.GetUserFromDb(idUser);

            if (user is null)
            {
                throw new EntityNotFoundException("User with this id does not exist.");
            }
            
            _usersRepository.DeleteUserFromDb(user);
            
            return new EntityDeletedResponseModel("User successfully deleted.");
        }
        catch (EntityNotFoundException e)
        {
            _logger.LogError(e, "User with id {0} does not exist in the Db.", idUser);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while deleting the user with id {0} from the Db.", idUser);
            throw new InternalServerException(e.Message);
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id {0} deleted in {1}ms.", idUser, stopWatch.ElapsedMilliseconds );
        }
    }
}