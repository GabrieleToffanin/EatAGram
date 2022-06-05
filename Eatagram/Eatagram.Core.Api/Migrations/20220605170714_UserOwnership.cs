using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eatagram.Core.Api.Migrations
{
    public partial class UserOwnership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_User_Id",
                table: "Recipes",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_User_Id",
                table: "Recipes",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_User_Id",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_User_Id",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Recipes");

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Bona", "Pasta" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Bona", "Pasta" });
        }
    }
}
