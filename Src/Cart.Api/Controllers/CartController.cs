using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        ArgumentNullException.ThrowIfNull(cartService);
       _cartService = cartService;
    }

    [Description("Зполнить товарами.")]
    [HttpPost("Add/{userProfileType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult AddRange([FromRoute] UserProfileType userProfileType, [FromBody] IReadOnlyCollection<CartItemModel> items)
    {
        _cartService.AddRange(items, userProfileType);
        return Ok();
    }

    [Description("Итог.")]
    [HttpGet("Total")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Total()
    {
        return Ok(_cartService.Total);
    }
}
