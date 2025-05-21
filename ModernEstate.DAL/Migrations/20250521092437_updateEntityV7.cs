using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_City",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_Address_District",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_Address_HouseNumber",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_Address_Street",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_Address_Ward",
                table: "address");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "owner_property",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "invetor",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "employee",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "broker",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "UX_Address_FullAddress",
                table: "address",
                columns: new[] { "house_number", "street", "ward", "district", "city", "country" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Address_FullAddress",
                table: "address");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "owner_property",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "invetor",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "employee",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "broker",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Address_City",
                table: "address",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_Address_District",
                table: "address",
                column: "district");

            migrationBuilder.CreateIndex(
                name: "IX_Address_HouseNumber",
                table: "address",
                column: "house_number");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Street",
                table: "address",
                column: "street");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Ward",
                table: "address",
                column: "ward");
        }
    }
}
