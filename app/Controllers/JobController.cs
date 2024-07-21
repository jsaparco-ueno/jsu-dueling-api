using DuelistApi.Models;
using Microsoft.AspNetCore.Mvc;
using DuelistApi.Services;
using System.Collections.Generic;

namespace DuelistApi.Controllers
{
  [ApiController]
  public class JobController : ControllerBase
  {
    private readonly CharacterService _characterService;

    public JobController(CharacterService characterService)
    {
      _characterService = characterService;
    }

    [HttpGet]
    [Route("[controller]/get")]
    public IEnumerable<Job> Get()
    {
      return _characterService.GetAllJobs();
    }
  }
}