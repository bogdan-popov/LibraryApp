using LibraryApp.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    public IUserRepository Users { get; private set; }
    public IBookRepository Books { get; private set; }

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Books = new BookRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
