namespace LibraryApp.Api.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<BookDto> BorrowedBooks { get; set; } = new();
    public bool HasActiveSubscription { get; set; }
}
