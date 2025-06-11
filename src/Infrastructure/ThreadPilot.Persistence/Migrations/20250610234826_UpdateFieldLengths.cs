using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreadPilot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registration_number",
                table: "Vehicle",
                newName: "RegistrationNumber");

            migrationBuilder.RenameIndex(
                name: "idx_vehicles_reg_num",
                table: "Vehicle",
                newName: "IX_Vehicle_RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "personal_identification_number",
                table: "persons",
                newName: "PersonalIdentificationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Vehicle",
                newName: "registration_number");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_RegistrationNumber",
                table: "Vehicle",
                newName: "idx_vehicles_reg_num");

            migrationBuilder.RenameColumn(
                name: "PersonalIdentificationNumber",
                table: "persons",
                newName: "personal_identification_number");
        }
    }
}
