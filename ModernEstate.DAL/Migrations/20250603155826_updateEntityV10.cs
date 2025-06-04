using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_package_access_priority",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_package_highlighted",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_package_top_listing",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_package_type_package",
                table: "package");

            migrationBuilder.DropColumn(
                name: "access_priority",
                table: "package");

            migrationBuilder.DropColumn(
                name: "duration_days",
                table: "package");

            migrationBuilder.AlterColumn<Guid>(
                name: "provide_id",
                table: "service",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "package_name",
                table: "property",
                type: "VARCHAR(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service",
                column: "provide_id",
                principalTable: "provide",
                principalColumn: "provide_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service");

            migrationBuilder.DropColumn(
                name: "package_name",
                table: "property");

            migrationBuilder.AlterColumn<Guid>(
                name: "provide_id",
                table: "service",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "access_priority",
                table: "package",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "duration_days",
                table: "package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_package_access_priority",
                table: "package",
                column: "access_priority");

            migrationBuilder.CreateIndex(
                name: "IX_package_highlighted",
                table: "package",
                column: "highlighted");

            migrationBuilder.CreateIndex(
                name: "IX_package_top_listing",
                table: "package",
                column: "top_listing");

            migrationBuilder.CreateIndex(
                name: "IX_package_type_package",
                table: "package",
                column: "type_package");

            migrationBuilder.AddForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service",
                column: "provide_id",
                principalTable: "provide",
                principalColumn: "provide_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
