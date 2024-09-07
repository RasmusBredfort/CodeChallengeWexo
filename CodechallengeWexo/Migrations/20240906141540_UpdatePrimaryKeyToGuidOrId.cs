using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodechallengeWexo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKeyToGuidOrId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Movies",
                newName: "Guid");

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Series",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Series");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Movies",
                newName: "Author");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");
        }
    }
}
