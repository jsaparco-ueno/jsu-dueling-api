using DuelistApi.Services;

namespace DuelistApi.Tests;

public class CharacterServiceTests
{
  CharacterService _characterService;
  public CharacterServiceTests()
  {
    _characterService = new CharacterService();
  }

  // Left these tests to do for later.
  // We have enough coverage with the controller integration test.
  // -------------------------------------------------------------
  // Constructor_InitializesCharacterList()
  // GetCharacters_ReturnsCharacterList()
  // GetById_ReturnsCharacter()
  // GetById_ReturnsBadRequestIfIdInvalid()
  // Create_AddsNewCharacterToList()
  // Create_ReturnsBadRequestIfNameInvalid()
  // Create_ReturnsBadRequestIfJobInvalid()

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

  // Left these tests to do for later.
  // We have enough coverage with the controller integration test.
  // -------------------------------------------------------------
  // ValidateName_DoesNotThrowIfValid()
  // ValidateName_ThrowsIfNameEmpty()
  // ValidateName_ThrowsIfNameShort()
  // ValidateName_ThrowsIfNameLong()
  // ValidateJob_DoesNotThrowIfValid()
  // ValidateJob_ThrowsIfJobNotFound()
  
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