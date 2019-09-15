using Microsoft.EntityFrameworkCore.Migrations;

namespace msac_tpa_new.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportmanComission_Comissions_ComissionId",
                table: "SportmanComission");

            migrationBuilder.DropForeignKey(
                name: "FK_SportmanComission_SportMans_SportmanId",
                table: "SportmanComission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportmanComission",
                table: "SportmanComission");

            migrationBuilder.RenameTable(
                name: "SportmanComission",
                newName: "SportmanComissions");

            migrationBuilder.RenameIndex(
                name: "IX_SportmanComission_ComissionId",
                table: "SportmanComissions",
                newName: "IX_SportmanComissions_ComissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportmanComissions",
                table: "SportmanComissions",
                columns: new[] { "SportmanId", "ComissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SportmanComissions_Comissions_ComissionId",
                table: "SportmanComissions",
                column: "ComissionId",
                principalTable: "Comissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportmanComissions_SportMans_SportmanId",
                table: "SportmanComissions",
                column: "SportmanId",
                principalTable: "SportMans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportmanComissions_Comissions_ComissionId",
                table: "SportmanComissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SportmanComissions_SportMans_SportmanId",
                table: "SportmanComissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportmanComissions",
                table: "SportmanComissions");

            migrationBuilder.RenameTable(
                name: "SportmanComissions",
                newName: "SportmanComission");

            migrationBuilder.RenameIndex(
                name: "IX_SportmanComissions_ComissionId",
                table: "SportmanComission",
                newName: "IX_SportmanComission_ComissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportmanComission",
                table: "SportmanComission",
                columns: new[] { "SportmanId", "ComissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SportmanComission_Comissions_ComissionId",
                table: "SportmanComission",
                column: "ComissionId",
                principalTable: "Comissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportmanComission_SportMans_SportmanId",
                table: "SportmanComission",
                column: "SportmanId",
                principalTable: "SportMans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
