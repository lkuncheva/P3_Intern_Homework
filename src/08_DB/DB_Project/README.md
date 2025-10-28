## Installation and Setup

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- SSMS 21.5.14
- Database Server: You need access to a database instance.
	Default: The project is configured to use SQL Server LocalDB ((localdb)\mssqllocaldb).
	Alternative: Any compatible SQL Server instance will work.
- EF Core Tools (CLI): Install the global tools for managing migrations:
	dotnet tool install --global dotnet-ef

### Steps to Run

1. **Clone or download the project**
   ```bash
   git clone <repository-url>
   cd DB_Project
   ```

2. **Edit appsettings.json and update the connection string:**
   For SQL Server LocalDB (default):
   ```
   {
   "ConnectionStrings": {
   "RpgDbContext": "Server=(localdb)\\mssqllocaldb;Database=DbProjectRpg;Trusted_Connection=True;MultipleActiveRes
   }
   }
   ```

   For SQL Server:
   ```
   {
   "ConnectionStrings": {
   "RpgDbContext": "Server=YOUR_SERVER_NAME;Database=DbProjectRpg;Trusted_Connection=True;MultipleActiveR
   }
   }
   ```

   For SQL Server with username/password:
   ```
   {
   "ConnectionStrings": {
   "RpgDbContext": "Server=YOUR_SERVER_NAME;Database=DbProjectRpg;User Id=YOUR_USERNAME;Password=Y
   }
   }
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Create database and apply migrations**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```

## Usage

### Main Menu Options

When you run the application, you'll see an interactive menu with the following options:

#### 1. Character Management
1. **Create Character** - Add a new character to the database
2. **Bulk Insert Characters from JSON** - Import multiple characters from file
3. **View All Characters** - List all characters with basic information
4. **View Character Details** - View a specific character with all details
5. **Update Character Name** - Modify character name
6. **Update Character Level** - Modify character level
7. **Delete Character** - Remove a character from the database
8. **Export Characters to JSON** - Export characters with filtering options

#### 2. Quest & Equipment Management
1. **Create Quest/Equipment Item** - Add a new quest/equipment item
2. **Bulk Insert Quests/Equipment from JSON** - Import multiple quests/equipment items from file
3. **View All Quests/Equipment Items** - List all quests/equipment items
4. **Update Quest Reqwards/Equipment Bonuses** - Modify quest rewards/equipment bonuses
5. **Delete Quest/Equipment Item** - Remove a quest/equipment item
6. **Export Quests/Equipment Items to JSON** - Export quests/equipment items to file

#### 3. Character Stats Management
1. **View Character Stats** - List the stats to a given character
2. **Create/Update Character Stats** - Modify given character's stats
3. **Delete Character Stats** - Remove given character's stats
4. **Bulk Insert Character Stats from JSON** - Import multiple character stats from file

#### 4. Character Quests & Character Equipment Management
1. **View Character Quests/Equipment** - List all quests/equipment items assigned to a character
2. **Assign Quest/Equipment to Character** - Link quests/equipment to characters
3. **Update Quest Status / Toggle Eqipment Status** - Modify quest progress / Equip/Uneqip equipment item
4. **Remove Quest/Equipment Item from Character** - Delete a quest/equipment item from a character
5. **Bulk Insert Character Quests/Equipment from JSON** - Import multiple character quests/equipment from file

#### 5. Data Management
1. **Seed Sample Data** - Load sample characters, classes, quests, and equipment (Skips classes if they already exist)
0. **Exit** - Close the application

### 6. Sample Data

The application includes sample data files in the `SampleData/` directory:
- `character_classes.json` - 5 pre-defined character classes
- `characters.json` - 8 sample characters with stats and equipment
- `quests.json` - 10 sample quests with varying difficulty levels
- `equipment.json` - 10 sample quests with varying difficulty levels
- `character_equipment.json` - 10 sample assignments of equipment items to different characters
- `character_quests.json` - 8 sample assignments of quests with progress status to different characters
- `character_stats.json` - 5 sample sets of stats assigned to different characters
