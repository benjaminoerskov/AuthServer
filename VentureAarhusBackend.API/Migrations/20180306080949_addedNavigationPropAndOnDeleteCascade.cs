using Microsoft.EntityFrameworkCore.Migrations;

namespace VentureAarhusBackend.API.Migrations
{
    public partial class addedNavigationPropAndOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedOccurrences_AspNetUsers_ApplicationUserId",
                table: "AssociatedOccurrences");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedOccurrences_AspNetUsers_ApplicationUserId",
                table: "AssociatedOccurrences",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedOccurrences_AspNetUsers_ApplicationUserId",
                table: "AssociatedOccurrences");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedOccurrences_AspNetUsers_ApplicationUserId",
                table: "AssociatedOccurrences",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
