namespace RPGManager.Interfaces;

public interface IDataSeederService
{
    Task SeedCharacterClassesAsync();
    Task SeedAllSampleDataAsync();
}