using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_invetor_InvetorId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_provide_provide_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_provide_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "provide_id",
                table: "project");

            migrationBuilder.RenameColumn(
                name: "InvetorId",
                table: "project",
                newName: "invetor_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_InvetorId",
                table: "project",
                newName: "IX_project_invetor_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "invetor_id",
                table: "project",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_project_invetor_invetor_id",
                table: "project",
                column: "invetor_id",
                principalTable: "invetor",
                principalColumn: "invertor_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_invetor_invetor_id",
                table: "project");

            migrationBuilder.RenameColumn(
                name: "invetor_id",
                table: "project",
                newName: "InvetorId");

            migrationBuilder.RenameIndex(
                name: "IX_project_invetor_id",
                table: "project",
                newName: "IX_project_InvetorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvetorId",
                table: "project",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "provide_id",
                table: "project",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_project_provide_id",
                table: "project",
                column: "provide_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_invetor_InvetorId",
                table: "project",
                column: "InvetorId",
                principalTable: "invetor",
                principalColumn: "invertor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_provide_provide_id",
                table: "project",
                column: "provide_id",
                principalTable: "provide",
                principalColumn: "provide_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
