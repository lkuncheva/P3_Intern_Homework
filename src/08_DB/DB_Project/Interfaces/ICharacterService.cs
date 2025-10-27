using RPGManager.Models;

namespace RPGManager.Services;

public interface ICharacterService
{
    Task<Character> CreateCharacterAsync(Character character);
    Task BulkInsertCharactersFromJsonAsync(string jsonFilePath);

    Task<Character> GetCharacterByIdAsync(int id);
    Task<Character> GetCharacterWithDetailsAsync(int id);
    Task<IEnumerable<Character>> GetAllCharactersAsync();
    Task<IEnumerable<Character>> GetCharactersByFilterAsync(int? minLevel = null, int? maxLevel = null, int? classId = null, bool? isActive = null);
    Task ExportCharactersToJsonAsync(string outputFilePath, int? minLevel = null, int? maxLevel = null, int? classId = null);

    Task<Character> UpdateCharacterAsync(Character character);
    Task<bool> UpdateCharacterNameAsync(int characterId, string newName);
    Task<bool> UpdateCharacterLevelAsync(int characterId, int newLevel);

    Task<bool> DeleteCharacterAsync(int characterId);

    // CharacterStats methods
    Task<CharacterStats> GetCharacterStatsAsync(int characterId);
    Task<CharacterStats> CreateCharacterStatsAsync(int characterId, CharacterStats stats);
    Task<bool> UpdateCharacterStatsAsync(int characterId, CharacterStats stats);
    Task<bool> DeleteCharacterStatsAsync(int characterId);
    Task BulkInsertCharacterStatsFromJsonAsync(string jsonFilePath);

    // CharacterQuest methods
    Task<IEnumerable<CharacterQuest>> GetCharacterQuestsAsync(int characterId);
    Task<CharacterQuest> AssignQuestToCharacterAsync(int characterId, int questId);
    Task<bool> UpdateQuestStatusAsync(int characterId, int questId, string status);
    Task<bool> RemoveQuestFromCharacterAsync(int characterId, int questId);
    Task BulkInsertCharacterQuestsFromJsonAsync(string jsonFilePath);

    // CharacterEquipment methods
    Task<IEnumerable<CharacterEquipment>> GetCharacterEquipmentAsync(int characterId);
    Task<CharacterEquipment> AssignEquipmentToCharacterAsync(int characterId, int equipmentId);
    Task<bool> ToggleEquipmentStatusAsync(int characterId, int equipmentId);
    Task<bool> RemoveEquipmentFromCharacterAsync(int characterId, int equipmentId);
    Task BulkInsertCharacterEquipmentFromJsonAsync(string jsonFilePath);
}