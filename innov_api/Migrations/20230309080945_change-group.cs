using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace innov_api.Migrations
{
    public partial class changegroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Verbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QueryString",
                table: "Verbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionString",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DbType",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Verbs");

            migrationBuilder.DropColumn(
                name: "QueryString",
                table: "Verbs");

            migrationBuilder.DropColumn(
                name: "ConnectionString",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DbType",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "Groups");
        }
    }
}
