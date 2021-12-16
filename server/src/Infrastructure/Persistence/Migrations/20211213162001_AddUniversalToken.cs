using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddUniversalToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MasterPasswordHash",
                table: "Users",
                newName: "UniversalToken");

            migrationBuilder.AddColumn<string>(
                name: "EncryptionKeyHash",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptionKeyHash",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UniversalToken",
                table: "Users",
                newName: "MasterPasswordHash");
        }
    }
}
