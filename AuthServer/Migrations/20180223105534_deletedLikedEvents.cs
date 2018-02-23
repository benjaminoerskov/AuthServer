using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuthServer.Migrations
{
    public partial class deletedLikedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "AssociatedEvents");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AssociatedEvents",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AssociatedEvents",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "AssociatedEvents",
                nullable: false,
                defaultValue: 0);
        }
    }
}
