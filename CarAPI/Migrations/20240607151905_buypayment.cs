using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAPI.Migrations
{
    public partial class buypayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_CreditCard_CreditCardCardNumber",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "CreditCardCardNumber",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Buy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buy_Car_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Car",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buy_CarLicensePlate",
                table: "Buy",
                column: "CarLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_CreditCard_CreditCardCardNumber",
                table: "Payment",
                column: "CreditCardCardNumber",
                principalTable: "CreditCard",
                principalColumn: "CardNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_CreditCard_CreditCardCardNumber",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "Buy");

            migrationBuilder.AlterColumn<string>(
                name: "CreditCardCardNumber",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_CreditCard_CreditCardCardNumber",
                table: "Payment",
                column: "CreditCardCardNumber",
                principalTable: "CreditCard",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
