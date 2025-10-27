using RPGManager.Models;

namespace RPGManager.Interfaces;
public interface ICharacterQuestService
{
    Task<IEnumerable<CharacterQuest>> GetCharacterQuestsAsync(int characterId);
    Task<CharacterQuest> AssignQuestToCharacterAsync(int characterId, int questId);
    Task<bool> UpdateQuestStatusAsync(int characterId, int questId, string status);
    Task<bool> RemoveQuestFromCharacterAsync(int characterId, int questId);
    Task BulkInsertCharacterQuestsFromJsonAsync(string jsonFilePath);
}