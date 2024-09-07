using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodechallengeWexo.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailUrlToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Movies");
        }
    }
}
