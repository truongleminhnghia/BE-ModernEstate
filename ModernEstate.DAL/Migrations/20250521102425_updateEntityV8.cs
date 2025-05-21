using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type_transaction",
                table: "property");

            migrationBuilder.AddColumn<string>(
                name: "attribute",
                table: "project",
                type: "JSON",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "project",
                type: "varchar(500)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "price_max",
                table: "project",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price_min",
                table: "project",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "time_start",
                table: "project",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "total_investment",
                table: "project",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit_area",
                table: "project",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "unit_currency",
                table: "project",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "post_id",
                table: "history",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_history_post_id",
                table: "history",
                column: "post_id");

            migrationBuilder.AddForeignKey(
                name: "FK_history_post_post_id",
                table: "history",
                column: "post_id",
                principalTable: "post",
                principalColumn: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_history_post_post_id",
                table: "history");

            migrationBuilder.DropIndex(
                name: "IX_history_post_id",
                table: "history");

            migrationBuilder.DropColumn(
                name: "attribute",
                table: "project");

            migrationBuilder.DropColumn(
                name: "description",
                table: "project");

            migrationBuilder.DropColumn(
                name: "price_max",
                table: "project");

            migrationBuilder.DropColumn(
                name: "price_min",
                table: "project");

            migrationBuilder.DropColumn(
                name: "time_start",
                table: "project");

            migrationBuilder.DropColumn(
                name: "total_investment",
                table: "project");

            migrationBuilder.DropColumn(
                name: "unit_area",
                table: "project");

            migrationBuilder.DropColumn(
                name: "unit_currency",
                table: "project");

            migrationBuilder.DropColumn(
                name: "post_id",
                table: "history");

            migrationBuilder.AddColumn<string>(
                name: "type_transaction",
                table: "property",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
