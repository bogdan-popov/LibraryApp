using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entites;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }

    // Автор оставлен строковым полем, а не вынесен в отдельную сущность, тк в рамках тз не требуется функциональность по управлению авторами или поиску по ним
    // Это упрощает модель и полностью соответствует поставленным задачам
    public string Author { get; set; }

    public int? BorrowedByUserId { get; set; }
    public User? BorrowedByUser { get; set; }
}
