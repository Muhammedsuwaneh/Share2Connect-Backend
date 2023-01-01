using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share2Connect.Api.Migrations
{
    public partial class allModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adNameText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adDescText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adImage = table.Column<byte>(type: "tinyint", nullable: true),
                    adClubName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adDateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adTicketText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adPriceText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adSeatText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adPlaceText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adPlaceGPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adRouteStartText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adRouteEndText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adRouteStartGPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adRouteEndGPS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userNameText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userImage = table.Column<byte>(type: "tinyint", nullable: true),
                    userDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    dataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Announcements_AnnouncementData_dataId",
                        column: x => x.dataId,
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
                name: "IX_Announcements_dataId",
                table: "Announcements",
                column: "dataId");

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
                name: "Users");

            migrationBuilder.DropTable(
                name: "AnnouncementData");
        }
    }
}
