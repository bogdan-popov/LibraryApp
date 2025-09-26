using LibraryApp.Api.Dtos;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> AddBook([FromBody] CreateBookDto createBookDto)
    {
        var newBook = new Book
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
        };

        var createdBook = await _bookService.AddBookAsync(newBook);

        var bookDto = new BookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
        };

        return CreatedAtAction(nameof(AddBook), new { id = bookDto.Id }, bookDto);
    }


    [HttpPost("{bookId}/borrow/{userId}")]
    public async Task<IActionResult> BorrowBook(int bookId, int userId)
    {
        try
        {
            var result = await _bookService.BorrowBookAsync(userId, bookId);
            return Ok("Книга успешно выдана пользователю.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("{bookId}/return")]
    public async Task<IActionResult> ReturnBook(int bookId)
    {
        var result = await _bookService.ReturnBookAsync(bookId);
        if (!result) return NotFound("Книга не найдена или не была выдана ранее.");

        return Ok("Книга успешно возвращена в библиотеку.");
    }
}
