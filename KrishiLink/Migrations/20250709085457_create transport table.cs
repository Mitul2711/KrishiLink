using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrishiLink.Migrations
{
    /// <inheritdoc />
    public partial class createtransporttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicleTransportData",
                columns: table => new
                {
                    VehicalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number_Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Laber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Market_Shake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Final_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicleTransportData", x => x.VehicalId);
                });

            migrationBuilder.CreateTable(
                name: "TransferDetail",
                columns: table => new
                {
                    TransferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicalId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Count_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferDetail", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK_TransferDetail_vehicleTransportData_VehicalId",
                        column: x => x.VehicalId,
                        principalTable: "vehicleTransportData",
                        principalColumn: "VehicalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferDetail_VehicalId",
                table: "TransferDetail",
                column: "VehicalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferDetail");

            migrationBuilder.DropTable(
                name: "vehicleTransportData");
        }
    }
}
