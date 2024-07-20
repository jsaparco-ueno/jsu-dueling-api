using DuelistApi.Services;

namespace DuelistApi.Tests;

public class CharacterServiceTests
{
  CharacterService _characterService;
  public CharacterServiceTests()
  {
    _characterService = new CharacterService();
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
}