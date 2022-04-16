using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class HomeworkSeleniumTests
{
    //выбрать пол попугайчика (мальчик или девочка)
    //ввести почту
    //нажать "подобрать имя"
    //нажать "указать другой e-mail"
    
    private ChromeDriver driver;
    private By girlParrotLokator = By.Id("girl");
    private By emailInputLokator = By.Name("email");
    private By emailErrorInputLokator = By.ClassName("error");
    private By buttonPickUpNameLokator = By.Id("sendMe");
    private By resultTextLokator = By.ClassName("result-text");
    private By errorTextLokator = By.ClassName("form-error");
    private By resultEmailLokator = By.ClassName("your-email");
    private By linkAnotherEmailLokator = By.Id("anotherEmail");
    private string email = "test@test.ru";
    private string emptyEmailErrorText = "Введите email";
    private string incorrectEmailErrorText = "Некорректный email";
    private string urlNameParrot = "https://qa-course.kontur.host/selenium-practice/";
    
    [SetUp]
    public void SetUp()
    {
        //открываем браузер, разворачиваем его на полный экран
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }
    
    [Test]
    public void NameParrot_ForBoy_Success()
    {
        driver.Navigate().GoToUrl(urlNameParrot);
        driver.FindElement(emailInputLokator).SendKeys(email);
        driver.FindElement(buttonPickUpNameLokator).Click();

        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";

        Assert.Multiple(() => 
            { 
                Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                     "Сообщение об успехе создания заявки отображается");
                Assert.AreEqual(expectedResultText,driver.FindElement(resultTextLokator).Text,
                     "Итоговый текст отличается от ожидаемого");
                Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                     "Неверный емейл на который будем присылать ответ");
                Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                     "Ссылка 'Указать другой e-mail' отображается");
            
            }
            );
    }
    
    [Test]
    public void NameParrot_ForGirl_Success()
    {
        driver.Navigate().GoToUrl(urlNameParrot);
        driver.FindElement(girlParrotLokator).Click();
        driver.FindElement(emailInputLokator).SendKeys(email);
        driver.FindElement(buttonPickUpNameLokator).Click();

        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
       
        Assert.Multiple(() => 
            {
                Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                     "Сообщение об успехе создания заявки отображается");
                Assert.AreEqual(expectedResultText,driver.FindElement(resultTextLokator).Text,
                     "Итоговый текст отличается от ожидаемого");
                Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                     "Неверный емейл на который будем присылать ответ");
                Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                     "Ссылка 'Указать другой e-mail' отображается");
            }
            );
       
    }
    
    [Test]
    public void NameParrot_ClickLinkAnotherEmail_Success()
    {
        driver.Navigate().GoToUrl(urlNameParrot);
        driver.FindElement(emailInputLokator).SendKeys(email);
        driver.FindElement(buttonPickUpNameLokator).Click();
        driver.FindElement(linkAnotherEmailLokator).Click();
        
        Assert.Multiple(() => 
            {
                Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                    "Ожидали что поле для ввода емейла очистится");
                Assert.IsTrue(driver.FindElement(buttonPickUpNameLokator).Displayed,
                    "Ожидали, что кнопка 'Напишите мне' отображается");
                Assert.IsFalse(driver.FindElement(linkAnotherEmailLokator).Displayed,
                    "Ожидали, что ссылка 'Указать другой email' не отображается"); 
            }
            );
    }
    
    [Test]
    public void NameParrot_EmptyEmail_Error()
    {
        driver.Navigate().GoToUrl(urlNameParrot);
        driver.FindElement(buttonPickUpNameLokator).Click();
        
        Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(emailErrorInputLokator).Displayed,
                    "Ожидали, что поле ввода email стало красным");
                Assert.IsTrue(driver.FindElement(errorTextLokator).Displayed,
                    "Ожидали, что сообщение об ошибке отображается");
                Assert.AreEqual(emptyEmailErrorText, driver.FindElement(errorTextLokator).Text,
                    "Ожидали, что сообщение об ошибке: 'Введите email'");
            }
            );
        
    }
    
    [Test]
    public void NameParrot_IncorrectEmail_Error()
    {
        driver.Navigate().GoToUrl(urlNameParrot);
        driver.FindElement(emailInputLokator).SendKeys("1234567890");
        driver.FindElement(buttonPickUpNameLokator).Click();
        
        Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(emailErrorInputLokator).Displayed,
                    "Ожидали, что поле ввода email стало красным");
                Assert.IsTrue(driver.FindElement(errorTextLokator).Displayed,
                    "Ожидали, что сообщение об ошибке отображается");
                Assert.AreEqual(incorrectEmailErrorText, driver.FindElement(errorTextLokator).Text,
                    "Ожидали, что сообщение об ошибке: 'Некорректный email'");
            }
            );
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

}