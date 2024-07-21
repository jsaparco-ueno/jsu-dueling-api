using DuelistApi.Services;

namespace DuelistApi.Tests;

public class BattleServiceTests
{
  JobService _jobService;
  CharacterService _characterService;
  BattleService _battleService;

  public BattleServiceTests()
  {
    _jobService = new JobService();
    _characterService = new CharacterService(_jobService);
    _battleService = new BattleService(_characterService);
  }

  [Fact]
  public void Battle_ReturnsLog()
  {
    var log = _battleService.Battle(1,2);

    Assert.NotEmpty(log);
  }

  [Fact]
    public void RollAndApplyDamage_AppliesDamage()
    {
      var attacker = _characterService.Get(0);
      var defender = _characterService.Get(1);
      var healthBefore = defender.CurrentHealthPoints;
      
      var damage = _battleService.RollAndApplyDamage(attacker, defender);

      Assert.Equal(healthBefore - damage, defender.CurrentHealthPoints);
    }

  [Fact]
  public void RollAndApplyDamage_SetsHealthToZeroIfOverkill()
  {
      var attacker = _characterService.Get(0);
      var defender = _characterService.Get(1);
      defender.CurrentHealthPoints = -1;
      
      _battleService.RollAndApplyDamage(attacker, defender);

      Assert.True(defender.CurrentHealthPoints == 0);
  }

  [Fact]
  public void Roll_ReturnsIntInRange()
  {
    var roll = _battleService.Roll(10);

    Assert.True(roll >= 0 && roll <= 10);
  }

  // Testing these private methods requires:
  // - adding a wrapper method that is internal protected and calls the private method
  //   - e.g. internal protected BeginBattleMessage_UnitTestWrapper(Character characterOne, Character characterTwo) { return BeginBattleMessage(characterOne, characterTwo) }
  // - adding this above namespace DuelistApi.Services in each service:
  //   - [assembly: InternalsVisibleTo("DuelistApi.Tests")]
  // public void BeginBattleMessage_ReturnsString()
  // public void SpeedMessage_ReturnsString()
  // public void AttackMessage_ReturnsString()
  // public void WinnerMessage_ReturnsString()
}