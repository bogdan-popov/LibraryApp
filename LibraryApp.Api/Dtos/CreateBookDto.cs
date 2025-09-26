using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Api.Dtos;

public class CreateBookDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(100)]
    public string Author { get; set; }
}
