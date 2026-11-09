using Cart.Api.Models;

namespace Cart.Api.Services;

/// <summary>
/// без скидки
/// </summary>
internal class DefaultSaleCalcuator : ISaleCalcuator
{
    /// <inheritdoc/>
    public virtual decimal Calcuate(SaleContext saleModel) => saleModel.Price;
}

/// <summary>
/// скидка 20% за 5 дней до начала
/// </summary>
internal class FiveDaysSaleCalcuator : DefaultSaleCalcuator
{
    /// <inheritdoc/>
    public override decimal Calcuate(SaleContext saleModel) => saleModel.StartDate.DaysLeft(5)
        ? saleModel.Price - saleModel.Price * 0.2m
        : base.Calcuate(saleModel);
}

/// <summary>
/// скидка 30% для выпускников
/// </summary>
internal class GraduateSaleCalcuator : FiveDaysSaleCalcuator
{
    /// <inheritdoc/>
    public override decimal Calcuate(SaleContext saleModel) => saleModel.IsGraduate
        ? saleModel.Price - saleModel.Price * 0.3m
        : base.Calcuate(saleModel);
}