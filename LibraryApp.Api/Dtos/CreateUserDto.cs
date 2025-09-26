using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Api.Dtos;

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)] 
    public string LastName { get; set; }
}
