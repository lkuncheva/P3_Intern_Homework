using RPGManager.Models;

namespace RPGManager.Interfaces;

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
}