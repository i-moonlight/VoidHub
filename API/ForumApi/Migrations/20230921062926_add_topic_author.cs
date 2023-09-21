using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApi.Migrations
{
    /// <inheritdoc />
    public partial class add_topic_author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Topics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AccountId",
                table: "Topics",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Accounts_AccountId",
                table: "Topics",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Accounts_AccountId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_AccountId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Topics");
        }
    }
}
