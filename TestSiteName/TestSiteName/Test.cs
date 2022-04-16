using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSiteName;

public class SiteTests
{
    private ChromeDriver _driver;

    private string _siteUrl = "https://qa-course.kontur.host/selenium-practice/";

    private By _buttonBoyLocator = By.Id("boy");
    private By _buttonGirlLocator = By.Id("girl");
    private By _inputEmailLocator = By.Name("email");
    private By _buttonSendMeLocator = By.Id("sendMe");
    private By _resultTextLocator = By.ClassName("result-text");
    private By _yourEmailLocator = By.ClassName("your-email");
    private By _linkAnotherEmailLocator = By.Id("anotherEmail");
    private By _formError = By.ClassName("form-error");

    private string _expectedEmail = "test@mail.ru";
    private string _incorrectEmail = "test@@@@mail.ru";
    private string _expectedBoyResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
    private string _expectedGirlResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
    private string _expectedIncorrectEmailError = "Некорректный email";
    private string _expectedMissedEmailError = "Введите email";

    private string _msgRequestSuccessNotDisplayed = "Сообщение об успехе создания запроса имени не отображается.";
    private string _msgRequestSuccessIncorrect = "Неверное сообщение об успехе запроса имени";
    private string _msgYourEmailIncorrect = "Неверный емейл на кторый будем отвечать.";
    private string _msgAnotherEmailNotDisplayed = "Ссылка 'Указать другой e-mail' не отображается.";
    private string _msgAnotherEmailDisplayed = "Ссылка 'Указать другой e-mail' отображается.";
    private string _msgButtonChoiseNameNotDisplayed = "Кнопка 'ПОДОБРАТЬ ИМЯ' не отображается.";
    private string _msgInputEmailNotEmpty = "Поле для ввода е-мейла не очистилось.";
    private string _msgErrorNotDisplayed = "Сообщение об ошибке не отображается.";
    private string _msgErrorIncorrectEmailText = "Ожидалось другое сообщение об ошибке при некорректном e-mail";
    private string _msgErrorEmptytEmailText = "Ожидалось другое сообщение об ошибке при незаполненном e-mail";

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        _driver = new ChromeDriver(options);
    }

    [Test]
    public void ShouldSendEmailSuccessIfSelectedBoyName()
    {
        FillFormData(url: _siteUrl, locator: _buttonBoyLocator, email: _expectedEmail);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(_driver.FindElement(_resultTextLocator).Displayed, _msgRequestSuccessNotDisplayed);
            Assert.AreEqual(_expectedBoyResultText, _driver.FindElement(_resultTextLocator).Text,
                _msgRequestSuccessIncorrect);
            Assert.AreEqual(_expectedEmail, _driver.FindElement(_yourEmailLocator).Text, _msgYourEmailIncorrect);
            Assert.IsTrue(_driver.FindElement(_linkAnotherEmailLocator).Displayed, _msgAnotherEmailNotDisplayed);
        });
    }

    [Test]
    public void ShouldSendEmailSuccessIfSelectedGirlName()
    {
        FillFormData(url: _siteUrl, locator: _buttonGirlLocator, email: _expectedEmail);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(_driver.FindElement(_resultTextLocator).Displayed, _msgRequestSuccessNotDisplayed);
            Assert.AreEqual(_expectedGirlResultText, _driver.FindElement(_resultTextLocator).Text,
                _msgRequestSuccessIncorrect);
            Assert.AreEqual(_expectedEmail, _driver.FindElement(_yourEmailLocator).Text, _msgYourEmailIncorrect);
            Assert.IsTrue(_driver.FindElement(_linkAnotherEmailLocator).Displayed, _msgAnotherEmailNotDisplayed);
        });
    }

    [Test]
    public void ShouldBeDisplayedEmailInputAfterAnotherEmailLClick()
    {
        FillFormData(url: _siteUrl, locator: _buttonBoyLocator, email: _expectedEmail);

        _driver.FindElement(_linkAnotherEmailLocator).Click();

        Assert.Multiple(() =>
        {
            Assert.IsEmpty(_driver.FindElement(_inputEmailLocator).Text, _msgInputEmailNotEmpty);
            Assert.IsTrue(_driver.FindElement(_buttonSendMeLocator).Displayed, _msgButtonChoiseNameNotDisplayed);
            Assert.IsFalse(_driver.FindElement(_linkAnotherEmailLocator).Displayed, _msgAnotherEmailDisplayed);
        });
    }

    [Test]
    public void ShouldBeDisplayedErrorForIncorrectEmail()
    {
        FillFormData(url: _siteUrl, locator: _buttonBoyLocator, email: _incorrectEmail);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(_driver.FindElement(_formError).Displayed, _msgErrorNotDisplayed);
            Assert.AreEqual(_expectedIncorrectEmailError, _driver.FindElement(_formError).Text,
                _msgErrorIncorrectEmailText);
        });
    }

    [Test]
    public void ShouldBeDisplayedErrorForEmptyEmail()
    {
        FillFormData(url: _siteUrl, locator: _buttonBoyLocator);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(_driver.FindElement(_formError).Displayed, _msgErrorNotDisplayed);
            Assert.AreEqual(_expectedMissedEmailError, _driver.FindElement(_formError).Text,
                _msgErrorEmptytEmailText);
        });
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }

    private void FillFormData(string url, By locator, string email = "")
    {
        _driver.Navigate().GoToUrl(url);

        _driver.FindElement(locator).Click();
        _driver.FindElement(_inputEmailLocator).SendKeys(email);
        _driver.FindElement(_buttonSendMeLocator).Click();
    }
}