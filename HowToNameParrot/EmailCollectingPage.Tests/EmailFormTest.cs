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
    
    [TestCase("тест@тест.рф")]
    [TestCase("test@test.ru")]
    public void EmailCollectingPage_SendEmail_Success(string email)
    {
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(emailButtonLocator).Click();
        Assert.Multiple(() =>
        {
            Assert.IsTrue(!driver.FindElement(emailValidationLocator).Displayed, 
                $"Валидация сработала на валидную почту: {email}");
            Assert.That(driver.FindElements(successTextLocator).Count, Is.EqualTo(1));
            Assert.That(driver.FindElements(successEmailLocator).Count, Is.EqualTo(1));
            Assert.IsTrue(driver.FindElements(successEmailLocator)[0].Text.Contains(email),
                $"В подтверждении отправки формы не указан введённый email: {email}"
            );
        });
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase("test.ru")]
    [TestCase("test@test.")]
    [TestCase("@test.ru")]
    public void EmailCollectingPage_SendWrongEmail_Validation(string email)
    {
        driver.FindElement(emailInputLocator).SendKeys(email);
        driver.FindElement(emailButtonLocator).Click();
        var validationText = (email == "" ? "Введите email" : "Некорректный email");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(emailValidationLocator).Displayed,
                $"Валидация не сработала на невалидную почту: {email}");
            Assert.That(driver.FindElement(emailValidationLocator).Text, Is.EqualTo(validationText), 
                "Текст валидации не соответствует прототипам");
        });
    }

    [TestCase(true)]
    [TestCase(false)]
    public void EmailCollectingPage_SendEmail_ResultTextIsRight(bool maleIsSelected)
    {
        if (!maleIsSelected)
        {
            driver.FindElement(emailRadioFemaleLocator).Click();
        }
        driver.FindElement(emailInputLocator).SendKeys("test@test.ru");
        driver.FindElement(emailButtonLocator).Click();
        var parrotSex = (maleIsSelected) ? "мальчик" : "девочк";
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(successTextLocator).Text.Contains(parrotSex),
                $"Выбранный пол: {parrotSex}.\n Текст после отправки формы: {driver.FindElement(successTextLocator).Text}"
            );
            Assert.That(driver.FindElement(successTextLocator).Text, 
                Is.EqualTo($"Хорошо, мы пришлём имя для " +
                $"{new string(maleIsSelected ? "вашего мальчика" : "вашей девочки")} " +
                $"на e-mail:"),
                "Текст подтверждения отправки формы не соответствует прототипам");
        });
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