using Newtonsoft.Json;
using RPGManager.Models;
using RPGManager.Interfaces;

namespace RPGManager.Services;

public class QuestService : IQuestService
{
    private readonly IRepository<Quest> _questRepository;
    private readonly IRepository<CharacterQuest> _characterQuestRepository;
    private readonly ICharacterRepository _characterRepository;

    public QuestService(
        IRepository<Quest> questRepository,
        IRepository<CharacterQuest> characterQuestRepository,
        ICharacterRepository characterRepository)
    {
        _questRepository = questRepository ?? throw new ArgumentNullException(nameof(questRepository));
        _characterQuestRepository = characterQuestRepository ?? throw new ArgumentNullException(nameof(characterQuestRepository));
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
    }

    public async Task<Quest> CreateQuestAsync(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        if (string.IsNullOrWhiteSpace(quest.Title))
            throw new ArgumentException("Quest title cannot be empty.", nameof(quest));

        await _questRepository.AddAsync(quest);
        return quest;
    }

    public async Task BulkInsertQuestsFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var quests = JsonConvert.DeserializeObject<List<Quest>>(jsonContent);

        if (quests == null || !quests.Any())
            throw new InvalidOperationException("No quests found in JSON file.");

        await _questRepository.AddRangeAsync(quests);
        Console.WriteLine($"Successfully inserted {quests.Count} quests from {jsonFilePath}");
    }

    public async Task<Quest> GetQuestByIdAsync(int id)
    {
        return await _questRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Quest>> GetAllQuestsAsync()
    {
        return await _questRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Quest>> GetQuestsByDifficultyAsync(string difficulty)
    {
        return await _questRepository.FindAsync(q => q.Difficulty == difficulty);
    }

    public async Task ExportQuestsToJsonAsync(string outputFilePath, string difficulty = null)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path cannot be empty.", nameof(outputFilePath));

        IEnumerable<Quest> quests;
        if (!string.IsNullOrWhiteSpace(difficulty))
            quests = await GetQuestsByDifficultyAsync(difficulty);
        else
            quests = await GetAllQuestsAsync();

        var jsonContent = JsonConvert.SerializeObject(quests, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await File.WriteAllTextAsync(outputFilePath, jsonContent);
        Console.WriteLine($"Successfully exported {quests.Count()} quests to {outputFilePath}");
    }

    public async Task<Quest> UpdateQuestAsync(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        var existingQuest = await _questRepository.GetByIdAsync(quest.Id);
        if (existingQuest == null)
            throw new InvalidOperationException($"Quest with ID {quest.Id} not found.");

        await _questRepository.UpdateAsync(quest);
        return quest;
    }

    public async Task<bool> UpdateQuestRewardsAsync(int questId, int newGold, int newExperience)
    {
        var quest = await _questRepository.GetByIdAsync(questId);
        if (quest == null)
            return false;

        quest.RewardGold = newGold;
        quest.RewardExperience = newExperience;
        await _questRepository.UpdateAsync(quest);
        return true;
    }

    public async Task<bool> DeleteQuestAsync(int questId)
    {
        var quest = await _questRepository.GetByIdAsync(questId);
        if (quest == null)
            return false;

        await _questRepository.DeleteAsync(quest);
        return true;
    }

    public async Task<bool> AssignQuestToCharacterAsync(int characterId, int questId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        var quest = await _questRepository.GetByIdAsync(questId);

        if (character == null || quest == null)
            return false;

        var existingAssignment = await _characterQuestRepository.FindAsync(
            cq => cq.CharacterId == characterId && cq.QuestId == questId);

        if (existingAssignment.Any())
            return false;

        var characterQuest = new CharacterQuest
        {
            CharacterId = characterId,
            QuestId = questId,
            Status = "NotStarted",
            StartedDate = DateTime.UtcNow
        };

        await _characterQuestRepository.AddAsync(characterQuest);
        return true;
    }

    public async Task<bool> UpdateQuestStatusAsync(int characterId, int questId, string status)
    {
        var characterQuests = await _characterQuestRepository.FindAsync(
            cq => cq.CharacterId == characterId && cq.QuestId == questId);

        var characterQuest = characterQuests.FirstOrDefault();
        if (characterQuest == null)
            return false;

        characterQuest.Status = status;
        if (status == "Completed")
            characterQuest.CompletedDate = DateTime.UtcNow;

        await _characterQuestRepository.UpdateAsync(characterQuest);
        return true;
    }

    public async Task<IEnumerable<Quest>> GetCharacterQuestsAsync(int characterId)
    {
        var characterQuests = await _characterQuestRepository.FindAsync(cq => cq.CharacterId == characterId);
        var questIds = characterQuests.Select(cq => cq.QuestId).ToList();

        var allQuests = await _questRepository.GetAllAsync();
        return allQuests.Where(q => questIds.Contains(q.Id));
    }
}