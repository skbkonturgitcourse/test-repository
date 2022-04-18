using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPractice;

enum Gender { Boy, Girl }

public class PracticeParrotName
{
    private ChromeDriver driver;
    private readonly string _urlParrotName = "https://qa-course.kontur.host/selenium-practice/";
    private readonly string _titleFormText = "Мы подберём имя для твоего попугайчика!";
    private readonly By _titleFormLokator = By.ClassName("subtitle-bold");
    private readonly string _questionGenderFormText = "Кто у тебя?";
    private readonly By _questionGenderFormLokator = By.XPath("//*[@class='formQuestion'][1]");
    private readonly string _questionMailFormText = "На какой e-mail прислать варианты имён?";
    private readonly By _questionMailFormLokator = By.XPath("//*[@class='formQuestion'][2]");
    private readonly string _emailTrue = "test@mail.ru";
    private readonly string _email3LevelDomain = "test@mail.mail.ru";
    private readonly string _emailDomainIsDot = "test@.";
    private readonly string _emailLoginIsEmpty = "@mail.ru";
    private readonly By _radioBoyLokator = By.Id("boy");
    private readonly By _labelRadioBoyLokator = By.XPath("//*[@class='parrot']/label[1]");
    private readonly string _labelRadioBoyText = "мальчик";
    private readonly By _radioGirlLokator = By.Id("girl");
    private readonly By _labelRadioGirlLokator = By.XPath("//*[@class='parrot']/label[2]");
    private readonly string _labelRadioGirlText = "девочка";
    private readonly By _emailInputLokator = By.Name("email");
    private readonly string _placeholderText = "e-mail";
    private readonly By _buttonSendMeLokator = By.Id("sendMe");
    private readonly string _buttonSendMeText = "ПОДОБРАТЬ ИМЯ";
    private readonly By _resultTextLokator = By.ClassName("result-text");
    private readonly By _resultEmailLokator = By.ClassName("your-email");
    private readonly By _linkAnotherEmailLokator = By.Id("anotherEmail");
    private readonly string _linkAnotherEmailText = "указать другой e-mail";
    private readonly By _errorFormLokator = By.ClassName("form-error");


    private static string expectedResultGender(Gender gender)
    {
        string genderText = "Хорошо, мы пришлём имя для ";
        switch (gender)
        {
            case Gender.Boy:
                genderText += "вашего мальчика ";
                break;
            case Gender.Girl:
                genderText += "вашей девочки ";
                break;
        }

        genderText += "на e-mail:";
        return genderText;
    }

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }

    [Test]
    public void ParrotName_CheckElementsForm_Success()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitLoadForm = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitLoadForm.Until(e => e.FindElement(_emailInputLokator));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(_titleFormLokator).Displayed,
                "Заголовок формы не отображается");
            Assert.AreEqual(_titleFormText, driver.FindElement(_titleFormLokator).Text,
                $"\tЗаголовок формы в прототипе: '{_titleFormText}'\n" +
                $"Фактический заголовок в форме: '{driver.FindElement(_titleFormLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_questionGenderFormLokator).Displayed,
                "Вопрос формы не отображается");
            Assert.True(driver.FindElement(_questionGenderFormLokator).Text.Contains(_questionGenderFormText),
                $"\tВопрос в прототипе: '{_questionGenderFormText}'\n" +
                $"Фактический вопрос в форме: '{driver.FindElement(_questionGenderFormLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_radioBoyLokator).Displayed,
                "Радио-баттон для мальчика отсутствует");
            Assert.True(driver.FindElement(_labelRadioBoyLokator).Text.Contains(_labelRadioBoyText),
                $"\tВариант в прототипе: '{_labelRadioBoyText}'\n" +
                $"Фактический вариант в форме: '{driver.FindElement(_labelRadioBoyLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_radioGirlLokator).Displayed,
                "Радио-баттон для девочки отсутствует");
            Assert.True(driver.FindElement(_labelRadioGirlLokator).Text.Contains(_labelRadioGirlText),
                $"\tВариант в прототипе: '{_labelRadioGirlLokator}'\n" +
                $"Фактический вариант в форме: '{driver.FindElement(_labelRadioGirlLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_questionMailFormLokator).Displayed,
                "Вопрос запроса емэйл не отображается");
            Assert.True(driver.FindElement(_questionMailFormLokator).Text.Contains(_questionMailFormText),
                $"\tВопрос в прототипе: '{_questionMailFormText}'\n" +
                $"Фактический вопрос в форме: '{driver.FindElement(_questionMailFormLokator).Text}'");
            Assert.IsEmpty(driver.FindElement(_emailInputLokator).Text,
                $"\tОжидалось пустое поле ввода e-mail\n" +
                $"Фактическое значение поля ввода e-mail: '{driver.FindElement(_buttonSendMeLokator).Text}'");
            Assert.True(driver.FindElement(_emailInputLokator).GetAttribute("placeholder").Contains(_placeholderText),
                $"\tТекст подсказки в прототипе: '{_buttonSendMeText}'\n" +
                $"Фактический текст подсказки в поле ввода: '{driver.FindElement(_buttonSendMeLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_buttonSendMeLokator).Displayed,
                "Кнопка 'ПОДОБРАТЬ ИМЯ' отсутствует");
            Assert.True(driver.FindElement(_buttonSendMeLokator).Text.Contains(_buttonSendMeText),
                $"\tТекст кнопки в прототипе: '{_buttonSendMeText}'\n" +
                $"Фактический текст кнопки в форме: '{driver.FindElement(_buttonSendMeLokator).Text}'");
        });
    }

    [Test]
    public void ParrotName_SelectBoy_SendTrueEmailOnClickButton_Success()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_emailTrue);
        driver.FindElement(_radioBoyLokator).Click();
        driver.FindElement(_buttonSendMeLokator).Click();
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(_resultTextLokator).Displayed,
                "Сообщение о отправке имени не отображается");
            Assert.AreEqual(expectedResultGender(Gender.Boy), driver.FindElement(_resultTextLokator).Text,
                $"\tПол питомца не соответствует выбранному\n" +
                $"Ожидается сообщение: '{expectedResultGender(Gender.Boy)}'\n" +
                $"Фактическое сообщение в форме: '{driver.FindElement(_resultTextLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_resultEmailLokator).Displayed,
                "Емэйл не отображается");
            Assert.AreEqual(_emailTrue, driver.FindElement(_resultEmailLokator).Text,
                $"Емэйл для отправки не соответствует введенному\n" +
                $"Емэйл, введенный в поле: '{_emailTrue}'\n" +
                $"Фактически отображаемый емэйл для отправки: '{driver.FindElement(_resultEmailLokator).Text}'");
            Assert.IsFalse(driver.FindElement(_buttonSendMeLokator).Displayed,
                "Кнопка не исчезла");
            Assert.IsTrue(driver.FindElement(_linkAnotherEmailLokator).Displayed,
                "Ссылка 'указать другой e-mail' не отображается");
            Assert.True(driver.FindElement(_linkAnotherEmailLokator).Text.Contains(_linkAnotherEmailText),
                $"\tТекст ссылки не соответствует прототипу\n" +
                $"Текст ссылки в прототипе: '{_linkAnotherEmailText}'\n" +
                $"Фактический текст ссылки в форме: '{driver.FindElement(_linkAnotherEmailLokator).Text}'");
        });
    }

    [Test]
    public void ParrotName_SelectGirl_SendTrueEmailOnClickButton_Success()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_emailTrue);
        driver.FindElement(_radioGirlLokator).Click();
        driver.FindElement(_buttonSendMeLokator).Click();
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(_resultTextLokator).Displayed,
                "Сообщение о отправке имени не отображается");
            Assert.AreEqual(expectedResultGender(Gender.Girl), driver.FindElement(_resultTextLokator).Text,
                $"\tПол питомца не соответствует выбранному\n" +
                $"Ожидается сообщение: '{expectedResultGender(Gender.Girl)}'\n" +
                $"Фактическое сообщение в форме: '{driver.FindElement(_resultTextLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_resultEmailLokator).Displayed,
                "Емэйл не отображается");
            Assert.AreEqual(_emailTrue, driver.FindElement(_resultEmailLokator).Text,
                $"Емэйл для отправки не соответствует введенному\n" +
                $"Емэйл, введенный в поле: '{_emailTrue}'\n" +
                $"Фактически отображаемый емэйл для отправки: '{driver.FindElement(_resultEmailLokator).Text}'");
            Assert.IsFalse(driver.FindElement(_buttonSendMeLokator).Displayed,
                "Кнопка не исчезла");
            Assert.IsTrue(driver.FindElement(_linkAnotherEmailLokator).Displayed,
                "Ссылка 'указать другой e-mail' не отображается");
            Assert.True(driver.FindElement(_linkAnotherEmailLokator).Text.Contains(_linkAnotherEmailText),
                $"\tТекст ссылки не соответствует прототипу\n" +
                $"Текст ссылки в прототипе: '{_linkAnotherEmailText}'\n" +
                $"Фактический текст ссылки в форме: '{driver.FindElement(_linkAnotherEmailLokator).Text}'");
        });
    }

    [Test]
    public void ParrotName_SelectGirl_SendTrueEmailOnClickButton_ClickLinkAnotherEmail_Success()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_emailTrue);
        driver.FindElement(_radioGirlLokator).Click();
        driver.FindElement(_buttonSendMeLokator).Click();
        driver.FindElement(_linkAnotherEmailLokator).Click();
        Assert.Multiple(() =>
        {
            Assert.IsFalse(driver.FindElement(_linkAnotherEmailLokator).Displayed,
                "Ожидалось, что ссылка исчезнет");
            Assert.AreEqual(0, driver.FindElements(_resultTextLokator).Count,
                "Ожидалось, что текст результата отправки исчезнет");
            Assert.AreEqual(0, driver.FindElements(_resultEmailLokator).Count,
                "Ожидалось, что емэйл результата исчезнет");
            Assert.IsEmpty(driver.FindElement(_emailInputLokator).Text,
                $"\tОжидалось, что поле ввода e-mail очистится\n" +
                $"Фактическое значение поля ввода e-mail: '{driver.FindElement(_buttonSendMeLokator).Text}'");
            Assert.True(driver.FindElement(_emailInputLokator).GetAttribute("placeholder").Contains(_placeholderText),
                $"\tОжидался текст подсказки в поле ввода: '{_buttonSendMeText}'\n" +
                $"Фактический текст подсказки в поле ввода: '{driver.FindElement(_buttonSendMeLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_buttonSendMeLokator).Displayed,
                "Кнопка 'ПОДОБРАТЬ ИМЯ' не появилась");
            Assert.True(driver.FindElement(_buttonSendMeLokator).Text.Contains(_buttonSendMeText),
                $"\tТекст кнопки в прототипе: '{_buttonSendMeText}'\n" +
                $"Фактический текст кнопки в форме: '{driver.FindElement(_buttonSendMeLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_radioBoyLokator).Displayed,
                "Радио-баттон для мальчика отсутствует");
            Assert.True(driver.FindElement(_labelRadioBoyLokator).Text.Contains(_labelRadioBoyText),
                $"\tВариант в прототипе: '{_labelRadioBoyText}'\n" +
                $"Фактический вариант в форме: '{driver.FindElement(_labelRadioBoyLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_radioGirlLokator).Displayed,
                "Радио-баттон для девочки отсутствует");
            Assert.True(driver.FindElement(_labelRadioGirlLokator).Text.Contains(_labelRadioGirlText),
                $"\tВариант в прототипе: '{_labelRadioGirlLokator}'\n" +
                $"Фактический вариант в форме: '{driver.FindElement(_labelRadioGirlLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_titleFormLokator).Displayed,
                "Заголовок формы не отображается");
            Assert.AreEqual(_titleFormText, driver.FindElement(_titleFormLokator).Text,
                $"\tЗаголовок формы в прототипе: '{_titleFormText}'\n" +
                $"Фактический заголовок в форме: '{driver.FindElement(_titleFormLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_questionGenderFormLokator).Displayed,
                "Вопрос формы не отображается");
            Assert.True(driver.FindElement(_questionGenderFormLokator).Text.Contains(_questionGenderFormText),
                $"\tВопрос в прототипе: '{_questionGenderFormText}'\n" +
                $"Фактический вопрос в форме: '{driver.FindElement(_questionGenderFormLokator).Text}'");
            Assert.IsTrue(driver.FindElement(_questionMailFormLokator).Displayed,
                "Вопрос запроса емэйл не отображается");
            Assert.True(driver.FindElement(_questionMailFormLokator).Text.Contains(_questionMailFormText),
                $"\tВопрос в прототипе: '{_questionMailFormText}'\n" +
                $"Фактический вопрос в форме: '{driver.FindElement(_questionMailFormLokator).Text}'");

        });
    }

    [Test]
    public void ParrotName_SendEmail3LevelDomain_Success()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_email3LevelDomain);
        driver.FindElement(_buttonSendMeLokator).Click();
        Assert.IsFalse(driver.FindElement(_errorFormLokator).Displayed,
            $"\tОжидалась успешная валидация емэйла домена третьего уровня '{_email3LevelDomain}'\n" +
            $"Фактически ошибка валидации емэйл: '{driver.FindElement(_errorFormLokator).Text}'");
    }

    [Test]
    public void ParrotName_SendEmaiLoginlIsEmpty_Failure()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_emailLoginIsEmpty);
        driver.FindElement(_buttonSendMeLokator).Click();
        Assert.IsTrue(driver.FindElement(_errorFormLokator).Displayed,
            $"\tОжидалась ошибка валидации емэйла без логина '{_emailLoginIsEmpty}'\n");

    }

    [Test]
    public void ParrotName_SendEmaiDomainlIsDot_Failure()
    {
        driver.Navigate().GoToUrl(_urlParrotName);
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(_emailInputLokator)).SendKeys(_emailDomainIsDot);
        driver.FindElement(_buttonSendMeLokator).Click();
        Assert.IsTrue(driver.FindElement(_errorFormLokator).Displayed,
            $"\tОжидалась ошибка валидации емэйла без домена '{_emailDomainIsDot}'\n");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}