using Cart.Api.Models;

namespace Cart.Api.Services;

/// <inheritdoc/>
internal sealed class CartService : ICartService
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly ISaleCalcuator _saleCalcuator;

    public CartService(
        IHttpContextAccessor httpContextAccessor,
        ISaleCalcuator saleCalcuator)
    {
        _httpContextAccessor = httpContextAccessor;
        _saleCalcuator = saleCalcuator;
    }

    /// <inheritdoc/>
    public decimal Total => _httpContextAccessor.GetSessionValue<CartModel>()?.Total ?? 0;

    /// <inheritdoc/>
    public void AddRange(IReadOnlyCollection<CartItemModel> items, UserProfileType userProfileType)
    {
        ArgumentNullException.ThrowIfNull(items);
        var total = items.Sum(x => _saleCalcuator.Calcuate(new SaleContext
        {
            IsGraduate = userProfileType == UserProfileType.IsGraduate,
            StartDate = x.StartDate,
            Price = x.Price,
        }));
        _httpContextAccessor.SetSessionValue(new CartModel
        {
            Items = items,
            Total = total,
        });
    }
}