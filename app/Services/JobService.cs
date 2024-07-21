using System;
using System.Collections.Generic;
using DuelistApi.Models;

namespace DuelistApi.Services
{
  public class JobService
  {
    public List<Job> GetJobs()
    {
      List<Job> jobList = new List<Job>();
      foreach (var jobName in GetJobNames)
      {
        jobList.Add(Build(jobName));
      }
      return jobList;
    }

    public Job Build(string jobName)
    {
      switch (jobName)
      {
        case "Warrior":
          return new Warrior();
        case "Thief":
          return new Thief();
        case "Mage":
          return new Mage();
        default:
          // The entry was not a valid job! Do something!
          return null;
      }
    }

    public string GetRandomJobName()
    {
      var randomInt = new Random();
      return GetJobNames[randomInt.Next(0, GetJobNames.Count - 1)];
    }

    // The list of valid job names.  This needs to be updated when a new job is added.
    // Refer to this list when we need the defacto list of valid jobs.
    public readonly List<string> GetJobNames = new()
    {
      "Warrior",
      "Thief",
      "Mage"
    };
  }
}