using Microsoft.EntityFrameworkCore.Migrations;

namespace SoloProject.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUser_Products_ProductId",
                table: "ProductUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUser_Users_UserId",
                table: "ProductUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUser",
                table: "ProductUser");

            migrationBuilder.RenameTable(
                name: "ProductUser",
                newName: "ProductUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUser_UserId",
                table: "ProductUsers",
                newName: "IX_ProductUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUser_ProductId",
                table: "ProductUsers",
                newName: "IX_ProductUsers_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers",
                column: "ProductUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUsers_Products_ProductId",
                table: "ProductUsers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUsers_Users_UserId",
                table: "ProductUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUsers_Products_ProductId",
                table: "ProductUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUsers_Users_UserId",
                table: "ProductUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers");

            migrationBuilder.RenameTable(
                name: "ProductUsers",
                newName: "ProductUser");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUsers_UserId",
                table: "ProductUser",
                newName: "IX_ProductUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUsers_ProductId",
                table: "ProductUser",
                newName: "IX_ProductUser_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUser",
                table: "ProductUser",
                column: "ProductUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUser_Products_ProductId",
                table: "ProductUser",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUser_Users_UserId",
                table: "ProductUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
