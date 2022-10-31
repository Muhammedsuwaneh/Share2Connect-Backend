using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share2Connect.Api.Migrations.ApplicationDb
{
    public partial class AddAnnouncementTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place_gps = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    DataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Post_id);
                    table.ForeignKey(
                        name: "FK_Announcements_AnnouncementData_DataId",
                        column: x => x.DataId,
                        principalTable: "AnnouncementData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    user = table.Column<int>(type: "int", nullable: false),
                    AnnouncementDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.user);
                    table.ForeignKey(
                        name: "FK_Participant_AnnouncementData_AnnouncementDataId",
                        column: x => x.AnnouncementDataId,
                        principalTable: "AnnouncementData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_DataId",
                table: "Announcements",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_AnnouncementDataId",
                table: "Participant",
                column: "AnnouncementDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "AnnouncementData");
        }
    }
}
