using Microsoft.EntityFrameworkCore.Migrations;

namespace msac_tpa_new.Migrations
{
    public partial class key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SportmanComissions_Id",
                table: "SportmanComissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SportmanComissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SportmanComissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SportmanComissions_Id",
                table: "SportmanComissions",
                column: "Id");
        }
    }
}
