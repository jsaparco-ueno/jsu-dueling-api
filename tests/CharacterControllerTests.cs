using DuelistApi.Services;
using DuelistApi.Controllers;
using DuelistApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuelistApi.Tests;

public class CharacterControllerTests
{
  CharacterService _characterService;
  CharacterController _characterController;

  public CharacterControllerTests()
  {
    _characterService = new CharacterService();
    _characterController = new CharacterController(_characterService);
  }

  [Fact]
  public void GetById_ReturnsSingleCharacter()
  {
    var result = _characterController.Get(0);
    Assert.Equal(GetStatusCode(result), HttpStatusCode.OK);
    Assert.IsType<Character>((result as OkObjectResult).Value);
  }

  [Fact]
  public void GetById_ReturnsBadRequestIfIdNotFound()
  {
    var result = _characterController.Get(777);
    Assert.Equal(GetStatusCode(result), HttpStatusCode.BadRequest);
  }

  [Fact]
  public void GetAll_ReturnsListOfCharacters()
  {
    var result = _characterController.Get();
    Assert.IsType<List<Character>>(result);
  }

  [Fact]
  public void Create_ReturnsCreatedCharacter()
  {
    var name = "Xx_Jones_xX";
    var job = "Warrior";
    var request = new CharacterRequest(name, job);
    var oldList = new List<Character>(_characterService.GetCharacters());

    var newCharacter = (Character)(_characterController.Create(request) as OkObjectResult).Value;
    var newList = _characterService.GetCharacters();

    Assert.NotEqual(oldList, newList);
    Assert.DoesNotContain(newCharacter, oldList);
    Assert.Contains(newCharacter, newList);
    Assert.Equal(newCharacter.Name, name);
    Assert.Equal(newCharacter.Job.Name, job);
  }

  [Fact]
  public void Create_ReturnsBadRequestIfNameInvalid()
  {
    var name = "Jones_22503";
    var job = "Warrior";
    var request = new CharacterRequest(name, job);
    var oldList = new List<Character>(_characterService.GetCharacters());

    var response = _characterController.Create(request);
    var newList = _characterService.GetCharacters();

    Assert.Equal(oldList, newList);
    Assert.Equal(GetStatusCode(response), HttpStatusCode.BadRequest);
  }

  [Fact]
  public void Create_ReturnsBadRequestIfJobInvalid()
  {
    var name = "Jones";
    var job = "Sumo Wrestler"; // Not planned for release anytime soon
    var request = new CharacterRequest(name, job);
    var oldList = new List<Character>(_characterService.GetCharacters());

    var response = _characterController.Create(request);
    var newList = _characterService.GetCharacters();

    Assert.Equal(oldList, newList);
    Assert.Equal(GetStatusCode(response), HttpStatusCode.BadRequest);
  }

  [Fact]
  public void Create_ReturnsBadRequestIfNameMissing()
  {
    var job = "Warrior";
    var request = new CharacterRequest(null, job);
    var oldList = new List<Character>(_characterService.GetCharacters());

    var response = _characterController.Create(request);
    var newList = _characterService.GetCharacters();

    Assert.Equal(oldList, newList);
    Assert.Equal(GetStatusCode(response), HttpStatusCode.BadRequest);
  }

  [Fact]
  public void Create_ReturnsBadRequestIfJobMissing()
  {
    var name = "Jones";
    var request = new CharacterRequest(name, null);
    var oldList = new List<Character>(_characterService.GetCharacters());

    var response = _characterController.Create(request);
    var newList = _characterService.GetCharacters();

    Assert.Equal(oldList, newList);
    Assert.Equal(GetStatusCode(response), HttpStatusCode.BadRequest);
  }


  // Helpers
  // -------

  public static HttpStatusCode? GetStatusCode(IActionResult response)
  {
    if (response == null) return null;

    return (HttpStatusCode)response.GetType().GetProperty("StatusCode").GetValue(response, null);
  }
}