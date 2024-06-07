using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAPI.Migrations
{
    public partial class alltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientDocument = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeeDocument = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Car_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Car",
                        principalColumn: "LicensePlate");
                    table.ForeignKey(
                        name: "FK_Sale_Client_ClientDocument",
                        column: x => x.ClientDocument,
                        principalTable: "Client",
                        principalColumn: "Document");
                    table.ForeignKey(
                        name: "FK_Sale_Employee_EmployeeDocument",
                        column: x => x.EmployeeDocument,
                        principalTable: "Employee",
                        principalColumn: "Document");
                    table.ForeignKey(
                        name: "FK_Sale_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarJob_Car_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Car",
                        principalColumn: "LicensePlate");
                    table.ForeignKey(
                        name: "FK_CarJob_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarJob_CarLicensePlate",
                table: "CarJob",
                column: "CarLicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_CarJob_JobId",
                table: "CarJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CarLicensePlate",
                table: "Sale",
                column: "CarLicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ClientDocument",
                table: "Sale",
                column: "ClientDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_EmployeeDocument",
                table: "Sale",
                column: "EmployeeDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_PaymentId",
                table: "Sale",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarJob");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Job");
        }
    }
}
