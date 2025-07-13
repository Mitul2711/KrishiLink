using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrishiLink.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckVehTransportDTO",
                columns: table => new
                {
                    VehicalId = table.Column<int>(type: "int", nullable: false),
                    Number_Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Laber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Market_Shake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Final_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckVehTransportDTO");
        }
    }
}
