using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers;

public class StatusController : ControllerBase
{
    private readonly IProvideStatusInformation _statusService;
    private readonly ISystemTime _systemTime;

    public StatusController(IProvideStatusInformation statusService, ISystemTime systemTime)
    {
        _statusService = statusService;
        _systemTime = systemTime;
    }

    [HttpGet("status")]
    public async Task<ActionResult> GetTheStatusAsync()
    {
        //var response = new GetStatusResponse("Looks Good Here", DateTime.Now, new OnCallDeveloperInfo("Bob Smith", "bob@aol.com", "555-1212"));
        GetStatusResponse response = await _statusService.GetStatusResponseAsync();
        return Ok(response);
    }
}
