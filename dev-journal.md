### Dueling API
Technical Assessment for Neo Financial | Senior Backend Developer | Justin Saparco-Ueno
***

### Directory
- ClientApp
  - Holds the React app files that handle the frontend.
- Controllers
  - API routes and handlers.
- Models
  - Defines the entity classes.
- Services
  - Defines the logic used for handlers and constructors.  It is logically categorized by the entities they serve.

### Outline
- This API will have the following parts:
  - Models, contains class files each representing objects in the namespace and their properties
    - Character
    - Job
      - Warrior, Thief, Mage are subclasses of job
    - Battle
  - Services, encapsulates business logic specific to each class
    - CharacterService
    - JobService
    - BattleService
  - Controllers, contains the API routes and handlers. 
    - Concerned only with receive request and transmitting response. 
    - Relies on Services for class specific logic, like providing data. 
    - Forms the response with this data but does not otherwise transform it.
  - Tests, which follow naming/folder conventions for the class and service files they target. 
    - xUnit
  - Web, contains minimal js/ts/html for a React frontend. Structured logically.

### Limitations and Quirks
- Convert.ToInt32 is used for calculating attack and speed modifiers. If the result is halfway between whole numbers, it is rounded to the even number.

### How-Tos
- When adding a new job:
  - add the lowercase name to the list of valid jobs in CharacterService.cs
  - add a new job class to the Models/Jobs folder, inheriting from the base Job class.

### Dev Journal

#### 2024-07-19
- Services should be a singleton that is added at startup.
- Names are hard to think of so I added a list of cool names that we can pick from when initializing the in-memory character list.
- When the character service singleton is instantiated, the constructor will automatically make 7 new characters and add them to the in-memory list.
- Added a How-Tos section and a description of how to add new jobs.
- I added CharacterService and made it a singleton. It stores the in-memory list of Characters, so henceforth there will only be one list in memory.  If there was a database, I'd introduce an ICharacterService interface to abstract the database operations away from the CRUD handlers.

#### 2024-07-18
- `dotnet new react -o jsu-dueling-api` to create the new app. Has bootstrap for styling, React for frontend Single Page Application, C# for backend
- Add models
- I see that I need to add validation, will be doing that on the services/handler.
- Decided that attack and speed modifier are a Job class method. Attack and Speed modifier methods are abstract on the base Job class, where deriving Job classes like Warrior will implement the modifier methods.
- documented a quirk: Convert.ToInt32 is used for calculating attack and speed modifiers. If the result is halfway between whole numbers, it is rounded to the even number
- I need to get CharacterFactory working how I want. CharacterFactory takes a name and valid job, validates the name and job, and outputs a Character instance with Job being an instance of the chosen subclass.
- We set the Job properties with the subclass constructors using the factory pattern in CharacterService Create method.
- I need tests

#### 2024-07-17
- Added entries and outline in this journal
