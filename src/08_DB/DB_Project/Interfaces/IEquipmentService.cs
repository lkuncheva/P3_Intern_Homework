using RPGManager.Models;

namespace RPGManager.Interfaces;

public interface IEquipmentService
{
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    Task BulkInsertEquipmentFromJsonAsync(string jsonFilePath);

    Task<Equipment> GetEquipmentByIdAsync(int id);
    Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
    Task<IEnumerable<Equipment>> GetEquipmentByRarityAsync(string rarity);
    Task ExportEquipmentToJsonAsync(string outputFilePath, string rarityFilter = null);

    Task<Equipment> UpdateEquipmentAsync(Equipment equipment);
    Task<bool> UpdateEquipmentBonusesAsync(int id, int attackBonus, int defenseBonus);

    Task<bool> DeleteEquipmentAsync(int equipmentId);

    Task<bool> AssignEquipmentToCharacterAsync(int characterId, int equipmentId);
    Task<bool?> ToggleEquipmentStatusAsync(int characterId, int equipmentId);
    Task<IEnumerable<Equipment>> GetCharacterEquipmentAsync(int characterId);
}