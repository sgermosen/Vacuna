using Microsoft.EntityFrameworkCore.Migrations;

namespace VacunaAPI.Migrations
{
    public partial class RenamingImmunization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inmunizations_AspNetUsers_UserId",
                table: "Inmunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmunizations_Laboratories_LaboratoryId",
                table: "Inmunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmunizations_Vaccines_VaccineId",
                table: "Inmunizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inmunizations",
                table: "Inmunizations");

            migrationBuilder.RenameTable(
                name: "Inmunizations",
                newName: "Immunizations");

            migrationBuilder.RenameIndex(
                name: "IX_Inmunizations_VaccineId",
                table: "Immunizations",
                newName: "IX_Immunizations_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Inmunizations_UserId",
                table: "Immunizations",
                newName: "IX_Immunizations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Inmunizations_LaboratoryId",
                table: "Immunizations",
                newName: "IX_Immunizations_LaboratoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Immunizations",
                table: "Immunizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Immunizations_AspNetUsers_UserId",
                table: "Immunizations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Immunizations_Laboratories_LaboratoryId",
                table: "Immunizations",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Immunizations_Vaccines_VaccineId",
                table: "Immunizations",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Immunizations_AspNetUsers_UserId",
                table: "Immunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Immunizations_Laboratories_LaboratoryId",
                table: "Immunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Immunizations_Vaccines_VaccineId",
                table: "Immunizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Immunizations",
                table: "Immunizations");

            migrationBuilder.RenameTable(
                name: "Immunizations",
                newName: "Inmunizations");

            migrationBuilder.RenameIndex(
                name: "IX_Immunizations_VaccineId",
                table: "Inmunizations",
                newName: "IX_Inmunizations_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Immunizations_UserId",
                table: "Inmunizations",
                newName: "IX_Inmunizations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Immunizations_LaboratoryId",
                table: "Inmunizations",
                newName: "IX_Inmunizations_LaboratoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inmunizations",
                table: "Inmunizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inmunizations_AspNetUsers_UserId",
                table: "Inmunizations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inmunizations_Laboratories_LaboratoryId",
                table: "Inmunizations",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inmunizations_Vaccines_VaccineId",
                table: "Inmunizations",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
