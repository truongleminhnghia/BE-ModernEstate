using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_support_support_id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_property_project_ProjectId",
                table: "property");

            migrationBuilder.DropIndex(
                name: "IX_post_support_id",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_package_max_posts",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_package_package_code",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_package_price",
                table: "package");

            migrationBuilder.DropIndex(
                name: "IX_Account_Package_Subscription_EndDate",
                table: "account_package_subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Account_Package_Subscription_Id",
                table: "account_package_subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Account_Package_Subscription_StartDate",
                table: "account_package_subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Account_Package_Subscription_Status",
                table: "account_package_subscriptions");

            migrationBuilder.DropColumn(
                name: "number_of_bedroom",
                table: "property");

            migrationBuilder.DropColumn(
                name: "number_of_floor",
                table: "property");

            migrationBuilder.DropColumn(
                name: "original_price",
                table: "property");

            migrationBuilder.DropColumn(
                name: "price_text",
                table: "property");

            migrationBuilder.DropColumn(
                name: "sale_price",
                table: "property");

            migrationBuilder.DropColumn(
                name: "state",
                table: "property");

            migrationBuilder.DropColumn(
                name: "type_property",
                table: "property");

            migrationBuilder.DropColumn(
                name: "description",
                table: "post");

            migrationBuilder.DropColumn(
                name: "support_id",
                table: "post");

            migrationBuilder.DropColumn(
                name: "title",
                table: "post");

            migrationBuilder.DropColumn(
                name: "highlighted",
                table: "package");

            migrationBuilder.DropColumn(
                name: "max_posts",
                table: "package");

            migrationBuilder.DropColumn(
                name: "top_listing",
                table: "package");

            migrationBuilder.RenameColumn(
                name: "property_area",
                table: "property",
                newName: "area");

            migrationBuilder.RenameColumn(
                name: "number_of_toilet",
                table: "property",
                newName: "number_of_bathrooms");

            migrationBuilder.RenameColumn(
                name: "number_of_room",
                table: "property",
                newName: "number_Of_Bedrooms");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "property",
                newName: "projecy_id");

            migrationBuilder.RenameIndex(
                name: "IX_property_ProjectId",
                table: "property",
                newName: "IX_property_projecy_id");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "post",
                newName: "demand");

            migrationBuilder.RenameIndex(
                name: "IX_post_state",
                table: "post",
                newName: "IX_post_demand");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Package_Subscription_PostId",
                table: "account_package_subscriptions",
                newName: "IX_account_package_subscriptions_post_id");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Package_Subscription_PackageId",
                table: "account_package_subscriptions",
                newName: "IX_account_package_subscriptions_package_id");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Package_Subscription_AccountId",
                table: "account_package_subscriptions",
                newName: "IX_account_package_subscriptions_account_id");

            migrationBuilder.AlterColumn<string>(
                name: "transaction_code",
                table: "transaction",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "property",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "demand",
                table: "property",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "document",
                table: "property",
                type: "JSON",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "house_direction",
                table: "property",
                type: "varchar(300)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "interior",
                table: "property",
                type: "varchar(200)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "property",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "price_unit",
                table: "property",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "priority_status",
                table: "property",
                type: "varchar(300)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "status_source",
                table: "property",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "property",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "video_url",
                table: "property",
                type: "json",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "approve_by",
                table: "post",
                type: "varchar(400)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(400)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "post",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "package",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "package",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "expired_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "purchase_date",
                table: "account_package_subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_property_project_projecy_id",
                table: "property",
                column: "projecy_id",
                principalTable: "project",
                principalColumn: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_property_project_projecy_id",
                table: "property");

            migrationBuilder.DropColumn(
                name: "demand",
                table: "property");

            migrationBuilder.DropColumn(
                name: "document",
                table: "property");

            migrationBuilder.DropColumn(
                name: "house_direction",
                table: "property");

            migrationBuilder.DropColumn(
                name: "interior",
                table: "property");

            migrationBuilder.DropColumn(
                name: "price",
                table: "property");

            migrationBuilder.DropColumn(
                name: "price_unit",
                table: "property");

            migrationBuilder.DropColumn(
                name: "priority_status",
                table: "property");

            migrationBuilder.DropColumn(
                name: "status_source",
                table: "property");

            migrationBuilder.DropColumn(
                name: "type",
                table: "property");

            migrationBuilder.DropColumn(
                name: "video_url",
                table: "property");

            migrationBuilder.DropColumn(
                name: "status",
                table: "post");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "package");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "package");

            migrationBuilder.DropColumn(
                name: "expired_date",
                table: "account_package_subscriptions");

            migrationBuilder.DropColumn(
                name: "purchase_date",
                table: "account_package_subscriptions");

            migrationBuilder.RenameColumn(
                name: "projecy_id",
                table: "property",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "number_of_bathrooms",
                table: "property",
                newName: "number_of_toilet");

            migrationBuilder.RenameColumn(
                name: "number_Of_Bedrooms",
                table: "property",
                newName: "number_of_room");

            migrationBuilder.RenameColumn(
                name: "area",
                table: "property",
                newName: "property_area");

            migrationBuilder.RenameIndex(
                name: "IX_property_projecy_id",
                table: "property",
                newName: "IX_property_ProjectId");

            migrationBuilder.RenameColumn(
                name: "demand",
                table: "post",
                newName: "state");

            migrationBuilder.RenameIndex(
                name: "IX_post_demand",
                table: "post",
                newName: "IX_post_state");

            migrationBuilder.RenameIndex(
                name: "IX_account_package_subscriptions_post_id",
                table: "account_package_subscriptions",
                newName: "IX_Account_Package_Subscription_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_account_package_subscriptions_package_id",
                table: "account_package_subscriptions",
                newName: "IX_Account_Package_Subscription_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_account_package_subscriptions_account_id",
                table: "account_package_subscriptions",
                newName: "IX_Account_Package_Subscription_AccountId");

            migrationBuilder.UpdateData(
                table: "transaction",
                keyColumn: "transaction_code",
                keyValue: null,
                column: "transaction_code",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "transaction_code",
                table: "transaction",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "property",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "number_of_bedroom",
                table: "property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "number_of_floor",
                table: "property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "original_price",
                table: "property",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "price_text",
                table: "property",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "sale_price",
                table: "property",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "property",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "type_property",
                table: "property",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "post",
                keyColumn: "approve_by",
                keyValue: null,
                column: "approve_by",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "approve_by",
                table: "post",
                type: "varchar(400)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(400)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "post",
                type: "text",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "support_id",
                table: "post",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "post",
                type: "varchar(400)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "highlighted",
                table: "package",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<int>(
                name: "max_posts",
                table: "package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<ulong>(
                name: "top_listing",
                table: "package",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateIndex(
                name: "IX_post_support_id",
                table: "post",
                column: "support_id");

            migrationBuilder.CreateIndex(
                name: "IX_package_max_posts",
                table: "package",
                column: "max_posts");

            migrationBuilder.CreateIndex(
                name: "IX_package_package_code",
                table: "package",
                column: "package_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_package_price",
                table: "package",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_EndDate",
                table: "account_package_subscriptions",
                column: "end_date");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_Id",
                table: "account_package_subscriptions",
                column: "account_package_subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_StartDate",
                table: "account_package_subscriptions",
                column: "start_date");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_Status",
                table: "account_package_subscriptions",
                column: "status");

            migrationBuilder.AddForeignKey(
                name: "FK_post_support_support_id",
                table: "post",
                column: "support_id",
                principalTable: "support",
                principalColumn: "support_id");

            migrationBuilder.AddForeignKey(
                name: "FK_property_project_ProjectId",
                table: "property",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "project_id");
        }
    }
}
