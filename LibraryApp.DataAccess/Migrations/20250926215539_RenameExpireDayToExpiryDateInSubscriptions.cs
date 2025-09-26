using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameExpireDayToExpiryDateInSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpireDay",
                table: "Subscriptions",
                newName: "ExpiryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Subscriptions",
                newName: "ExpireDay");
        }
    }
}
