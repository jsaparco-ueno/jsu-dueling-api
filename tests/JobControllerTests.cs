using DuelistApi.Services;
using DuelistApi.Controllers;
using DuelistApi.Models;

namespace DuelistApi.Tests;

public class JobControllerTests
{
  JobService _jobService;
  CharacterService _characterService;
  JobController _jobController;

  public JobControllerTests()
  {
    _jobService = new JobService();
    _characterService = new CharacterService(_jobService);
    _jobController = new JobController(_characterService);
  }

  [Fact]
  public void Get_ReturnsAllJobs()
  {
    var jobs = _jobController.Get();

    Assert.IsType<List<Job>>(jobs);
    Assert.NotEmpty(jobs);
  }
}