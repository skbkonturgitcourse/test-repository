namespace SeleniumTests.PageObjects;

public class ConstantsForTests
{
    public const string email = "test@mail.ru";
    public const string wrongEmail = "test@mailru";
    public const string kirillianMail = "емэйл@собака.рф";
    public const string expectedResultTextForBoys = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
    public const string expectedResultTextForGirls = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
    public const string expectedValidationMessageEmptyEMail = "Введите email";
    public const string expectedValidationMessageWrongEMail = "Некорректный email";

}