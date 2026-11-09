using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Models;

[Description("Курс")]
public class CartItemModel
{
    [Description("Наименование курса")]
    [Required]
    public string Name { set; get; } = default!;

    [Description("Цена")]
    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { set; get; } = default!;

    [Description("Дата старта курса")]
    [Required]
    public DateTimeOffset StartDate { set; get; } = default!;
}

public class CartModel
{
    public IReadOnlyCollection<CartItemModel> Items { init; get; } = default!;
    public decimal Total { init; get; }
}