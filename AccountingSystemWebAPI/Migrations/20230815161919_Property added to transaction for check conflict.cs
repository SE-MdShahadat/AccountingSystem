using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Propertyaddedtotransactionforcheckconflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Transactions",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Transactions");
        }
    }
}
