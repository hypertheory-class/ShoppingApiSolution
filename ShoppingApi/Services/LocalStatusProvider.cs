using ShoppingApi.Models;

namespace ShoppingApi.Services;

public class LocalStatusProvider : IProvideStatusInformation
{

    private readonly ISystemTime _systemTime;

    public LocalStatusProvider(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    public async Task<GetStatusResponse> GetStatusResponseAsync()
    {
        var response = new GetStatusResponse("Looks Good Here", _systemTime.GetCurrent(), new OnCallDeveloperInfo("Bob Smith", "bob@aol.com", "555-1212"));
        return response;
        
    }
}
