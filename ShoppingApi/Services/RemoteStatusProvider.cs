using ShoppingApi.Models;

namespace ShoppingApi.Services;

public class RemoteStatusProvider : IProvideStatusInformation
{
    private readonly ISystemTime _systemTime;
    private readonly OnCallDeveloperHttpService _callDeveloperHttpService;

    public RemoteStatusProvider(ISystemTime systemTime, OnCallDeveloperHttpService callDeveloperHttpService)
    {
        _systemTime = systemTime;
        _callDeveloperHttpService = callDeveloperHttpService;
    }

    public async Task<GetStatusResponse> GetStatusResponseAsync()
    {
        //var myPart - new GetStatusResponse()
        OnCallDeveloperInfo developer = null;

        try
        {
            var onCallDeveloperInfo = await _callDeveloperHttpService.GetOnCallDeveloperAsync();

            developer = new OnCallDeveloperInfo($"{onCallDeveloperInfo.firstName} {onCallDeveloperInfo.lastName}", onCallDeveloperInfo.telephoneNumber, onCallDeveloperInfo.emailAddress);
        }
        catch (HttpRequestException)
        {
            developer = new OnCallDeveloperInfo("Unavailable, Call Luke.", "luke@aol.com", "800 555-1212");

        }

        var response = new GetStatusResponse("This is good, too", _systemTime.GetCurrent(), developer);

        return response;
    }
}
