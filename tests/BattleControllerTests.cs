using System.Net;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;
using DuelistApi.Controllers;

namespace DuelistApi.Tests;

public class BattleControllerTests
{
  CharacterService _characterService;
  BattleService _battleService;
  BattleController _battleController;

  public BattleControllerTests()
  {
    _characterService = new CharacterService();
    _battleService = new BattleService(_characterService);
    _battleController = new BattleController(_battleService);
  }

  // Because this test uses seed data which is procedurally generated on startup, this test could flake.
  // This applies to all of the tests that use the seed data, but I wanted to point it out here.
  [Fact]
  public void Battle_ReturnsLog()
  {
    var request = new BattleRequest(1,2);
    var response = _battleController.Battle(request);

    Assert.Equal(TestHelpers.GetStatusCode(response), HttpStatusCode.OK);
    var battleLog = (response as OkObjectResult).Value;
    Assert.IsType<string>(battleLog);
    Assert.Contains("begins!", (string)battleLog);
    Assert.Contains("wins the battle!", (string)battleLog); 
  }
}