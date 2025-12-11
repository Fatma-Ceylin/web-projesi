using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageUrlFromPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Places");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Places",
                type: "TEXT",
                nullable: true);
        }
    }
}
