using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Tests
{
  public static class TestHelpers
  {
    public static HttpStatusCode? GetStatusCode(IActionResult response)
    {
      if (response == null) return null;

      return (HttpStatusCode)response.GetType().GetProperty("StatusCode").GetValue(response, null);
    }
  }
}