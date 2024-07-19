using DuelistApi.Services;
using DuelistApi.Controllers;
using DuelistApi.Models;

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
    public void Get()
    {
        var result = _characterController.Get();
        Assert.IsType<List<Character>>(result);
    }
}