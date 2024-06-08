using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAPI.Migrations
{
    public partial class att : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_PositionCompany_PositionCompanyId",
                table: "Person");

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
                name: "Discriminator",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonIncome",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PositionCompanyId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Document);
                    table.ForeignKey(
                        name: "FK_Client_Person_Document",
                        column: x => x.Document,
                        principalTable: "Person",
                        principalColumn: "Document");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PositionCompanyId = table.Column<int>(type: "int", nullable: false),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Document);
                    table.ForeignKey(
                        name: "FK_Employee_Person_Document",
                        column: x => x.Document,
                        principalTable: "Person",
                        principalColumn: "Document");
                    table.ForeignKey(
                        name: "FK_Employee_PositionCompany_PositionCompanyId",
                        column: x => x.PositionCompanyId,
                        principalTable: "PositionCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PositionCompanyId",
                table: "Employee",
                column: "PositionCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Employee");

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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PersonIncome",
                table: "Person",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionCompanyId",
                table: "Person",
                type: "int",
                nullable: true);

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
    }
}
