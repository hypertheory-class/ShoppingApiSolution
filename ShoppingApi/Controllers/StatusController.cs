using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers;

public class StatusController : ControllerBase
{
    private readonly IProvideStatusInformation _statusService;

    public StatusController(IProvideStatusInformation statusService)
    {
        _statusService = statusService;
    }

    [HttpGet("status")]
    public async Task<ActionResult> GetTheStatusAsync()
    {
        //var response = new GetStatusResponse("Looks Good Here", DateTime.Now, new OnCallDeveloperInfo("Bob Smith", "bob@aol.com", "555-1212"));
        GetStatusResponse response = await _statusService.GetStatusResponseAsync();
        return Ok(response);
    }
}
