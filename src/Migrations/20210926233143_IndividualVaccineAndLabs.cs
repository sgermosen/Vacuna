using Microsoft.EntityFrameworkCore.Migrations;

namespace VacunaAPI.Migrations
{
    public partial class IndividualVaccineAndLabs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_vacunationCenters_VacunationCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vacunationCenters",
                table: "vacunationCenters");

            migrationBuilder.DropColumn(
                name: "Laboratory",
                table: "Inmunizations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Inmunizations");

            migrationBuilder.RenameTable(
                name: "vacunationCenters",
                newName: "VacunationCenters");

            migrationBuilder.RenameColumn(
                name: "Inmunizator",
                table: "Inmunizations",
                newName: "Immunizator");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "Inmunizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VaccineId",
                table: "Inmunizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacunationCenters",
                table: "VacunationCenters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inmunizations_LaboratoryId",
                table: "Inmunizations",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inmunizations_VaccineId",
                table: "Inmunizations",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_VacunationCenters_VacunationCenterId",
                table: "AspNetUsers",
                column: "VacunationCenterId",
                principalTable: "VacunationCenters",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_VacunationCenters_VacunationCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmunizations_Laboratories_LaboratoryId",
                table: "Inmunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmunizations_Vaccines_VaccineId",
                table: "Inmunizations");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacunationCenters",
                table: "VacunationCenters");

            migrationBuilder.DropIndex(
                name: "IX_Inmunizations_LaboratoryId",
                table: "Inmunizations");

            migrationBuilder.DropIndex(
                name: "IX_Inmunizations_VaccineId",
                table: "Inmunizations");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "Inmunizations");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "Inmunizations");

            migrationBuilder.RenameTable(
                name: "VacunationCenters",
                newName: "vacunationCenters");

            migrationBuilder.RenameColumn(
                name: "Immunizator",
                table: "Inmunizations",
                newName: "Inmunizator");

            migrationBuilder.AddColumn<string>(
                name: "Laboratory",
                table: "Inmunizations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Inmunizations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vacunationCenters",
                table: "vacunationCenters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_vacunationCenters_VacunationCenterId",
                table: "AspNetUsers",
                column: "VacunationCenterId",
                principalTable: "vacunationCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
