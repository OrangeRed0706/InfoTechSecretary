using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoTechSecretary.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Post",
                type: "varchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TranslatedDescription",
                table: "Post",
                type: "varchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Blog",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TranslatedDescription",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Blog");
        }
    }
}
