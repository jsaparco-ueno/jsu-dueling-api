using System.Net;
using DuelistApi.Services;
using Microsoft.AspNetCore.Authentication;
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

  [Fact]
  public void Battle_ReturnsLog()
  {
    var request = new BattleRequest("1","2");
    var response = _battleController.Battle(request);

    Assert.Equal(TestHelpers.GetStatusCode(response), HttpStatusCode.OK);
    Assert.IsType<string>((response as OkObjectResult).Value);
  }
}