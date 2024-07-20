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

  // public void RollAndApplyDamage_AppliesDamage()
  // public void RollAndApplyDamage_SetsHealthToZeroIfOverkill()
  // public void Roll_ReturnsIntInRange()
  // public void BeginBattleMessage_ReturnsString()
  // public void SpeedMessage_ReturnsString()
  // public void AttackMessage_ReturnsString()
  // public void WinnerMessage_ReturnsString()
}