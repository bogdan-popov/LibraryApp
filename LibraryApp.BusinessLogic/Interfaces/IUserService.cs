using LibraryApp.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(User newUser);
    Task<bool> UpdateUserAsync(User userToUpdate);
    Task<bool> DeleteUserAsync(int userId);

    // Метод по созданию подписки находится в IUserService, а не в отдельном ISubscriptionService для упрощения архитектуры в рамках тестового задания
    // Это отражает бизнес-требование "установить, что пользователь купил абонемент"
    // В более сложной системе с несколькими операциями я бы вынес логику подписок в отдельный сервис для соблюдения SRP
    Task<Subscription?> CreateSubscriptionAsync(int userId, DateTime expiryDate);
}
