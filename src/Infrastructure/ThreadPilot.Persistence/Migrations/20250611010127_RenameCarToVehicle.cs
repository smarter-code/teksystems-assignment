using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThreadPilot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameCarToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_insurance_details");

            migrationBuilder.DropTable(
                name: "car_insurance_types");

            migrationBuilder.CreateTable(
                name: "vehicle_insurance_types",
                columns: table => new
                {
                    vehicle_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    base_monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_insurance_types", x => x.vehicle_insurance_type_id);
                    table.ForeignKey(
                        name: "FK_vehicle_insurance_types_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_insurance_details",
                columns: table => new
                {
                    vehicle_insurance_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false),
                    vehicle_id = table.Column<long>(type: "INTEGER", nullable: false),
                    vehicle_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_insurance_details", x => x.vehicle_insurance_id);
                    table.ForeignKey(
                        name: "FK_vehicle_insurance_details_Vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "Vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_insurance_details_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_insurance_details_insurance_policies_policy_id",
                        column: x => x.policy_id,
                        principalTable: "insurance_policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_insurance_details_vehicle_insurance_types_vehicle_insurance_type_id",
                        column: x => x.vehicle_insurance_type_id,
                        principalTable: "vehicle_insurance_types",
                        principalColumn: "vehicle_insurance_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 3L,
                column: "policy_number",
                value: "POL-001-VEH");

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 5L,
                column: "policy_number",
                value: "POL-002-VEH");

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 7L,
                column: "policy_number",
                value: "POL-004-VEH");

            migrationBuilder.UpdateData(
                table: "insurance_types",
                keyColumn: "insurance_type_id",
                keyValue: 3,
                column: "type_name",
                value: "Vehicle");

            migrationBuilder.InsertData(
                table: "vehicle_insurance_types",
                columns: new[] { "vehicle_insurance_type_id", "base_monthly_cost", "currency_id", "type_name" },
                values: new object[] { 1, 30m, 1, "Vehicle Insurance" });

            migrationBuilder.InsertData(
                table: "vehicle_insurance_details",
                columns: new[] { "vehicle_insurance_id", "created_at", "currency_id", "monthly_cost", "policy_id", "updated_at", "vehicle_id", "vehicle_insurance_type_id" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L, 1 },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2L, 1 },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 7L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4L, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "idx_vehicle_details_policy",
                table: "vehicle_insurance_details",
                column: "policy_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurance_details_currency_id",
                table: "vehicle_insurance_details",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurance_details_vehicle_id",
                table: "vehicle_insurance_details",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurance_details_vehicle_insurance_type_id",
                table: "vehicle_insurance_details",
                column: "vehicle_insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurance_types_currency_id",
                table: "vehicle_insurance_types",
                column: "currency_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle_insurance_details");

            migrationBuilder.DropTable(
                name: "vehicle_insurance_types");

            migrationBuilder.CreateTable(
                name: "car_insurance_types",
                columns: table => new
                {
                    car_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    base_monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    type_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
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
                name: "car_insurance_details",
                columns: table => new
                {
                    car_insurance_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    car_insurance_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    currency_id = table.Column<int>(type: "INTEGER", nullable: false),
                    policy_id = table.Column<long>(type: "INTEGER", nullable: false),
                    vehicle_id = table.Column<long>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    monthly_cost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
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

            migrationBuilder.InsertData(
                table: "car_insurance_types",
                columns: new[] { "car_insurance_type_id", "base_monthly_cost", "currency_id", "type_name" },
                values: new object[] { 1, 30m, 1, "Car Insurance" });

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 3L,
                column: "policy_number",
                value: "POL-001-CAR");

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 5L,
                column: "policy_number",
                value: "POL-002-CAR");

            migrationBuilder.UpdateData(
                table: "insurance_policies",
                keyColumn: "policy_id",
                keyValue: 7L,
                column: "policy_number",
                value: "POL-004-CAR");

            migrationBuilder.UpdateData(
                table: "insurance_types",
                keyColumn: "insurance_type_id",
                keyValue: 3,
                column: "type_name",
                value: "Car");

            migrationBuilder.InsertData(
                table: "car_insurance_details",
                columns: new[] { "car_insurance_id", "car_insurance_type_id", "created_at", "currency_id", "monthly_cost", "policy_id", "updated_at", "vehicle_id" },
                values: new object[,]
                {
                    { 1L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 2L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2L },
                    { 3L, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 30m, 7L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4L }
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
        }
    }
}
