using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrishiLink.Migrations
{
    /// <inheritdoc />
    public partial class updatetransporttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "brokerage",
                table: "vehicleTransportData",
                newName: "Brokerage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brokerage",
                table: "vehicleTransportData",
                newName: "brokerage");
        }
    }
}
