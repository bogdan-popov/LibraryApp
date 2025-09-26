using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<User> CreateUserAsync(User newUser)
    {
        if (newUser == null) throw new ArgumentNullException(nameof(newUser));

        _unitOfWork.Users.Add(newUser);
        await _unitOfWork.CompleteAsync();

        return newUser;
    }

    public async Task<bool> UpdateUserAsync(User userToUpdate)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(userToUpdate.Id);
        if (existingUser == null) return false;

        existingUser.FirstName = userToUpdate.FirstName;
        existingUser.LastName = userToUpdate.LastName;

        _unitOfWork.Users.Update(existingUser);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var userToDelete = await _unitOfWork.Users.GetByIdAsync(userId);
        if (userToDelete == null) return false;

        _unitOfWork.Users.Delete(userToDelete);
        await _unitOfWork.CompleteAsync();

        return true;
    }


    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _unitOfWork.Users.GetAllUsersWithSubscriptionsAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetUserWithBooksAsync(userId);
    }


    public async Task<Subscription?> CreateSubscriptionAsync(int userId, DateTime expiryDate)
    {
        var user = await _unitOfWork.Users.GetUserWithSubscriptionAsync(userId);
        if (user == null) return null;

        if (user.Subscription != null && user.Subscription.ExpiryDate > DateTime.UtcNow)
            throw new InvalidOperationException("У пользователя есть активный абонемент.");

        var newSubscription = new Subscription
        {
            UserId = userId,
            ExpiryDate = expiryDate,
        };

        user.Subscription = newSubscription;
        await _unitOfWork.CompleteAsync();


        return newSubscription;
    }
}
