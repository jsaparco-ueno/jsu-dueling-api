using System;
using System.Collections.Generic;
using System.Linq;
using DuelistApi.Models;

namespace DuelistApi.Services
{
  public class CharacterService
  {
    private JobService _jobService;

    // This is the in-memory list of characters. In a real application this would likely be stored in a database.
    private List<Character> _characters;

    public CharacterService(JobService jobService)
    {
      _jobService = jobService;

      _characters = new List<Character>();

      // initialize character list with 7 characters
      for (int i = 0; i <= 7; i++)
      {
        SaveNew(Build(GetRandomName(), _jobService.GetRandomJobName()));
      }
    }

    public List<Character> GetCharacters()
    {
      return _characters;
    }

    public Character Get(int id)
    {
      ValidateCharacterId(id);

      return _characters[id];
    }

    /***
      *  Throws exception if validation fails.
      */
    public Character Create(string name, string job)
    {
      ValidateName(name);
      ValidateJob(job);

      return SaveNew(Build(name,job));
    }

    public Character Save(Character character)
    {
      _characters[character.Id] = character;

      return character;
    }

    public List<Job> GetAllJobs()
    {
      return _jobService.GetJobs();
    }

    // Validators
    // ----------

    // Throws exception if name is not valid.
    public void ValidateName(string name)
    {
      if (name == "")
        throw new Exception("Name must be non-empty.");

      if (name.Length < 4 || name.Length > 15)
        throw new Exception("Name must be between 4 characters to 15 characters (inclusive).");

      var lowercase = Enumerable.Range('a', 'z'-'a'+1).Select(x => (char)x).ToList();
      var uppercase = Enumerable.Range('A', 'Z'-'A'+1).Select(x => (char)x).ToList();
      var specialCharacters = new List<char> { '_' };

      var validCharacters = new List<char>();
      validCharacters.AddRange(lowercase);
      validCharacters.AddRange(uppercase);
      validCharacters.AddRange(specialCharacters);

      foreach (char c in name)
      {
        if (!validCharacters.Contains(c))
        {
          throw new Exception($"Invalid name: character '{c}' in name {name} is not allowed.  Names must contain letters and underscores.");
        }
      }
    } 

    // Throws exception if job is not valid.
    public void ValidateJob(string job)
    {
      if (_jobs.Contains(job) == false)
      {
        throw new Exception($"Invalid job: {job}");
      }
    }

    // Throws exception if a character using the Id does not exist.
    public void ValidateCharacterId(int id)
    {
      if (id >= _characters.Count)
        throw new Exception($"Character with Id not found: {id}");
    }

    // Throws exception if a character is not valid.
    public void ValidateCharacter(Character character)
    {
      ValidateCharacterId(character.Id);
      ValidateName(character.Name);
      ValidateJob(character.Job.Name);
    }

    // Private methods
    // ---------------
    private Character Build(string name, string job)
    {
      var character = new Character
      {
          Name = name,
          Job = _jobService.Build(job)
      };

      character.CurrentHealthPoints = character.Job.HealthPoints;

      return character;
    }

    private Character SaveNew(Character character)
    {
      // Calculates the next valid Id
      character.Id = _characters.Count;
      _characters.Add(character);

      return character;
    }

    // Helpers
    // -------
    // Methods that support the main Character methods but don't serve other classes.
    // We should consider sharing helper methods in a separate class and file if other classes need these.


    // To enable random names when you can't think of a cool one.
    private readonly List<string> _names = new()
    {
      "Aurelia",
      "Bastian",
      "Cassandra",
      "Darius",
      "Elowen",
      "Finnian",
      "Guinevere",
      "Hawthorne",
      "Isolde",
      "Jasper",
      "Kieran",
      "Lysandra",
      "Morrigan",
      "Nikolai",
      "Ophelia",
      "Peregrine",
      "Quintessa",
      "Rhiannon",
      "Sebastian",
      "Thalia",
      "Ulysses",
      "Vesper",
      "Wren",
      "Xander",
      "Yvaine",
      "Zephyr",
    };

    // The list of valid job names.  This needs to be updated when a new job is added.
    // Refer to this list when we need the defacto list of valid jobs.
    private readonly List<string> _jobs = new()
    {
      "Warrior",
      "Thief",
      "Mage"
    };

    private string GetRandomName()
    {
      var randomInt = new Random();
      return _names[randomInt.Next(0, _names.Count - 1)];
    }
  }
}