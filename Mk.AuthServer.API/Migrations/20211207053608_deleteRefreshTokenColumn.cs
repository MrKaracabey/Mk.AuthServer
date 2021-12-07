using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mk.AuthServer.API.Migrations
{
    public partial class deleteRefreshTokenColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserRefreshTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserRefreshTokens",
                type: "text",
                nullable: true);
        }
    }
}
