using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace innov_api.Migrations
{
    public partial class editquery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QueryString",
                table: "Verbs",
                newName: "QueryStatement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QueryStatement",
                table: "Verbs",
                newName: "QueryString");
        }
    }
}
