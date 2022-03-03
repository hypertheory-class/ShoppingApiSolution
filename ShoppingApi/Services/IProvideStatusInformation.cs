using ShoppingApi.Models;

namespace ShoppingApi.Services;

public interface IProvideStatusInformation
{
    Task<GetStatusResponse> GetStatusResponseAsync();
}
