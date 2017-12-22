using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RK.Framework.Migrations
{
    public partial class Add_Record : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(65, 30)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Remark = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IconCss = table.Column<string>(type: "longtext", nullable: true),
                    IconName = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordType_Record_Id",
                        column: x => x.Id,
                        principalTable: "Record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordType");

            migrationBuilder.DropTable(
                name: "Record");
        }
    }
}
