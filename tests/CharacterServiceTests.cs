using DuelistApi.Models;
using DuelistApi.Services;

namespace DuelistApi.Tests;

public class CharacterServiceTests
{
  JobService _jobService;
  CharacterService _characterService;
  public CharacterServiceTests()
  {
    _jobService = new JobService();
    _characterService = new CharacterService(_jobService);
  }
  [Fact]
  public void Constructor_InitializesCharacterList()
  {
    CharacterService testCharacterService = new CharacterService(_jobService);
    var characters = testCharacterService.GetCharacters();

    Assert.IsType<List<Character>>(characters);
    Assert.NotEmpty(characters);
  }

  [Fact]
  public void GetCharacters_ReturnsCharacterList()
  {
    var characters = _characterService.GetCharacters();

    Assert.IsType<List<Character>>(characters);
    Assert.NotEmpty(characters);
  }

  [Fact]
  public void GetById_ReturnsCharacter()
  {
    var character = _characterService.Get(0);

    Assert.IsType<Character>(character);
  }

  [Fact]
  public void GetById_RaisesExceptionWithMessageIfIdInvalid()
  {
    string message = "";

    try
    {
      _characterService.Get(777);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Character with Id not found: 777", message);
  }

  [Fact]
  public void Create_AddsNewCharacterToList()
  {
    var name = "Xx_Jones_xX";
    var job = "Warrior";

    var character = _characterService.Create(name, job);
    var characterList = _characterService.GetCharacters();
    
    Assert.Contains(character, characterList);
  }

  [Fact]
  public void Create_RaisesExceptionWithMessageIfNameInvalid()
  {
    var name = "Jones_22503";
    var job = "Warrior";
    var message = "";

    try
    {
      _characterService.Create(name, job);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Invalid name: character '2' in name Jones_22503 is not allowed.  Names must contain letters and underscores.", message);
  }

  [Fact]
  public void Create_RaisesExceptionWithMessageIfJobInvalid()
  {
    var name = "Jones";
    var job = "Sumo Wrestler"; // The datamined leak was fake, we're truly not working on this
    var message = "";

    try
    {
      _characterService.Create(name, job);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Invalid job: Sumo Wrestler", message);
  }

  [Fact]
  public void Save_PersistsCharacter()
  {
    var character = _characterService.Get(0);
    var healthBeforeSave = character.CurrentHealthPoints;

    character.CurrentHealthPoints = 0;
    _characterService.Save(character);

    var savedCharacter = _characterService.Get(character.Id);
    Assert.Equal(savedCharacter.CurrentHealthPoints, character.CurrentHealthPoints);
  }

  [Fact]
  public void ValidateName_DoesNotThrowIfValid()
  {
    var name = "Xx_Jones_xX";
    var message = "";

    try
    {
      _characterService.ValidateName(name);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Empty(message);
  }

  [Fact]
  public void ValidateName_ThrowsIfNameEmpty()
  {
    var name = "";
    var message = "";

    try
    {
      _characterService.ValidateName(name);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Name must be non-empty.", message);
  }

  [Fact]
  public void ValidateName_ThrowsIfNameShort()
  {
    var name = "Jon";
    var message = "";

    try
    {
      _characterService.ValidateName(name);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Name must be between 4 characters to 15 characters (inclusive).", message);
  }

  [Fact]
  public void ValidateName_ThrowsIfNameLong()
  {
    var name = "XxxXxxX_Jones_XxxXxxX";
    var message = "";

    try
    {
      _characterService.ValidateName(name);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Name must be between 4 characters to 15 characters (inclusive).", message);
  }

  [Fact]
  public void ValidateJob_DoesNotThrowIfValid()
  {
    var job = "Warrior";
    var message = "";

    try
    {
      _characterService.ValidateJob(job);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Empty(message);
  }

  [Fact]
  public void ValidateJob_ThrowsIfJobNotFound()
  {
    var job = "Sumo Wrestler";
    var message = "";

    try
    {
      _characterService.ValidateJob(job);
    }
    catch (Exception e)
    {
      message = e.Message;
    }

    Assert.Equal("Invalid job: Sumo Wrestler", message);
  }

  // Testing these private methods requires:
  // - adding a wrapper method that is internal protected and calls the private method
  //   - e.g. internal protected Build_UnitTestWrapper(string name, string job) { return Build(name, job) }
  // - adding this above namespace DuelistApi.Services in each service:
  //   - [assembly: InternalsVisibleTo("DuelistApi.Tests")]
  // Build_ReturnsCharacter();
  // SaveNew_AddsCharacter();
  // GetRandomName_ReturnsName();
  // GetRandomJob_ReturnsJob();
}