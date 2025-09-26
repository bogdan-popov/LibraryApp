using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Book> BorrowedBooks { get; set; } = new();

    // Для выполнения требований ТЗ используется простая связь 1:1, так как требуется проверять только наличие текущего активного абонемента
    // в реальном приложении для хранения истории покупок была бы связь 1:М (List<Subscription>)
    public Subscription? Subscription { get; set; }
}
