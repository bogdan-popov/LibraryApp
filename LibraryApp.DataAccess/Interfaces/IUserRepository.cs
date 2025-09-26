using LibraryApp.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Interfaces;

public interface IUserRepository : IRepository<User>
{
}
