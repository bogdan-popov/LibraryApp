using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BusinessLogic.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private const int MaxBorrowedBooksLimit = 5;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Book> AddBookAsync(Book newBook)
    {
        if (newBook == null) throw new ArgumentNullException(nameof(newBook));

        _unitOfWork.Books.Add(newBook);
        await _unitOfWork.CompleteAsync();

        return newBook;
    }

    public async Task<bool> BorrowBookAsync(int userId, int bookId)
    {
        // Здесь нужны данные о подписке и о книгах пользователя
        // Это два разных запроса, но они необходимы для проверки всех бизнес-правил
        var userWithSub = await _unitOfWork.Users.GetUserWithSubscriptionAsync(userId);
        var userWithBooks = await _unitOfWork.Users.GetUserWithBooksAsync(userId);
        
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);

        if (userWithSub == null || userWithBooks == null || book == null)
            throw new KeyNotFoundException("Данные о пользователе или книге не найдены.");

        if (book.BorrowedByUserId != null)
            throw new InvalidOperationException("Книга уже выдана.");

        if (userWithSub.Subscription == null || userWithSub.Subscription.ExpiryDate <= DateTime.UtcNow)
            throw new InvalidOperationException("У пользователя нет активного абонемента.");

        if (userWithBooks.BorrowedBooks.Count >= MaxBorrowedBooksLimit)
            throw new InvalidOperationException("Пользователь достиг лимита выданных книг.");

        book.BorrowedByUserId = userId;
        _unitOfWork.Books.Update(book);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> ReturnBookAsync(int bookId)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);
        if (book == null || book.BorrowedByUserId == null) return false;

        book.BorrowedByUserId = null;
        _unitOfWork.Books.Update(book);

        await _unitOfWork.CompleteAsync();
        return true;
    }
}
