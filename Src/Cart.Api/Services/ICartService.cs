using Cart.Api.Models;

namespace Cart.Api.Services;

public interface ICartService
{
    void AddRange(IReadOnlyCollection<CartItemModel> items, UserProfileType userProfileType);
    decimal Total { get; }
}