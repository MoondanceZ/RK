using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RK.Framework.Migrations
{
    public partial class UpdateAccountRecord_Column_AccountTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord");

            migrationBuilder.DropColumn(
                name: "AccountRecordTypeId",
                table: "AccountRecord");

            migrationBuilder.AlterColumn<int>(
                name: "AccountTypeId",
                table: "AccountRecord",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord",
                column: "AccountTypeId",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord");

            migrationBuilder.AlterColumn<int>(
                name: "AccountTypeId",
                table: "AccountRecord",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccountRecordTypeId",
                table: "AccountRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord",
                column: "AccountTypeId",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
