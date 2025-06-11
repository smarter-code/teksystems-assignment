using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThreadPilot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    symbol = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "insurance_types",
                columns: table => new
                {
                    insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_types", x => x.insurance_type_id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    person_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    personal_identification_number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.person_id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    vehicle_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    registration_number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.vehicle_id);
                });

            migrationBuilder.CreateTable(
                name: "car_insurance_types",
                columns: table => new
                {
                    car_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    base_monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_insurance_types", x => x.car_insurance_type_id);
                    table.ForeignKey(
                        name: "FK_car_insurance_types_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "health_insurance_types",
                columns: table => new
                {
                    health_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    base_monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_health_insurance_types", x => x.health_insurance_type_id);
                    table.ForeignKey(
                        name: "FK_health_insurance_types_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pet_insurance_types",
                columns: table => new
                {
                    pet_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    base_monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_insurance_types", x => x.pet_insurance_type_id);
                    table.ForeignKey(
                        name: "FK_pet_insurance_types_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "insurance_policies",
                columns: table => new
                {
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policy_number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    person_id = table.Column<long>(type: "INTEGER", nullable: false),
                    insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    start_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_policies", x => x.policy_id);
                    table.ForeignKey(
                        name: "FK_insurance_policies_insurance_types_insurance_type_id",
                        column: x => x.insurance_type_id,
                        principalTable: "insurance_types",
                        principalColumn: "insurance_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_policies_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car_insurance_details",
                columns: table => new
                {
                    car_insurance_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false),
                    vehicle_id = table.Column<long>(type: "INTEGER", nullable: false),
                    car_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_insurance_details", x => x.car_insurance_id);
                    table.ForeignKey(
                        name: "FK_car_insurance_details_Vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "Vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_car_insurance_details_car_insurance_types_car_insurance_type_id",
                        column: x => x.car_insurance_type_id,
                        principalTable: "car_insurance_types",
                        principalColumn: "car_insurance_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_car_insurance_details_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_car_insurance_details_insurance_policies_policy_id",
                        column: x => x.policy_id,
                        principalTable: "insurance_policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "health_insurance_details",
                columns: table => new
                {
                    health_insurance_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false),
                    health_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_health_insurance_details", x => x.health_insurance_id);
                    table.ForeignKey(
                        name: "FK_health_insurance_details_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_health_insurance_details_health_insurance_types_health_insurance_type_id",
                        column: x => x.health_insurance_type_id,
                        principalTable: "health_insurance_types",
                        principalColumn: "health_insurance_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_health_insurance_details_insurance_policies_policy_id",
                        column: x => x.policy_id,
                        principalTable: "insurance_policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_insurance_details",
                columns: table => new
                {
                    pet_insurance_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false),
                    pet_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_insurance_details", x => x.pet_insurance_id);
                    table.ForeignKey(
                        name: "FK_pet_insurance_details_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pet_insurance_details_insurance_policies_policy_id",
                        column: x => x.policy_id,
                        principalTable: "insurance_policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pet_insurance_details_pet_insurance_types_pet_insurance_type_id",
                        column: x => x.pet_insurance_type_id,
                        principalTable: "pet_insurance_types",
                        principalColumn: "pet_insurance_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "vehicle_id", "Color", "created_at", "Model", "registration_number", "updated_at" },
                values: new object[,]
                {
                    { 1L, "Red", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tesla Model 3", "ABC123", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, "Blue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "BMW X5", "XYZ789", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mercedes C-Class", "KLM456", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, "Black", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Audi A4", "DEF12A", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, "Silver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Volvo XC90", "GHI34B", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "currencies",
                columns: new[] { "currency_id", "code", "symbol" },
                values: new object[,]
                {
                    { 1, "USD", "$" },
                    { 2, "EUR", "€" },
                    { 3, "SEK", "kr" }
                });

            migrationBuilder.InsertData(
                table: "insurance_types",
                columns: new[] { "insurance_type_id", "type_name" },
                values: new object[,]
                {
                    { 1, "Pet" },
                    { 2, "Health" },
                    { 3, "Car" }
                });

            migrationBuilder.InsertData(
                table: "persons",
                columns: new[] { "person_id", "created_at", "personal_identification_number", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8905152384", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9211302391", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8501011234", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9512245678", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0107089012", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "car_insurance_types",
                columns: new[] { "car_insurance_type_id", "base_monthly_cost", "currency_id", "type_name" },
                values: new object[] { 1, 30m, 1, "Car Insurance" });

            migrationBuilder.InsertData(
                table: "health_insurance_types",
                columns: new[] { "health_insurance_type_id", "base_monthly_cost", "currency_id", "type_name" },
                values: new object[] { 1, 20m, 1, "Health Insurance" });

            migrationBuilder.InsertData(
                table: "insurance_policies",
                columns: new[] { "policy_id", "created_at", "end_date", "insurance_type_id", "person_id", "policy_number", "start_date", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 1L, "POL-001-PET", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 1L, "POL-001-HEALTH", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, 1L, "POL-001-CAR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 2L, "POL-002-HEALTH", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, 2L, "POL-002-CAR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 3L, "POL-003-PET", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, 4L, "POL-004-CAR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 4L, "POL-004-HEALTH", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "pet_insurance_types",
                columns: new[] { "pet_insurance_type_id", "base_monthly_cost", "currency_id", "type_name" },
                values: new object[] { 1, 10m, 1, "Pet Insurance" });

            migrationBuilder.InsertData(
                table: "car_insurance_details",
                columns: new[] { "car_insurance_id", "car_insurance_type_id", "created_at", "currency_id", "monthly_cost", "policy_id", "updated_at", "vehicle_id" },
                values: new object[,]
                {
                    { 1L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 2L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2L },
                    { 3L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 7L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4L }
                });

            migrationBuilder.InsertData(
                table: "health_insurance_details",
                columns: new[] { "health_insurance_id", "created_at", "currency_id", "health_insurance_type_id", "monthly_cost", "policy_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 20m, 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 20m, 4L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 20m, 8L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "pet_insurance_details",
                columns: new[] { "pet_insurance_id", "created_at", "currency_id", "monthly_cost", "pet_insurance_type_id", "policy_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 10m, 1, 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 10m, 1, 6L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "idx_car_details_policy",
                table: "car_insurance_details",
                column: "policy_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_insurance_details_car_insurance_type_id",
                table: "car_insurance_details",
                column: "car_insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_insurance_details_currency_id",
                table: "car_insurance_details",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_insurance_details_vehicle_id",
                table: "car_insurance_details",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_insurance_types_currency_id",
                table: "car_insurance_types",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_currencies_code",
                table: "currencies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_health_details_policy",
                table: "health_insurance_details",
                column: "policy_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_health_insurance_details_currency_id",
                table: "health_insurance_details",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_health_insurance_details_health_insurance_type_id",
                table: "health_insurance_details",
                column: "health_insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_health_insurance_types_currency_id",
                table: "health_insurance_types",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "idx_policies_person_type",
                table: "insurance_policies",
                columns: new[] { "person_id", "insurance_type_id" });

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_insurance_type_id",
                table: "insurance_policies",
                column: "insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_policy_number",
                table: "insurance_policies",
                column: "policy_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_persons_pin",
                table: "persons",
                column: "personal_identification_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pet_details_policy",
                table: "pet_insurance_details",
                column: "policy_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pet_insurance_details_currency_id",
                table: "pet_insurance_details",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_insurance_details_pet_insurance_type_id",
                table: "pet_insurance_details",
                column: "pet_insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_insurance_types_currency_id",
                table: "pet_insurance_types",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "idx_vehicles_reg_num",
                table: "Vehicle",
                column: "registration_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_insurance_details");

            migrationBuilder.DropTable(
                name: "health_insurance_details");

            migrationBuilder.DropTable(
                name: "pet_insurance_details");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "car_insurance_types");

            migrationBuilder.DropTable(
                name: "health_insurance_types");

            migrationBuilder.DropTable(
                name: "insurance_policies");

            migrationBuilder.DropTable(
                name: "pet_insurance_types");

            migrationBuilder.DropTable(
                name: "insurance_types");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "currencies");
        }
    }
}
