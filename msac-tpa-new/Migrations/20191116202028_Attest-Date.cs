using Microsoft.EntityFrameworkCore.Migrations;

namespace msac_tpa_new.Migrations
{
    public partial class AttestDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attestations_Regions_RegionId",
                table: "Attestations");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Attestations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attestations_Regions_RegionId",
                table: "Attestations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attestations_Regions_RegionId",
                table: "Attestations");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Attestations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Attestations_Regions_RegionId",
                table: "Attestations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
