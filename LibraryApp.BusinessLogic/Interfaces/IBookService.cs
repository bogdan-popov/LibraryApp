using LibraryApp.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BusinessLogic.Interfaces;

public interface IBookService
{
    Task<Book> AddBookAsync(Book newBook);
    Task<bool> BorrowBookAsync(int userId, int bookId);
    Task<bool> ReturnBookAsync(int bookId);
}
