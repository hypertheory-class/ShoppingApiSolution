namespace ShoppingApi.Services;

public class OnCallDeveloperHttpService
{
    private readonly HttpClient _httpClient;

    public OnCallDeveloperHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "shopping-api");
    }

    public async Task<GetOnCallDeveloperResponse> GetOnCallDeveloperAsync()
    {
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode(); // This will throw an exception if it isn't a 200-299 response. I'll talk about this in a bit.

        var developer = await response.Content.ReadFromJsonAsync<GetOnCallDeveloperResponse>();
        return developer!;
    }

}



public record GetOnCallDeveloperResponse(string firstName, string lastName, string emailAddress, string telephoneNumber);