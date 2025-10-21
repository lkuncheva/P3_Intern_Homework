using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Project.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CharacterClasses",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                BaseHealth = table.Column<int>(type: "int", nullable: false),
                BaseMana = table.Column<int>(type: "int", nullable: false),
                PrimaryAttribute = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CharacterClasses", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Quests",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                RewardGold = table.Column<int>(type: "int", nullable: false),
                RewardExperience = table.Column<int>(type: "int", nullable: false),
                RequiredLevel = table.Column<int>(type: "int", nullable: false),
                Difficulty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quests", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Characters",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Level = table.Column<int>(type: "int", nullable: false),
                Experience = table.Column<int>(type: "int", nullable: false),
                Gold = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CharacterClassId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Characters", x => x.Id);
                table.ForeignKey(
                    name: "FK_Characters_CharacterClasses_CharacterClassId",
                    column: x => x.CharacterClassId,
                    principalTable: "CharacterClasses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "CharacterQuests",
            columns: table => new
            {
                CharacterId = table.Column<int>(type: "int", nullable: false),
                QuestId = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CharacterQuests", x => new { x.CharacterId, x.QuestId });
                table.ForeignKey(
                    name: "FK_CharacterQuests_Characters_CharacterId",
                    column: x => x.CharacterId,
                    principalTable: "Characters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CharacterQuests_Quests_QuestId",
                    column: x => x.QuestId,
                    principalTable: "Quests",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CharacterStats",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CharacterId = table.Column<int>(type: "int", nullable: false),
                Strength = table.Column<int>(type: "int", nullable: false),
                Dexterity = table.Column<int>(type: "int", nullable: false),
                Intelligence = table.Column<int>(type: "int", nullable: false),
                Constitution = table.Column<int>(type: "int", nullable: false),
                Wisdom = table.Column<int>(type: "int", nullable: false),
                Charisma = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CharacterStats", x => x.Id);
                table.ForeignKey(
                    name: "FK_CharacterStats_Characters_CharacterId",
                    column: x => x.CharacterId,
                    principalTable: "Characters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Equipment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                Rarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AttackBonus = table.Column<int>(type: "int", nullable: false),
                DefenseBonus = table.Column<int>(type: "int", nullable: false),
                CharacterId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Equipment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Equipment_Characters_CharacterId",
                    column: x => x.CharacterId,
                    principalTable: "Characters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CharacterClasses_Name",
            table: "CharacterClasses",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CharacterQuests_QuestId",
            table: "CharacterQuests",
            column: "QuestId");

        migrationBuilder.CreateIndex(
            name: "IX_Characters_CharacterClassId",
            table: "Characters",
            column: "CharacterClassId");

        migrationBuilder.CreateIndex(
            name: "IX_Characters_Level",
            table: "Characters",
            column: "Level");

        migrationBuilder.CreateIndex(
            name: "IX_Characters_Name",
            table: "Characters",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_CharacterStats_CharacterId",
            table: "CharacterStats",
            column: "CharacterId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Equipment_CharacterId",
            table: "Equipment",
            column: "CharacterId");

        migrationBuilder.CreateIndex(
            name: "IX_Quests_Difficulty",
            table: "Quests",
            column: "Difficulty");

        migrationBuilder.CreateIndex(
            name: "IX_Quests_Title",
            table: "Quests",
            column: "Title");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CharacterQuests");

        migrationBuilder.DropTable(
            name: "CharacterStats");

        migrationBuilder.DropTable(
            name: "Equipment");

        migrationBuilder.DropTable(
            name: "Quests");

        migrationBuilder.DropTable(
            name: "Characters");

        migrationBuilder.DropTable(
            name: "CharacterClasses");
    }
}
