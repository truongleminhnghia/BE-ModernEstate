using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "account",
                newName: "gender");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "account",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "account",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "account",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                table: "account",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "favority",
                columns: table => new
                {
                    favorite_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favority", x => x.favorite_id);
                    table.ForeignKey(
                        name: "FK_favority_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "account_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    location_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    house_number = table.Column<int>(type: "int", nullable: true),
                    street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ward = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_detail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<float>(type: "float", maxLength: 20, nullable: true),
                    longitude = table.Column<float>(type: "float", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.location_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "new",
                columns: table => new
                {
                    new_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_new", x => x.new_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    role_name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    service_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.service_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    investor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    progress = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_project_location_location_id",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "property",
                columns: table => new
                {
                    property_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Building = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<float>(type: "float", nullable: true),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: false),
                    Balcony = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Furniture = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_property", x => x.property_id);
                    table.ForeignKey(
                        name: "FK_property_location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "location",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_property_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "project_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "listing",
                columns: table => new
                {
                    listing_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    property_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
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
                name: "contact",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    listing_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_contact_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contact_listing_listing_id",
                        column: x => x.listing_id,
                        principalTable: "listing",
                        principalColumn: "listing_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    image_url = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ListingId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ProjectId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_image_listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "listing",
                        principalColumn: "listing_id");
                    table.ForeignKey(
                        name: "FK_image_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "project_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_account_account_status",
                table: "account",
                column: "account_status");

            migrationBuilder.CreateIndex(
                name: "IX_account_email",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_role_id",
                table: "account",
                column: "role_id");

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
                name: "IX_favority_AccountId",
                table: "favority",
                column: "AccountId");

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
                name: "IX_image_ListingId",
                table: "image",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_image_ProjectId",
                table: "image",
                column: "ProjectId");

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

            migrationBuilder.CreateIndex(
                name: "IX_project_location_id",
                table: "project",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_property_LocationId",
                table: "property",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_property_ProjectId",
                table: "property",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_account_role_role_id",
                table: "account",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_role_role_id",
                table: "account");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "favority");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "new");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "service");

            migrationBuilder.DropTable(
                name: "listing");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "property");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropIndex(
                name: "IX_account_account_status",
                table: "account");

            migrationBuilder.DropIndex(
                name: "IX_account_email",
                table: "account");

            migrationBuilder.DropIndex(
                name: "IX_account_role_id",
                table: "account");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "account");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "account");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "account");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "account");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "account",
                newName: "role");
        }
    }
}
