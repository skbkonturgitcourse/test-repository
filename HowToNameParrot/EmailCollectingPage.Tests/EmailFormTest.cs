using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace NameParrot.Tests;

public class EmailFormTest
{
    private ChromeDriver driver;
    private const string url = "https://qa-course.kontur.host/selenium-practice/";
    private By emailInputLocator = By.XPath(".//*[@id = 'form']/input");
    private By emailButtonLocator = By.Id("sendMe");
    private By emailRadioMaleLocator = By.Id("boy");
    private By emailRadioFemaleLocator = By.Id("girl");
    private By radioMaleTextLocator = By.XPath(".//*[@id = 'form']//label[1]");
    private By radioFemaleTextLocator = By.XPath(".//*[@id = 'form']//label[2]");
    private By emailValidationLocator = By.XPath(".//*[@id = 'form']/pre");
    private By successTextLocator = By.XPath(".//*[@id = 'resultTextBlock']/div[@class='result-text']"); 
    private By successEmailLocator = By.XPath(".//*[@id = 'resultTextBlock']/pre");
    private By useAnotherEmailLinkLocator = By.XPath(".//*[@id = 'resultTextBlock']/following-sibling::a");
    
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(GetProjectDirectory(), options);
        driver.Navigate().GoToUrl(url);
    }

    [Test]
    public void EmailCollectingPage_MaleSelectedByDefault_IsTrue() =>
        Assert.IsTrue(driver.FindElement(emailRadioMaleLocator).Selected);

    [Test]
    public void EmailCollectingPage_SwitchRadio_Success()
    {
        driver.FindElement(emailRadioFemaleLocator).Click();
        Assert.True(driver.FindElement(emailRadioFemaleLocator).Selected,
            "Не удалось выбрать женский пол"
            );
        driver.FindElement(emailRadioMaleLocator).Click();
        Assert.True(driver.FindElement(emailRadioMaleLocator).Selected,
            "Не удалось переключить радиокнопку обратно с мужского пола на женский"
            );
    }
    
    [Test]
    public void EmailCollectingPage_DoubleSelectRadio_SelectStays()
    {
        new Actions(driver)
            .DoubleClick(driver.FindElement(emailRadioFemaleLocator))
            .Build()
            .Perform();
        Assert.IsTrue(driver.FindElement(emailRadioFemaleLocator).Selected);
    }
    
    [Test]
    public void EmailCollectingPage_SelectRadioByLabel_Success()
    {
        driver.FindElement(radioFemaleTextLocator).Click();
        Assert.IsTrue(driver.FindElement(emailRadioFemaleLocator).Selected);
    }
    
    [TestCase("", false)]
    [TestCase("test.ru", false)]
    [TestCase("тест@тест.рф", true)]
    [TestCase("test@test.ru", true)]
    public void EmailCollectingPage_SendEmail_SuccessIfEmailIsValid(string email, bool emailIsValid)
    {
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(emailButtonLocator).Click();
        Assert.AreEqual(driver.FindElement(emailValidationLocator).Displayed, !emailIsValid,
            $"Валидация почты {new string(emailIsValid ? "зря" : "не")} сработала"
            );
        var successElements = driver.FindElements(successEmailLocator);
        Assert.AreEqual(successElements.Count, (emailIsValid) ? 1 : 0, 
            $"{new string(emailIsValid ? "Отсутствует" : "Зря появилось")} подтверждение отправки формы"
            );
        if (!emailIsValid) return;
        Assert.IsTrue(successElements[0].Text.Contains(email), 
            $"После отправки формы в тексте подтверждения не указан введённый email: {email}"
            );
    }

    [TestCase(true)]
    [TestCase(false)]
    public void EmailCollectingPage_SendEmail_ResultTextContainsSelectedSex(bool maleIsSelected)
    {
        if (!maleIsSelected)
        {
            driver.FindElement(emailRadioFemaleLocator).Click();
        }
        driver.FindElement(emailInputLocator).SendKeys("test@test.ru");
        driver.FindElement(emailButtonLocator).Click();
        var parrotSex = (maleIsSelected) ? "мальчик" : "девочк";
        Assert.IsTrue(driver.FindElement(successTextLocator).Text.Contains(parrotSex),
            $"Выбранный пол: {parrotSex}.\n Текст после отправки формы: {driver.FindElement(successTextLocator).Text}"
            );
    }

    [Test]
    public void EmailCollectingPage_UseAnotherEmailLink_EmptyForm()
    {
        driver.FindElement(emailInputLocator).SendKeys("test@test.ru");
        driver.FindElement(emailButtonLocator).Click();
        driver.FindElement(useAnotherEmailLinkLocator).Click();
        Assert.Multiple(() =>
        {
            Assert.IsEmpty(driver.FindElements(useAnotherEmailLinkLocator), 
                "На странице осталась ссылка Указать другой email");
            Assert.IsEmpty(driver.FindElement(emailInputLocator).Text,
                "Поле для ввода email не очистилось");
        });
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
    
    private string GetProjectDirectory() =>
        Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
}