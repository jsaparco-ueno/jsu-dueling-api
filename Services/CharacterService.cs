using System;
using System.Collections.Generic;
using System.Linq;
using DuelistApi.Models;

namespace DuelistApi.Services
{
  public class CharacterService
  {
    // This is the in-memory list of characters. In a real application this would likely be stored in a database.
    private List<Character> _characters;

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

    private readonly List<string> _jobs = new()
    {
      "warrior",
      "thief",
      "mage"
    };

    public CharacterService()
    {
      _characters = new List<Character>();

      // initialize character list with 7 characters
      for (int i = 0; i <= 7; i++)
      {
        _characters.Add(Create(GetRandomName(), GetRandomJob()));
      }
    }

    public string GetRandomName()
    {
      var randomInt = new Random();
      return _names[randomInt.Next(0, _names.Count - 1)];
    }

    public string GetRandomJob()
    {
      var randomInt = new Random();
      return _jobs[randomInt.Next(0, _jobs.Count - 1)];

    }

    public Character Create(string name, string job)
    {
      var character = new Character();
      character.Name = name;

      switch (job)
      {
        case "warrior":
          character.Job = new Warrior();
          break;
        
        case "thief":
          character.Job = new Thief();
          break;

        case "mage":
          character.Job = new Mage();
          break;

        default:
          character = null;
          // The entry was not a valid job! Do something!
          Console.WriteLine($"Invalid job: {job}");
          break;
      }

      return character;
    }

    public List<Character> GetCharacters()
    {
      return _characters;
    }
  }
}