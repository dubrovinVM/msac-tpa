using Microsoft.EntityFrameworkCore.Migrations;

namespace msac_tpa_new.Migrations
{
    public partial class Attestauthroles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegionId",
                table: "Users",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Regions_RegionId",
                table: "Users",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Regions_RegionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RegionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Users");
        }
    }
}
