using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSiteName;

public class SiteTests
{
    private ChromeDriver driver;

    private string siteUrl = "https://qa-course.kontur.host/selenium-practice/";

    private By buttonBoyLokator = By.Id("boy");
    private By buttonGirlLokator = By.Id("girl");
    private By inputEmailLokator = By.Name("email");
    private By buttonSendMeLokator = By.Id("sendMe");
    private By resultTextLokator = By.ClassName("result-text");
    private By yourEmailLokator = By.ClassName("your-email");
    private By linkAnotherEmailLokator = By.Id("anotherEmail");

    private string expectedEmail = "test@mail.ru";
    private string expectedBoyResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
    private string expectedGirlResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";

    private string msgRequestSuccessNotDisplayed = "Сообщение об успехе создания заявки не отображается";
    private string msgRequestSuccessIncorrect = "Неверное сообщение об успехе создания заявки";
    private string msgYourEmailIncorrect = "Неверный емейл на кторый будем отвечать";
    private string msgAnotherEmailNotDisplayed = "Ссылка 'Указать другой e-mail' отображается";

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }

    [Test]
    public void SiteName_BoyName_SendEmail_Success()
    {
        driver.Navigate().GoToUrl(siteUrl);
        driver.FindElement(buttonBoyLokator).Click();
        driver.FindElement(inputEmailLokator).SendKeys(expectedEmail);
        driver.FindElement(buttonSendMeLokator).Click();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, msgRequestSuccessNotDisplayed);
            Assert.AreEqual(expectedBoyResultText, driver.FindElement(resultTextLokator).Text,
                msgRequestSuccessIncorrect);
            Assert.AreEqual(expectedEmail, driver.FindElement(yourEmailLokator).Text, msgYourEmailIncorrect);
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed, msgAnotherEmailNotDisplayed);
        });
    }

    [Test]
    public void SiteName_GirlName_SendEmail_Success()
    {
        driver.Navigate().GoToUrl(siteUrl);
        driver.FindElement(buttonGirlLokator).Click();
        driver.FindElement(inputEmailLokator).SendKeys(expectedEmail);
        driver.FindElement(buttonSendMeLokator).Click();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, msgRequestSuccessNotDisplayed);
            Assert.AreEqual(expectedGirlResultText, driver.FindElement(resultTextLokator).Text,
                msgRequestSuccessIncorrect);
            Assert.AreEqual(expectedEmail, driver.FindElement(yourEmailLokator).Text, msgYourEmailIncorrect);
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed, msgAnotherEmailNotDisplayed);
        });
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}