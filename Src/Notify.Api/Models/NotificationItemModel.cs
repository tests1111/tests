using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

[Description("Уведомление")]
public class NotificationItemModel
{
    [Description("Имя пользователя")]
    [StringLength(30)]
    public string Name { set; get; } = default!;

    [Description("Телефон пользователя")]
    [Phone]
    [StringLength(15)]
    public string Phone { set; get; } = default!;

    [Description("Эл почта пользователя")]
    [EmailAddress]
    [StringLength(30)]
    public string Email { set; get; } = default!;

    [Description("Тип пользователя")]
    [Range(1, 2)]
    public PersonType PersonType { set; get; } = default!;
}

[Description("Тип пользователя")]
public enum PersonType
{
    [Description("Не задано")]
    Default = 0,

    [Description("Физ лицо")]
    Individual,
    
    [Description("Юр лицо")]
    Company
}