using DuelistApi.Models;
using DuelistApi.Services;

namespace DuelistApi.Tests;

public class JobServiceTests
{
  JobService _jobService;

  public JobServiceTests()
  {
    _jobService = new JobService();
  }

  [Fact]
  public void GetJobs_ReturnsJobs()
  {
    var jobs = _jobService.GetJobs();

    Assert.IsType<List<Job>>(jobs);
    Assert.NotEmpty(jobs);
  }

  [Fact]
  public void Build_ReturnsJobInstances()
  {
    var jobNames = _jobService.GetJobNames;
    List<Job> jobs = new List<Job>();

    foreach (var jobName in jobNames)
    {
      jobs.Add(_jobService.Build(jobName));
    }

    Assert.IsType<List<Job>>(jobs);
    jobNames.ForEach(jobName => Assert.Contains(jobName, jobs.Select(j => j.Name)));
  }

  [Fact]
  public void GetRandomJobName_ReturnsValidName()
  {
    var jobs = _jobService.GetJobs();
    
    var jobName = _jobService.GetRandomJobName();

    Assert.Contains(jobName, jobs.Select(j => j.Name));
  }

}