using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApi.Migrations
{
    /// <inheritdoc />
    public partial class upd_ban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Bans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bans_UpdatedById",
                table: "Bans",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bans_Accounts_UpdatedById",
                table: "Bans",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bans_Accounts_UpdatedById",
                table: "Bans");

            migrationBuilder.DropIndex(
                name: "IX_Bans_UpdatedById",
                table: "Bans");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Bans");
        }
    }
}
