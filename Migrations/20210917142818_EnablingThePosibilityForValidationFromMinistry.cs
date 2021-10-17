using Microsoft.EntityFrameworkCore.Migrations;

namespace VacunaAPI.Migrations
{
    public partial class EnablingThePosibilityForValidationFromMinistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasValidated",
                table: "Inmunizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMinistry",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VacunationCenterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vacunationCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacunationCenters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VacunationCenterId",
                table: "AspNetUsers",
                column: "VacunationCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_vacunationCenters_VacunationCenterId",
                table: "AspNetUsers",
                column: "VacunationCenterId",
                principalTable: "vacunationCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_vacunationCenters_VacunationCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "vacunationCenters");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VacunationCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WasValidated",
                table: "Inmunizations");

            migrationBuilder.DropColumn(
                name: "IsFromMinistry",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VacunationCenterId",
                table: "AspNetUsers");
        }
    }
}
