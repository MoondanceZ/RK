using Microsoft.EntityFrameworkCore.Migrations;

namespace RK.Framework.Migrations
{
    public partial class UpdateUserInfo_AddColumn_OpenId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QQOpenID",
                table: "UserInfo",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeiboOpenId",
                table: "UserInfo",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QQOpenID",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "WeiboOpenId",
                table: "UserInfo");
        }
    }
}
