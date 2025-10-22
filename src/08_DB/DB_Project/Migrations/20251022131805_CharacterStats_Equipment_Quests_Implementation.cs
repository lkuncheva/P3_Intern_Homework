using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Project.Migrations
{
    /// <inheritdoc />
    public partial class CharacterStats_Equipment_Quests_Implementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedDate",
                table: "CharacterQuests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "CharacterQuests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Rarity",
                table: "Equipment",
                column: "Rarity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipment_Name",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_Rarity",
                table: "Equipment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedDate",
                table: "CharacterQuests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "CharacterQuests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
