using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

[Description("Тип профиля пользователя")]
public enum UserProfileType
{
    [Description("Новый")]
    New = 0,

    [Description("Выпускник")]
    IsGraduate,
}