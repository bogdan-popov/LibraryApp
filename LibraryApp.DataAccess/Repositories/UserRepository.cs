using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(LibraryDbContext context) : base(context)
    {
    }

    public async Task<User?> GetUserWithBooksAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.BorrowedBooks)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserWithSubscriptionAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Subscription)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
