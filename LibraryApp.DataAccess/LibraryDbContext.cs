using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.BorrowedBooks)
            .WithOne(b => b.BorrowedByUser)
            .HasForeignKey(b => b.BorrowedByUserId)
            .OnDelete(DeleteBehavior.SetNull); 
            // Выбрано поведение SetNull, а не Cascade. При удалении пользователя его книги не удаляются из БД, а просто возвращаются в библиотеку (BorrowedByUserId = null)


        modelBuilder.Entity<User>()
            .HasOne(u => u.Subscription)
            .WithOne(s => s.User)
            .HasForeignKey<Subscription>(s => s.UserId);
    }
}
