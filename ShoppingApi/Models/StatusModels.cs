namespace ShoppingApi.Models;



public record OnCallDeveloperInfo(string name, string email, string phoneNumber);

public record GetStatusResponse(string message, DateTime lastChecked, OnCallDeveloperInfo onCallDeveloper);