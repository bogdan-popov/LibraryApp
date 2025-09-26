using LibraryApp.Api.Dtos;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        var userDto = users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            HasActiveSubscription = u.Subscription != null && u.Subscription.ExpiryDate > DateTime.UtcNow,
        });

        return Ok(userDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null) return NotFound();

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            HasActiveSubscription = user.Subscription != null && user.Subscription.ExpiryDate > DateTime.UtcNow,
            BorrowedBooks = user.BorrowedBooks.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
            }).ToList(),
        };

        return Ok(userDto);
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var newUser = new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
        };

        var createdUser = await _userService.CreateUserAsync(newUser);

        var userDto = new UserDto
        {
            Id = createdUser.Id,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            HasActiveSubscription = false,
        };

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto updateUserDto)
    {
        var userToUp = new User
        {
            Id = id,
            FirstName = updateUserDto.FirstName,
            LastName = updateUserDto.LastName,
        };

        var result = await _userService.UpdateUserAsync(userToUp);

        if (!result) return NotFound();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (!result) return NotFound();

        return NoContent();
    }


    [HttpPost("{id}/subscription")]
    public async Task<IActionResult> CreateSubscription(int id)
    {
        try
        {
            var expireDate = DateTime.UtcNow.AddDays(30);
            var subscription = await _userService.CreateSubscriptionAsync(id, expireDate);

            if (subscription == null) return NotFound("Пользователь не найден");

            return Ok("Абонемент успешно оформлен.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
