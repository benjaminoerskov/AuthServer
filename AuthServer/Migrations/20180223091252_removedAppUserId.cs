using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuthServer.Migrations
{
    public partial class removedAppUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedEvents_AspNetUsers_ApplicationUserId1",
                table: "AssociatedEvents");

            migrationBuilder.DropIndex(
                name: "IX_LikedEvents_ApplicationUserId1",
                table: "AssociatedEvents");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "AssociatedEvents");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "AssociatedEvents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_LikedEvents_ApplicationUserId",
                table: "AssociatedEvents",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedEvents_AspNetUsers_ApplicationUserId",
                table: "AssociatedEvents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedEvents_AspNetUsers_ApplicationUserId",
                table: "AssociatedEvents");

            migrationBuilder.DropIndex(
                name: "IX_LikedEvents_ApplicationUserId",
                table: "AssociatedEvents");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "AssociatedEvents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "AssociatedEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikedEvents_ApplicationUserId1",
                table: "AssociatedEvents",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedEvents_AspNetUsers_ApplicationUserId1",
                table: "AssociatedEvents",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
