using RPGManager.Models;

namespace RPGManager.Interfaces;

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
}