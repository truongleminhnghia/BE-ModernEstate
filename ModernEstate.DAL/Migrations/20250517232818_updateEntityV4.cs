using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_account_account_id",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_listing_listing_id",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_favority_account_AccountId",
                table: "favority");

            migrationBuilder.DropForeignKey(
                name: "FK_image_listing_ListingId",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_image_project_ProjectId",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_project_location_location_id",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_property_location_LocationId",
                table: "property");

            migrationBuilder.DropTable(
                name: "listing");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropIndex(
                name: "IX_image_image_id",
                table: "image");

            migrationBuilder.DropIndex(
                name: "IX_image_image_url",
                table: "image");

            migrationBuilder.DropIndex(
                name: "IX_contact_account_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_account_id_contact_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_account_id_listing_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_contact_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_listing_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_listing_id_contact_id",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_category_category_id",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_CategoryType",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favority",
                table: "favority");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "property");

            migrationBuilder.DropColumn(
                name: "Balcony",
                table: "property");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "property");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "property");

            migrationBuilder.DropColumn(
                name: "Furniture",
                table: "property");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "property");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "property");

            migrationBuilder.DropColumn(
                name: "description",
                table: "project");

            migrationBuilder.DropColumn(
                name: "investor",
                table: "project");

            migrationBuilder.DropColumn(
                name: "name",
                table: "project");

            migrationBuilder.DropColumn(
                name: "progress",
                table: "project");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "listing_id",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "message",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "category");

            migrationBuilder.RenameTable(
                name: "favority",
                newName: "favorite");

            migrationBuilder.RenameColumn(
                name: "NumberOfRooms",
                table: "property",
                newName: "number_of_toilet");

            migrationBuilder.RenameColumn(
                name: "NumberOfBathrooms",
                table: "property",
                newName: "number_of_room");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "property",
                newName: "address_id");

            migrationBuilder.RenameColumn(
                name: "Floor",
                table: "property",
                newName: "number_of_floor");

            migrationBuilder.RenameIndex(
                name: "IX_property_LocationId",
                table: "property",
                newName: "IX_property_address_id");

            migrationBuilder.RenameColumn(
                name: "location_id",
                table: "project",
                newName: "provide_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_location_id",
                table: "project",
                newName: "IX_project_provide_id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "image",
                newName: "project_id");

            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "image",
                newName: "property_id");

            migrationBuilder.RenameIndex(
                name: "IX_image_ProjectId",
                table: "image",
                newName: "IX_Image_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_image_ListingId",
                table: "image",
                newName: "IX_Image_PropertyId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "favorite",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_favority_AccountId",
                table: "favorite",
                newName: "IX_Favorite_AccountId");

            migrationBuilder.AlterTable(
                name: "project",
                comment: "Project entity representing a real estate project")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "duration_day",
                table: "service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<ulong>(
                name: "is_system",
                table: "service",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "service",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "provide_id",
                table: "service",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "staus",
                table: "service",
                type: "NVARCHAR(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "service",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "type_service",
                table: "service",
                type: "varchar(300)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "property",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "attribute",
                table: "property",
                type: "JSON",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "property",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "property",
                type: "varchar(1000)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "number_of_bedroom",
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

            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "property",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "price_text",
                table: "property",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "property_area",
                table: "property",
                type: "float",
                nullable: false,
                defaultValue: 0f);

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
                name: "status",
                table: "property",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "property",
                type: "varchar(300)",
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

            migrationBuilder.AddColumn<string>(
                name: "type_transaction",
                table: "property",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "InvetorId",
                table: "project",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "address_id",
                table: "project",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "block_name",
                table: "project",
                type: "JSON",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "project",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "project_area",
                table: "project",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "project",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "project",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "total_block",
                table: "project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_floor",
                table: "project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "type_project",
                table: "project",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "new",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "new",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "new",
                type: "text",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "new",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "publish_date",
                table: "new",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "new",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "status_new",
                table: "new",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "new",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contact_email",
                table: "contact",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contact_name",
                table: "contact",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contact_phone",
                table: "contact",
                type: "varchar(15)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "category_name",
                table: "category",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "account_id",
                table: "favorite",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "property_id",
                table: "favorite",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favorite",
                table: "favorite",
                column: "favorite_id");

            migrationBuilder.CreateTable(
                name: "account_service",
                columns: table => new
                {
                    account_service_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    subscription_term = table.Column<int>(type: "int", nullable: false),
                    unit_subscription_term = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_amount = table.Column<double>(type: "double", nullable: false),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    service_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_service", x => x.account_service_id);
                    table.ForeignKey(
                        name: "FK_account_service_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_service_service_service_id",
                        column: x => x.service_id,
                        principalTable: "service",
                        principalColumn: "service_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    house_number = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ward = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    district = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_detail = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.address_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "broker",
                columns: table => new
                {
                    broker_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_broker", x => x.broker_id);
                    table.ForeignKey(
                        name: "FK_broker_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_employee_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    history_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type_history = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    change_by = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    change_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    reason_change = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    property_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    project_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_history_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_history_property_property_id",
                        column: x => x.property_id,
                        principalTable: "property",
                        principalColumn: "property_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invetor",
                columns: table => new
                {
                    invertor_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_name = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tax_code = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    invetor_type = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invetor", x => x.invertor_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "owner_property",
                columns: table => new
                {
                    owner_property_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owner_property", x => x.owner_property_id);
                    table.ForeignKey(
                        name: "FK_owner_property_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "package",
                columns: table => new
                {
                    package_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    package_code = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    package_name = table.Column<string>(type: "varchar(400)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<double>(type: "double", nullable: false),
                    duration_days = table.Column<int>(type: "int", nullable: false),
                    max_posts = table.Column<int>(type: "int", nullable: false),
                    highlighted = table.Column<ulong>(type: "bit", nullable: false),
                    top_listing = table.Column<ulong>(type: "bit", nullable: false),
                    access_priority = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type_package = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_package", x => x.package_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "provide",
                columns: table => new
                {
                    provide_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    provide_name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provide_description = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provide_image = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provide_phone = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provide_email = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    website = table.Column<string>(type: "varchar(150)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provide", x => x.provide_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "support",
                columns: table => new
                {
                    support_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    support_name = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    support_email = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_support", x => x.support_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    tag_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tag_name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.tag_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(400)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    approve_by = table.Column<string>(type: "varchar(400)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    post_by = table.Column<string>(type: "varchar(400)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<string>(type: "varchar(300)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    source_status = table.Column<string>(type: "varchar(300)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rejection_reason = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    property_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    contact_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    support_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_post_contact_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contact",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_property_property_id",
                        column: x => x.property_id,
                        principalTable: "property",
                        principalColumn: "property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_support_support_id",
                        column: x => x.support_id,
                        principalTable: "support",
                        principalColumn: "support_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "new_tag",
                columns: table => new
                {
                    new_tag_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    new_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tag_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_new_tag", x => x.new_tag_id);
                    table.ForeignKey(
                        name: "FK_new_tag_new_new_id",
                        column: x => x.new_id,
                        principalTable: "new",
                        principalColumn: "new_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_new_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_package_subscriptions",
                columns: table => new
                {
                    account_package_subscription_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    package_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_package_subscriptions", x => x.account_package_subscription_id);
                    table.ForeignKey(
                        name: "FK_account_package_subscriptions_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_package_subscriptions_package_package_id",
                        column: x => x.package_id,
                        principalTable: "package",
                        principalColumn: "package_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_package_subscriptions_post_post_id",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    currency = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type_transaction = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment_method = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_code = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    account_service_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    post_package_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_transaction_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_account_package_subscriptions_post_package_id",
                        column: x => x.post_package_id,
                        principalTable: "account_package_subscriptions",
                        principalColumn: "account_package_subscription_id");
                    table.ForeignKey(
                        name: "FK_transaction_account_service_account_service_id",
                        column: x => x.account_service_id,
                        principalTable: "account_service",
                        principalColumn: "account_service_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Id",
                table: "service",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ProvideId",
                table: "service",
                column: "provide_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Status",
                table: "service",
                column: "staus");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Title",
                table: "service",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TypeService",
                table: "service",
                column: "type_service");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "role",
                column: "role_name");

            migrationBuilder.CreateIndex(
                name: "IX_property_AccountId",
                table: "property",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_property_owner_id",
                table: "property",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_address_id",
                table: "project",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_code",
                table: "project",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_InvetorId",
                table: "project",
                column: "InvetorId");

            migrationBuilder.CreateIndex(
                name: "IX_project_project_area",
                table: "project",
                column: "project_area");

            migrationBuilder.CreateIndex(
                name: "IX_project_status",
                table: "project",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_project_type_project",
                table: "project",
                column: "type_project");

            migrationBuilder.CreateIndex(
                name: "IX_New_AccountId",
                table: "new",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_New_CategoryId",
                table: "new",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_New_PublishDate",
                table: "new",
                column: "publish_date");

            migrationBuilder.CreateIndex(
                name: "IX_New_Slug",
                table: "new",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_New_StatusNew",
                table: "new",
                column: "status_new");

            migrationBuilder.CreateIndex(
                name: "IX_New_Title",
                table: "new",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactEmail",
                table: "contact",
                column: "contact_email");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactName",
                table: "contact",
                column: "contact_name");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactPhone",
                table: "contact",
                column: "contact_phone");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "category",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Id",
                table: "category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Id",
                table: "favorite",
                column: "favorite_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_PropertyId",
                table: "favorite",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_AccountId",
                table: "account_package_subscriptions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_EndDate",
                table: "account_package_subscriptions",
                column: "end_date");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_Id",
                table: "account_package_subscriptions",
                column: "account_package_subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_PackageId",
                table: "account_package_subscriptions",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_PostId",
                table: "account_package_subscriptions",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_StartDate",
                table: "account_package_subscriptions",
                column: "start_date");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Package_Subscription_Status",
                table: "account_package_subscriptions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_account_service_account_id",
                table: "account_service",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_service_service_id",
                table: "account_service",
                column: "service_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_broker_account_id",
                table: "broker",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Broker_AccountId",
                table: "broker",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Broker_Code",
                table: "broker",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_employee_account_id",
                table: "employee",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AccountId",
                table: "employee",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Code",
                table: "employee",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_History_ChangeBy",
                table: "history",
                column: "change_by");

            migrationBuilder.CreateIndex(
                name: "IX_History_ChangeDate",
                table: "history",
                column: "change_date");

            migrationBuilder.CreateIndex(
                name: "IX_history_project_id",
                table: "history",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_property_id",
                table: "history",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_History_ReasonChange",
                table: "history",
                column: "reason_change");

            migrationBuilder.CreateIndex(
                name: "IX_History_TypeHistory",
                table: "history",
                column: "type_history");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_Code",
                table: "invetor",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_CompanyName",
                table: "invetor",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_Email",
                table: "invetor",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_Id",
                table: "invetor",
                column: "invertor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_PhoneNumber",
                table: "invetor",
                column: "phone_number");

            migrationBuilder.CreateIndex(
                name: "IX_Invetor_TaxCode",
                table: "invetor",
                column: "tax_code");

            migrationBuilder.CreateIndex(
                name: "IX_NewTag_Id",
                table: "new_tag",
                column: "new_tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_NewTag_NewId",
                table: "new_tag",
                column: "new_id");

            migrationBuilder.CreateIndex(
                name: "IX_NewTag_TagId",
                table: "new_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_owner_property_account_id",
                table: "owner_property",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_AccountId",
                table: "owner_property",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_Code",
                table: "owner_property",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_Id",
                table: "owner_property",
                column: "owner_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_package_access_priority",
                table: "package",
                column: "access_priority");

            migrationBuilder.CreateIndex(
                name: "IX_package_highlighted",
                table: "package",
                column: "highlighted");

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
                name: "IX_package_top_listing",
                table: "package",
                column: "top_listing");

            migrationBuilder.CreateIndex(
                name: "IX_package_type_package",
                table: "package",
                column: "type_package");

            migrationBuilder.CreateIndex(
                name: "IX_post_code",
                table: "post",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_contact_id",
                table: "post",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_property_id",
                table: "post",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_source_status",
                table: "post",
                column: "source_status");

            migrationBuilder.CreateIndex(
                name: "IX_post_state",
                table: "post",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX_post_support_id",
                table: "post",
                column: "support_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_account_id",
                table: "transaction",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_account_service_id",
                table: "transaction",
                column: "account_service_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_payment_method",
                table: "transaction",
                column: "payment_method");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_post_package_id",
                table: "transaction",
                column: "post_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_status",
                table: "transaction",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_transaction_code",
                table: "transaction",
                column: "transaction_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_type_transaction",
                table: "transaction",
                column: "type_transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_favorite_account_account_id",
                table: "favorite",
                column: "account_id",
                principalTable: "account",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favorite_property_property_id",
                table: "favorite",
                column: "property_id",
                principalTable: "property",
                principalColumn: "property_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_image_project_project_id",
                table: "image",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_image_property_property_id",
                table: "image",
                column: "property_id",
                principalTable: "property",
                principalColumn: "property_id");

            migrationBuilder.AddForeignKey(
                name: "FK_new_account_account_id",
                table: "new",
                column: "account_id",
                principalTable: "account",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_new_category_category_id",
                table: "new",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_address_address_id",
                table: "project",
                column: "address_id",
                principalTable: "address",
                principalColumn: "address_id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_property_account_AccountId",
                table: "property",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_property_address_address_id",
                table: "property",
                column: "address_id",
                principalTable: "address",
                principalColumn: "address_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_property_owner_property_owner_id",
                table: "property",
                column: "owner_id",
                principalTable: "owner_property",
                principalColumn: "owner_property_id");

            migrationBuilder.AddForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service",
                column: "provide_id",
                principalTable: "provide",
                principalColumn: "provide_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favorite_account_account_id",
                table: "favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_favorite_property_property_id",
                table: "favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_image_project_project_id",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_image_property_property_id",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_new_account_account_id",
                table: "new");

            migrationBuilder.DropForeignKey(
                name: "FK_new_category_category_id",
                table: "new");

            migrationBuilder.DropForeignKey(
                name: "FK_project_address_address_id",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_invetor_InvetorId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_provide_provide_id",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_property_account_AccountId",
                table: "property");

            migrationBuilder.DropForeignKey(
                name: "FK_property_address_address_id",
                table: "property");

            migrationBuilder.DropForeignKey(
                name: "FK_property_owner_property_owner_id",
                table: "property");

            migrationBuilder.DropForeignKey(
                name: "FK_service_provide_provide_id",
                table: "service");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "broker");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "invetor");

            migrationBuilder.DropTable(
                name: "new_tag");

            migrationBuilder.DropTable(
                name: "owner_property");

            migrationBuilder.DropTable(
                name: "provide");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "account_package_subscriptions");

            migrationBuilder.DropTable(
                name: "account_service");

            migrationBuilder.DropTable(
                name: "package");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "support");

            migrationBuilder.DropIndex(
                name: "IX_Service_Id",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ProvideId",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_Service_Status",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_Service_Title",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TypeService",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_Role_RoleName",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_property_AccountId",
                table: "property");

            migrationBuilder.DropIndex(
                name: "IX_property_owner_id",
                table: "property");

            migrationBuilder.DropIndex(
                name: "IX_project_address_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_code",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_InvetorId",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_project_area",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_status",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_type_project",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_New_AccountId",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_New_CategoryId",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_New_PublishDate",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_New_Slug",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_New_StatusNew",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_New_Title",
                table: "new");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactEmail",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactName",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactPhone",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_Category_Id",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favorite",
                table: "favorite");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_Id",
                table: "favorite");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_PropertyId",
                table: "favorite");

            migrationBuilder.DropColumn(
                name: "duration_day",
                table: "service");

            migrationBuilder.DropColumn(
                name: "is_system",
                table: "service");

            migrationBuilder.DropColumn(
                name: "price",
                table: "service");

            migrationBuilder.DropColumn(
                name: "provide_id",
                table: "service");

            migrationBuilder.DropColumn(
                name: "staus",
                table: "service");

            migrationBuilder.DropColumn(
                name: "title",
                table: "service");

            migrationBuilder.DropColumn(
                name: "type_service",
                table: "service");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "property");

            migrationBuilder.DropColumn(
                name: "attribute",
                table: "property");

            migrationBuilder.DropColumn(
                name: "code",
                table: "property");

            migrationBuilder.DropColumn(
                name: "description",
                table: "property");

            migrationBuilder.DropColumn(
                name: "number_of_bedroom",
                table: "property");

            migrationBuilder.DropColumn(
                name: "original_price",
                table: "property");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "property");

            migrationBuilder.DropColumn(
                name: "price_text",
                table: "property");

            migrationBuilder.DropColumn(
                name: "property_area",
                table: "property");

            migrationBuilder.DropColumn(
                name: "sale_price",
                table: "property");

            migrationBuilder.DropColumn(
                name: "state",
                table: "property");

            migrationBuilder.DropColumn(
                name: "status",
                table: "property");

            migrationBuilder.DropColumn(
                name: "title",
                table: "property");

            migrationBuilder.DropColumn(
                name: "type_property",
                table: "property");

            migrationBuilder.DropColumn(
                name: "type_transaction",
                table: "property");

            migrationBuilder.DropColumn(
                name: "InvetorId",
                table: "project");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "block_name",
                table: "project");

            migrationBuilder.DropColumn(
                name: "code",
                table: "project");

            migrationBuilder.DropColumn(
                name: "project_area",
                table: "project");

            migrationBuilder.DropColumn(
                name: "status",
                table: "project");

            migrationBuilder.DropColumn(
                name: "title",
                table: "project");

            migrationBuilder.DropColumn(
                name: "total_block",
                table: "project");

            migrationBuilder.DropColumn(
                name: "total_floor",
                table: "project");

            migrationBuilder.DropColumn(
                name: "type_project",
                table: "project");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "new");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "new");

            migrationBuilder.DropColumn(
                name: "content",
                table: "new");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "new");

            migrationBuilder.DropColumn(
                name: "publish_date",
                table: "new");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "new");

            migrationBuilder.DropColumn(
                name: "status_new",
                table: "new");

            migrationBuilder.DropColumn(
                name: "title",
                table: "new");

            migrationBuilder.DropColumn(
                name: "contact_email",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "contact_name",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "contact_phone",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "category_name",
                table: "category");

            migrationBuilder.DropColumn(
                name: "property_id",
                table: "favorite");

            migrationBuilder.RenameTable(
                name: "favorite",
                newName: "favority");

            migrationBuilder.RenameColumn(
                name: "number_of_toilet",
                table: "property",
                newName: "NumberOfRooms");

            migrationBuilder.RenameColumn(
                name: "number_of_room",
                table: "property",
                newName: "NumberOfBathrooms");

            migrationBuilder.RenameColumn(
                name: "number_of_floor",
                table: "property",
                newName: "Floor");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "property",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_property_address_id",
                table: "property",
                newName: "IX_property_LocationId");

            migrationBuilder.RenameColumn(
                name: "provide_id",
                table: "project",
                newName: "location_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_provide_id",
                table: "project",
                newName: "IX_project_location_id");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "image",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "image",
                newName: "ListingId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_ProjectId",
                table: "image",
                newName: "IX_image_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_PropertyId",
                table: "image",
                newName: "IX_image_ListingId");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "favority",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_AccountId",
                table: "favority",
                newName: "IX_favority_AccountId");

            migrationBuilder.AlterTable(
                name: "project",
                oldComment: "Project entity representing a real estate project")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "Area",
                table: "property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Balcony",
                table: "property",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "property",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "property",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Furniture",
                table: "property",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "property",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "property",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "project",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "investor",
                table: "project",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "project",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "progress",
                table: "project",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "contact",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "contact",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "contact",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "listing_id",
                table: "contact",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "contact",
                type: "varchar(500)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "contact",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "favority",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favority",
                table: "favority",
                column: "favorite_id");

            migrationBuilder.CreateTable(
                name: "listing",
                columns: table => new
                {
                    listing_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    property_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listing", x => x.listing_id);
                    table.ForeignKey(
                        name: "FK_listing_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listing_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listing_property_property_id",
                        column: x => x.property_id,
                        principalTable: "property",
                        principalColumn: "property_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    location_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    address_detail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    house_number = table.Column<int>(type: "int", nullable: true),
                    latitude = table.Column<float>(type: "float", maxLength: 20, nullable: true),
                    longitude = table.Column<float>(type: "float", maxLength: 20, nullable: true),
                    street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ward = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.location_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_image_image_id",
                table: "image",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_image_image_url",
                table: "image",
                column: "image_url");

            migrationBuilder.CreateIndex(
                name: "IX_contact_account_id",
                table: "contact",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_account_id_contact_id",
                table: "contact",
                columns: new[] { "account_id", "contact_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_account_id_listing_id",
                table: "contact",
                columns: new[] { "account_id", "listing_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_contact_id",
                table: "contact",
                column: "contact_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_listing_id",
                table: "contact",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_listing_id_contact_id",
                table: "contact",
                columns: new[] { "listing_id", "contact_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_category_id",
                table: "category",
                column: "category_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_CategoryType",
                table: "category",
                column: "CategoryType");

            migrationBuilder.CreateIndex(
                name: "IX_listing_account_id",
                table: "listing",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_listing_category_id",
                table: "listing",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_listing_description",
                table: "listing",
                column: "description");

            migrationBuilder.CreateIndex(
                name: "IX_listing_property_id",
                table: "listing",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_listing_status",
                table: "listing",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_listing_title",
                table: "listing",
                column: "title");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_account_account_id",
                table: "contact",
                column: "account_id",
                principalTable: "account",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_listing_listing_id",
                table: "contact",
                column: "listing_id",
                principalTable: "listing",
                principalColumn: "listing_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favority_account_AccountId",
                table: "favority",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_image_listing_ListingId",
                table: "image",
                column: "ListingId",
                principalTable: "listing",
                principalColumn: "listing_id");

            migrationBuilder.AddForeignKey(
                name: "FK_image_project_ProjectId",
                table: "image",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_location_location_id",
                table: "project",
                column: "location_id",
                principalTable: "location",
                principalColumn: "location_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_property_location_LocationId",
                table: "property",
                column: "LocationId",
                principalTable: "location",
                principalColumn: "location_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
