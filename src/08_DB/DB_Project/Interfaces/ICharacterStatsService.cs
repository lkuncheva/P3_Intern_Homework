using RPGManager.Models;

namespace RPGManager.Interfaces;
public interface ICharacterStatsService
{
    Task<CharacterStats> GetCharacterStatsAsync(int characterId);
    Task<CharacterStats> CreateCharacterStatsAsync(int characterId, CharacterStats stats);
    Task<bool> UpdateCharacterStatsAsync(int characterId, CharacterStats stats);
    Task<bool> DeleteCharacterStatsAsync(int characterId);
    Task BulkInsertCharacterStatsFromJsonAsync(string jsonFilePath);
}