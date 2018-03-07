using Microsoft.EntityFrameworkCore.Migrations;

namespace DetSkerIAarhusBackend.API.Migrations
{
    public partial class moreRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "AssociatedOccurrences",
                newName: "OccurrenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OccurrenceId",
                table: "AssociatedOccurrences",
                newName: "EventId");
        }
    }
}
