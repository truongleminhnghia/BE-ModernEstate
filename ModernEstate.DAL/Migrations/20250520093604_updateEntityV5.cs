using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OwnerProperty_Code",
                table: "owner_property");

            migrationBuilder.DropIndex(
                name: "IX_Invetor_Code",
                table: "invetor");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Code",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Broker_Code",
                table: "broker");

            migrationBuilder.CreateIndex(
                name: "IX_property_code",
                table: "property",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_Code",
                table: "owner_property",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_Code",
                table: "invetor",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Code",
                table: "employee",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Broker_Code",
                table: "broker",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_property_code",
                table: "property");

            migrationBuilder.DropIndex(
                name: "IX_OwnerProperty_Code",
                table: "owner_property");

            migrationBuilder.DropIndex(
                name: "IX_Invetor_Code",
                table: "invetor");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Code",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Broker_Code",
                table: "broker");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_Code",
                table: "owner_property",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_Code",
                table: "invetor",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Code",
                table: "employee",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_Broker_Code",
                table: "broker",
                column: "code");
        }
    }
}
