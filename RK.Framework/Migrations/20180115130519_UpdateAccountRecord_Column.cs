using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RK.Framework.Migrations
{
    public partial class UpdateAccountRecord_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRecord_AccountType_RecordTypeId",
                table: "AccountRecord");

            migrationBuilder.DropIndex(
                name: "IX_AccountRecord_RecordTypeId",
                table: "AccountRecord");

            migrationBuilder.DropColumn(
                name: "RecordTypeId",
                table: "AccountRecord");

            migrationBuilder.AddColumn<string>(
                name: "AccountDate",
                table: "AccountRecord",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "AccountRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRecord_AccountTypeId",
                table: "AccountRecord",
                column: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord",
                column: "AccountTypeId",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRecord_AccountType_AccountTypeId",
                table: "AccountRecord");

            migrationBuilder.DropIndex(
                name: "IX_AccountRecord_AccountTypeId",
                table: "AccountRecord");

            migrationBuilder.DropColumn(
                name: "AccountDate",
                table: "AccountRecord");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "AccountRecord");

            migrationBuilder.AddColumn<int>(
                name: "RecordTypeId",
                table: "AccountRecord",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRecord_RecordTypeId",
                table: "AccountRecord",
                column: "RecordTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRecord_AccountType_RecordTypeId",
                table: "AccountRecord",
                column: "RecordTypeId",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
