using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_package_subscriptions_package_package_id",
                table: "account_package_subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "priority_status",
                table: "post",
                type: "varchar(300)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "priority_status",
                table: "package",
                type: "varchar(300)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "total_day",
                table: "account_package_subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "total_amount",
                table: "account_package_subscriptions",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<DateTime>(
                name: "purchase_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<Guid>(
                name: "package_id",
                table: "account_package_subscriptions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expired_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "account_package_subscriptions",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_account_package_subscriptions_package_package_id",
                table: "account_package_subscriptions",
                column: "package_id",
                principalTable: "package",
                principalColumn: "package_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_package_subscriptions_package_package_id",
                table: "account_package_subscriptions");

            migrationBuilder.DropColumn(
                name: "priority_status",
                table: "post");

            migrationBuilder.DropColumn(
                name: "priority_status",
                table: "package");

            migrationBuilder.AlterColumn<int>(
                name: "total_day",
                table: "account_package_subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "total_amount",
                table: "account_package_subscriptions",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "purchase_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "package_id",
                table: "account_package_subscriptions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expired_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "account_package_subscriptions",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "account_package_subscriptions",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_account_package_subscriptions_package_package_id",
                table: "account_package_subscriptions",
                column: "package_id",
                principalTable: "package",
                principalColumn: "package_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
