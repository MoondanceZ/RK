using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RK.Framework.Migrations
{
    public partial class UpdateAccountRecord_Column_UserInfoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccountRecord");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "AccountRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "AccountRecord");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AccountRecord",
                nullable: false,
                defaultValue: 0);
        }
    }
}
