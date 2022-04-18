using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SelenumTestsHome
{
    public class SeleniumHomework
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        private ChromeDriver driver;
        private By radiobuttonMale = By.Id("boy");
        private By radiobuttonFemale = By.Id("girl");
        private By emailInputLocator = By.Name("email");
        private By buttonPickUpName = By.Id("sendMe");
        private By resultTextLocator = By.ClassName("result-text");
        private By resultEmailLocator = By.ClassName("your-email");
        private By linkAnotherEmailLocator = By.LinkText("указать другой e-mail");
        private By formErrorLocator = By.ClassName("form-error");
        private string urlParrotName = "https://qa-course.kontur.host/selenium-practice/";
        private string emailCorrect = "test@test.ru";
        private string emailIncorrect = "test";
        private string emailWithoutName = "@test.ru";
        private string emailWithoutDomainName = "test@.ru";


        [Test] //№1
        public void ParrotName_ForBoy_SendCorrectEmail_Success()
        {
            var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";

            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(radiobuttonMale).Click();
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(buttonPickUpName).Click();
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed,
                "Сообщение об успешной отправке заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,
                "Некорректное сообщение о создании заявки");
            Assert.AreEqual(emailCorrect, driver.FindElement(resultEmailLocator).Text,
                "Email не совпадает с ранее введенным");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed,
                "Ссылка 'указать другой e-mail' отображается");
        });
        }


        [Test] //№2
        public void ParrotName_ForGirl_SendCorrectEmail_Success()
        {
            var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(radiobuttonFemale).Click();
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(buttonPickUpName).Click();
           
            Assert.Multiple(() =>
                {
            Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed,
                "Сообщение об успешной отправке заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,
                "Некорректное сообщение о создании заявки");
            Assert.AreEqual(emailCorrect, driver.FindElement(resultEmailLocator).Text,
                "Email не совпадает с ранее введенным");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed,
                "Ссылка 'указать другой e-mail' отображается");
                });

        }

        [Test] //№3
        public void ParrotName_SendEmptyInput_Error()
        {
            var expectedResultText = "Введите email";
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(buttonPickUpName).Click();
            Assert.AreEqual(expectedResultText, driver.FindElement(formErrorLocator).Text,
                "Отображается сообщение о необходимости ввода email ");
        }

        [Test] //№4
        public void ParrotName_SendIncorrectEmail_Error()
        {
            var expectedResultText = "Некорректный email";
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(emailInputLocator).SendKeys(emailIncorrect);
            driver.FindElement(buttonPickUpName).Click();
            
            Assert.Multiple(() =>
                {
            Assert.IsTrue(driver.FindElement(formErrorLocator).Displayed,
                "Сообщение о некорректном вводе Email отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(formErrorLocator).Text,
                "Отображается сообщение о некорректности введеного email ");
                });

        }

        [Test] //№5
        public void ParrotName_ClickLinkAnotherEmail_Success()
        {
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(buttonPickUpName).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            Assert.IsEmpty(driver.FindElement(emailInputLocator).Text,
                "Поле для ввода Email очиищается от введеных ранее данных");
            Assert.IsTrue(driver.FindElement(buttonPickUpName).Displayed,
                "Кнопка 'Подобрать имя' отображается");
            Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLocator).Count);
        }

        [Test] //№6
        public void ParrotName_SendEmailWithoutName_Error()
        {
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(emailInputLocator).SendKeys(emailWithoutName);
            driver.FindElement(buttonPickUpName).Click();
            Assert.IsTrue(driver.FindElement(formErrorLocator).Displayed,
                "Сообщение о некорректном вводе Email отображается");

        }

        [Test] //№7
        public void ParrotName_SendEmailWithoutDomainName_Error()
        {
            driver.Navigate().GoToUrl(urlParrotName);
            driver.FindElement(emailInputLocator).SendKeys(emailWithoutDomainName);
            driver.FindElement(buttonPickUpName).Click();
            Assert.IsTrue(driver.FindElement(formErrorLocator).Displayed,
                "Сообщение о некорректном вводе Email отображается");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}