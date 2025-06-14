using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_property_project_projecy_id",
                table: "property");

            migrationBuilder.DropIndex(
                name: "IX_post_demand",
                table: "post");

            migrationBuilder.DropColumn(
                name: "package_name",
                table: "property");

            migrationBuilder.RenameColumn(
                name: "projecy_id",
                table: "property",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_property_projecy_id",
                table: "property",
                newName: "IX_property_project_id");

            migrationBuilder.AddColumn<string>(
                name: "area_unit",
                table: "property",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_property_project_project_id",
                table: "property",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_property_project_project_id",
                table: "property");

            migrationBuilder.DropColumn(
                name: "area_unit",
                table: "property");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "property",
                newName: "projecy_id");

            migrationBuilder.RenameIndex(
                name: "IX_property_project_id",
                table: "property",
                newName: "IX_property_projecy_id");

            migrationBuilder.AddColumn<string>(
                name: "package_name",
                table: "property",
                type: "VARCHAR(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_post_demand",
                table: "post",
                column: "demand");

            migrationBuilder.AddForeignKey(
                name: "FK_property_project_projecy_id",
                table: "property",
                column: "projecy_id",
                principalTable: "project",
                principalColumn: "project_id");
        }
    }
}
