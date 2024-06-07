using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAPI.Migrations
{
    public partial class employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "Person",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionPercentage",
                table: "Person",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionCompanyId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PositionCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionCompany", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PositionCompanyId",
                table: "Person",
                column: "PositionCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_PositionCompany_PositionCompanyId",
                table: "Person",
                column: "PositionCompanyId",
                principalTable: "PositionCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_PositionCompany_PositionCompanyId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "PositionCompany");

            migrationBuilder.DropIndex(
                name: "IX_Person_PositionCompanyId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CommissionPercentage",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PositionCompanyId",
                table: "Person");
        }
    }
}
