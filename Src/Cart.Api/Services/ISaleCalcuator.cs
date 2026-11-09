using Cart.Api.Models;

namespace Cart.Api.Services;

public interface ISaleCalcuator
{
    decimal Calcuate(SaleContext model);
}