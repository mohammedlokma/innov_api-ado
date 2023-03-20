using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace innov_api.Migrations
{
    public partial class editverb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "Verbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "Verbs");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
