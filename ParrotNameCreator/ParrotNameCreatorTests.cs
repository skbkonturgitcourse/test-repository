using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParrotNameCreator;

public class ParrotNameCreatorTests
{
    private ChromeDriver driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }
    
    private string email = "test@mail.ru";
    private string urlNameCreator = "https://qa-course.kontur.host/selenium-practice/";
    
    private By boySelectLocator = By.Id("boy");
    private By girlSelectLocator = By.Id("girl");
    private By emailInputLocator = By.Name("email");
    private By submitButtonLocator = By.Id("sendMe");
    private By resultTextLocator = By.ClassName("result-text");
    private By resultEmailLocator = By.ClassName("your-email");
    private By formErrorLocator = By.ClassName("form-error");
    private By anotherEmailLinkLocator = By.Id("anotherEmail");
    
    private string incorrectEmailErrorMessage = "Некорректный email";
    private string emptyEmailErrorMessage = "Введите email";
    private string expectedResultTextForBoy = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
    private string expectedResultTextForGirl = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
    private string anotherEmailLinkText = "указать другой e-mail";

    [Test]
    public void NameCreator_CheckDefaultGender_Boy()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        Assert.Multiple(() =>
        {
            Assert.True(driver.FindElement(boySelectLocator).Selected, "Пол по умолчанию выбран неправильно");
            Assert.False(driver.FindElement(girlSelectLocator).Selected, "Пол по умолчанию выбран неправильно");
        });
    }
    
    [Test]
    public void NameCreator_CreateNameForBoy_Success()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        driver.FindElement(boySelectLocator).Click();
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(submitButtonLocator).Click();
        Assert.AreEqual(driver.FindElement(resultTextLocator).Text, expectedResultTextForBoy, "Имя создается не для мальчика." );
    }

    [Test]
    public void NameCreator_CreateNameForGirl_Success()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        driver.FindElement(girlSelectLocator).Click();
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(submitButtonLocator).Click();
        Assert.AreEqual(driver.FindElement(resultTextLocator).Text, expectedResultTextForGirl, "Имя создается не для девочки." );
    }
    
    [Test]
    public void NameCreator_CheckInputEmail_Success()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(submitButtonLocator).Click();
        Assert.AreEqual(driver.FindElement(resultEmailLocator).Text, email, "Отображаемый e-mail не совпадает с введённым." );
    }
    
    [Test]
    public void NameCreator_InputIncorrectEmail_Error()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        var wrongEmail = "privet";
        driver.FindElement(emailInputLocator).SendKeys(wrongEmail);
        driver.FindElement(submitButtonLocator).Click();
        Assert.True(driver.FindElement(formErrorLocator).Displayed, "Отсутствует валидация поля e-mail.");
        Assert.AreEqual(driver.FindElement(formErrorLocator).Text, incorrectEmailErrorMessage, $"Некорректный текст ошибки. " +
            $"\nДолжно быть: '{incorrectEmailErrorMessage}'");
    }
    
    [Test]
    public void NameCreator_EmptyEmailField_Error()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        driver.FindElement(submitButtonLocator).Click();
        Assert.True(driver.FindElement(formErrorLocator).Displayed, "Отсутствует валидация поля e-mail.");
        Assert.AreEqual(driver.FindElement(formErrorLocator).Text, emptyEmailErrorMessage, $"Некорректный текст ошибки. " +
            $"\nДолжно быть: '{emptyEmailErrorMessage}'");
    }
    
    [Test]
    public void NameCreator_CheckAnotherEmailInputLink_Success()
    {
        driver.Navigate().GoToUrl(urlNameCreator);
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(submitButtonLocator).Click();
        Assert.Multiple(() =>
        {
            Assert.True(driver.FindElement(anotherEmailLinkLocator).Displayed, "Отсутствует ссылка 'указать другой e-mail'");
            Assert.AreEqual(driver.FindElement(anotherEmailLinkLocator).Text, anotherEmailLinkText,
                "Некорректный текст ссылки" +
                $"\nДолжно быть: '{anotherEmailLinkText}'");
        });
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}