using System.ComponentModel.DataAnnotations;

public class SignUpRequest
{
    [Required(ErrorMessage = "Имя обязательно.")]
    [StringLength(128, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 128 символов.")]
    [RegularExpression(
        @"^[A-Za-zА-Яа-я\s,]+$",
        ErrorMessage = "Имя должно содержать только латинские символы и кирилицу. Знаки препинания только пробел."
    )]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email обязателен.")]
    [EmailAddress(ErrorMessage = "Некорректный формат адреса электронной почты.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [StringLength(128, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Подтверждение пароля обязательно.")]
    [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля должны совпадать.")]
    public required string ConfirmPassword { get; set; }
}
