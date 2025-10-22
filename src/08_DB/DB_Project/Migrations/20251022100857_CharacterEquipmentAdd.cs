using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Project.Migrations
{
    /// <inheritdoc />
    public partial class CharacterEquipmentAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Characters_CharacterId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_CharacterId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CharacterId",
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

            migrationBuilder.CreateTable(
                name: "CharacterEquipment",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    IsEquipped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEquipment", x => new { x.CharacterId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_CharacterEquipment_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipment_EquipmentId",
                table: "CharacterEquipment",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEquipment");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Equipment_CharacterId",
                table: "Equipment",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Characters_CharacterId",
                table: "Equipment",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
