## Installation and Setup

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code

### Steps to Run

1. **Clone or download the project**
   ```bash
   git clone <repository-url>
   cd DB_Project
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Create database and apply migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

## Usage

### Main Menu Options

When you run the application, you'll see an interactive menu with the following options:

#### 1. Character Management
1. **Create Character** - Add a new character to the database
2. **View Character** - View a specific character with all details
3. **View All Characters** - List all characters with basic information
4. **Update Character** - Modify character name or level
5. **Delete Character** - Remove a character from the database

#### 2. Quest Management
6. **Create Quest** - Add a new quest
7. **View Quest** - View a specific quest with details
8. **View All Quests** - List all quests
9. **Update Quest** - Modify quest rewards or status
10. **Delete Quest** - Remove a quest

#### 3. Advanced Operations
11. **Assign Quest to Character** - Link quests to characters
12. **View Character's Quests** - Show all quests assigned to a character
13. **Bulk Import Characters from JSON** - Import multiple characters from file
14. **Export Characters to JSON** - Export characters with filtering options

#### 4. Data Management
15. **Seed Sample Data** - Load sample characters, classes, and quests
16. **View Database Statistics** - Show counts of all entities
0. **Exit** - Close the application

### 5. Sample Data

The application includes sample data files in the `SampleData/` directory:
- `character_classes.json` - 5 pre-defined character classes
- `characters.json` - 8 sample characters with stats and equipment
- `quests.json` - 10 sample quests with varying difficulty levels

To load sample data, run the application and select option 15.