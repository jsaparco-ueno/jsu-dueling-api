### Dueling API
Technical Assessment for Neo Financial | Senior Backend Developer | Justin Saparco-Ueno
***

### Getting Started
- To launch and run the React frontend and C# API, open the root folder in Visual Studio Code and press F5.
- Alternatively:
  - Run the frontend in a terminal. cd to app/ClientApp and run `npm start`.
  - Run the backend in a terminal. cd to app/ and run `dotnet start`.

- To run tests, in a terminal cd to /tests and run `dotnet test`.

- Send requests to the API using curl statements from the terminal:

- Get By Id. (On startup, the app is preseeded with 7 characters with Ids 0 through 6)
  - `curl https://localhost:5001/character/get/0`
- Get All 
  - `curl https://localhost:5001/character/get/`
- Create 
  - `curl -v -d '&#123;\"Name\":\"Jones\", \"Job\":\"Warrior\"&#125;' -H "Content-Type: application/json" https://localhost:5001/character/create`
- Battle
  - `curl -v -d '{\"CharacterOneId\":\"0\", \"CharacterTwoId\":\"1\"}' -H "Content-Type: application/json" https://localhost:5001/battle`

### Outline
- This API will have the following parts:
  - app: contains the frontend and backend
    - ClientApp: contains the frontend. This has been left to do later.
    - Models: contains class files each representing hierarchical objects in the namespace and their properties
      - Character: has a name
        - Job: has a name, HP, strength, dexterity, intelligence, AttackModifier and SpeedModifier
          - Warrior: determines HP, strength, dexterity, intelligence; implements AttackModifier and SpeedModifier
          - Thief, ...
          - Mage, ...
    - Services: encapsulates business logic specific to each class
      - CharacterService
        - Handles character creation, validation, saving and loading.
      - BattleService
        - Handles calculations for battles and outputting the battle log.
    - Controllers: contains the API routes and handlers. 
      - CharacterController
        - GetCharacters: retrieves list of all characters.
        - Get: Given a character ID, returns the character in response.
        - Create: Given name and job, validates inputs, creates the character in memory, and returns the character in response.
        - Save: Saves a character's state so values like HP are persisted.
        - Validators for Character and its properties.
      - BattleController
        - Battle: Given two character IDs, validates the inputs, and then pits the two characters against one another in a battle to the death. Saves the character statuses. Returns the battle log in response.
  - Tests: these should follow naming/folder conventions for the class and service files they target. There aren't many now, so I've left them in root, but if they become more numerous then we should add folders to match.
    - I've written these to test each endpoint in all controllers, and each method in all services. I didn't write tests for the private methods but it is left to do for later.

### Limitations and Quirks
- Convert.ToInt32 is used for calculating attack and speed modifiers. If the result is halfway between whole numbers, it is rounded to the even number.
- If the character list gets very big we should consider paginating the Character GET response.

### How-Tos
- When adding a new job:
  - add the lowercase name to the list of valid jobs in CharacterService.cs
  - add a new job class to the Models/Jobs folder, inheriting from the base Job class.