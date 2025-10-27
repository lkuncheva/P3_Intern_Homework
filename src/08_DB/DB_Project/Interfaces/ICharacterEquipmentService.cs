using RPGManager.Models;

namespace RPGManager.Interfaces;
public interface ICharacterEquipmentService
{
    Task<IEnumerable<CharacterEquipment>> GetCharacterEquipmentAsync(int characterId);
    Task<CharacterEquipment> AssignEquipmentToCharacterAsync(int characterId, int equipmentId);
    Task<bool> ToggleEquipmentStatusAsync(int characterId, int equipmentId);
    Task<bool> RemoveEquipmentFromCharacterAsync(int characterId, int equipmentId);
    Task BulkInsertCharacterEquipmentFromJsonAsync(string jsonFilePath);
}