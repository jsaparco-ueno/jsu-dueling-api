using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using DuelistApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Services
{
  public class CharacterService
  {
    // This is the in-memory list of characters. In a real application this would likely be stored in a database.
    private List<Character> _characters;

    public CharacterService()
    {
      _characters = new List<Character>();

      // initialize character list with 7 characters
      for (int i = 0; i <= 7; i++)
      {
        Save(Build(GetRandomName(), GetRandomJob()));
      }
    }

    public List<Character> GetCharacters()
    {
      return _characters;
    }

    public Character Get(int id)
    {
      return _characters[id];
    }

    /***
     *  Throws exception if validation fails.
     */
    public Character Create(string name, string job)
    {
      validateName(name);
      validateJob(job);

      return Save(Build(name,job));
    }

    // Private methods
    // ---------------
    private static Character Build(string name, string job)
    {
      var character = new Character();
      character.Name = name;

      switch (job)
      {
        case "Warrior":
          character.Job = new Warrior();
          break;
        
        case "Thief":
          character.Job = new Thief();
          break;

        case "Mage":
          character.Job = new Mage();
          break;

        default:
          character = null;
          // The entry was not a valid job! Do something!
          Console.WriteLine($"Invalid job: {job}");
          break;
      }

      character.CurrentHealthPoints = character.Job.HealthPoints;

      return character;
    }

    private Character Save(Character character)
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

    private string GetRandomJob()
    {
      var randomInt = new Random();
      return _jobs[randomInt.Next(0, _jobs.Count - 1)];

    }

    // Throws exception if name is not valid.
    private void validateName(string name)
    {
      if (name == null)
        throw new Exception("Name must be non-empty.");

      // There's a length requirement here, please add it *****************************************
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
          throw new Exception($"Invalid name: {name}");
        }
      }
    } 

    // Throws exception if job is not valid.
    private void validateJob(string job)
    {
      if (_jobs.Contains(job) == false)
      {
        throw new Exception($"Invalid job: {job}");
      }
    }
  }
}