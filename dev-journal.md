### Dev Journal

#### 2024-07-20
- To run a Battle and return the log:
  - curl -v -d '{\"CharacterOneId\":\"0\", \"CharacterTwoId\":\"1\"}' -H "Content-Type: application/json" https://localhost:5001/battle
- if param mapping is failing for weird reasons, e.g.: System.InvalidOperationException: Each parameter in constructor 'Void .ctor(System.String, System.String)' on type 'DuelistApi.Controllers.BattleRequest' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object. The match can be case-insensitive.
- The param names in the request object constructor must match the property names or else you get the above error
- Yes, we deserialize the JSON from the request and try to form the [FromBody]<type> object. It's counting on having matching names.
- what remains?  I should test methods in BattleService and check that I've covered CharacterService too.
- I missed some Character validators that I should add: Health not zero, attack modifier non-negative, speed modifier non-negative
- we will want to test those validators
- also, don't forget to remove unused references
- I tightened up the battle loop. The challenge is how to avoid repeating code while keeping the ability to break the loop early since submethods can't break a caller's loop
- So I simplified the logging to make things easier to read and work with
- I should remove the sample files created when scaffolded
- I need to update the SPA with a note about how I didn't touch the frontend

#### 2024-07-19
- Services should be a singleton that is added at startup.
- Names are hard to think of so I added a list of cool names that we can pick from when initializing the in-memory character list.
- When the character service singleton is instantiated, the constructor will make 7 new characters with randomly chosen names/jobs and add them to the in-memory list.
- Added a How-Tos section and a description of how to add new jobs.
- I added CharacterService and made it a singleton. It stores the in-memory list of Characters, so henceforth there will only be one list in memory.  If there was a database, I'd introduce an ICharacterService interface to abstract the database operations away from the CRUD handlers.
- Added XUnit. To run tests, in a terminal cd to /tests and run `dotnet test`
- I'll write tests for each API endpoint.
- Getting POST endpoint working
- To create a new character:
  - curl -v -d '{\"Name\":\"Jones\", \"Job\":\"Warrior\"}' -H "Content-Type: application/json" https://localhost:5001/character/create
- To get a character:
  - curl https://localhost:5001/character/get/0
- got all CharacterController endpoints working and tested
- forgot to add a name length validator, just added it.
- Create battle game loop
  - validate:
    - character.name is non-empty
    - character.job.name is non-empty
    - current HP is greater than 0 for both characters
    - attack and speed modifier are >= 0
  - while both characters have current HP > 0
    - calculate speed (fastest speed attacks first)
    - calculate attack
    - subtract HP for damage from fastest attack, then other attack

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
