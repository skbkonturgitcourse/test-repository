using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPracticeTests
{
    public class FirstPracticeTest
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        private ChromeDriver driver;
        private readonly By sarchInputLocator = By.Name("search");

        private By emailinputlokator = By.Name("email");
        private By ButtonSendMeLocator = By.Id("sendMe");
        private By resultTextLocator = By.ClassName("result-text");
        private By resultEmailLocator = By.ClassName("your-email");
        private By linkAnotherEmailLocator = By.Id("anotherEmail");
        private string UrlBoyName = "https://qa-course.kontur.host/selenium-practice/";

        [Test]
        public void nameboy_SendEmail_Success()
        {
            
            var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            driver.Navigate().GoToUrl(UrlBoyName);
            var email = "hiyono4719@arpizol.com";
            driver.FindElement(emailinputlokator).SendKeys(email);
            driver.FindElement(ButtonSendMeLocator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed,
                "Сообщение об успехе заявки не отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,
                "Неверное сообщение об успехе создание заявки");
            Assert.AreEqual(email, driver.FindElement(resultEmailLocator).Text,
                "Неверный email на который не будем отвечать");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed,
                "Ссылка 'указать другой e-mail' не отображается");
        }
        [Test]
        public void NameBoy_ClickLinkAnotherEmail_Success()
        {
                var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
                driver.Navigate().GoToUrl(UrlBoyName);
                var email = "hiyono4719@arpizol.com";
                driver.FindElement(emailinputlokator).SendKeys(email);
                driver.FindElement(ButtonSendMeLocator).Click();
                driver.FindElement(linkAnotherEmailLocator).Click();
                Assert.IsEmpty(driver.FindElement(emailinputlokator).Text, "Ожидали что поле для email очистится");
                Assert.IsTrue(driver.FindElement(ButtonSendMeLocator).Displayed, "Кнопка 'Подобрать имя' не отображается");
                Assert.IsFalse(driver.FindElement(linkAnotherEmailLocator).Displayed, "Ожидали что ссылка 'Указать другой email' не отображается");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }

    
    
}