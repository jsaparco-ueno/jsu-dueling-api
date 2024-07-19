### Dueling API
Technical Assessment for Neo Financial | Senior Backend Developer | Justin Saparco-Ueno
***

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

### Dev Journal

#### 2024-07-18
- `dotnet new react -o jsu-dueling-api` to create the new app. Has bootstrap for styling, React for frontend Single Page Application, C# for backend
- Add models
- I see that I need to add validation, will be doing that on the services/handler.
- Decided that attack and speed modifier are a Job class method. Attack and Speed modifier methods are abstract on the base Job class, where deriving Job classes like Warrior will implement the modifier methods.
- documented a quirk: Convert.ToInt32 is used for calculating attack and speed modifiers. If the result is halfway between whole numbers, it is rounded to the even number
- I need to get CharacterFactory working how I want. CharacterFactory takes a name and valid job, validates the name and job, and outputs a Character instance with Job being an instance of the chosen subclass.


#### 2024-07-17
- Added entries and outline in this journal
