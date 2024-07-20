using DuelistApi.Services;

namespace DuelistApi.Tests;

public class BattleServiceTests
{
  CharacterService _characterService;
  BattleService _battleService;

  public BattleServiceTests()
  {
    _characterService = new CharacterService();
    _battleService = new BattleService(_characterService);
  }

  [Fact]
  public void Battle_ReturnsLog()
  {
    var log = _battleService.Battle(1,2);
  }
}