using RPGManager.Models;

namespace RPGManager.Services;

public interface IQuestService
{
    Task<Quest> CreateQuestAsync(Quest quest);
    Task BulkInsertQuestsFromJsonAsync(string jsonFilePath);

    Task<Quest> GetQuestByIdAsync(int id);
    Task<IEnumerable<Quest>> GetAllQuestsAsync();
    Task<IEnumerable<Quest>> GetQuestsByDifficultyAsync(string difficulty);
    Task ExportQuestsToJsonAsync(string outputFilePath, string difficulty = null);

    Task<Quest> UpdateQuestAsync(Quest quest);
    Task<bool> UpdateQuestRewardsAsync(int questId, int newGold, int newExperience);

    Task<bool> DeleteQuestAsync(int questId);

    Task<bool> AssignQuestToCharacterAsync(int characterId, int questId);
    Task<bool> UpdateQuestStatusAsync(int characterId, int questId, string status);
    Task<IEnumerable<Quest>> GetCharacterQuestsAsync(int characterId);
}