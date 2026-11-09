namespace Cart.Api.Models;

public class SaleContext
{
    public DateTimeOffset StartDate { get; init; }
    public bool IsGraduate { get; init; }
    public decimal Price { get; init; }
}