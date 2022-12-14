using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share2Connect.Api.Migrations
{
    public partial class UpdateImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ImageFile");

            migrationBuilder.AddColumn<byte>(
                name: "ImageByte",
                table: "ImageFile",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "ImageFile");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ImageFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
