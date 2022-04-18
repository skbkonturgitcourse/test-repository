using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace TestNameChoiceSite
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
        }

        private ChromeDriver driver;
        private By emailInputLocator = By.Name("email");
        private By buttonSendMeLocator = By.Id("sendMe");
        private By errMsgLocator = By.ClassName("form-error");
        private By resultMsgForBoyLocator = By.ClassName("result-text");
        private By radiobuttonByGirlLocator = By.Id("girl");
        private By sendAnotherEmailLocator = By.Id("anotherEmail");
        private By resultEmailLocator = By.ClassName("your-email");

        [Test]
        public void GeneralPageIsView()
        {
            Assert.True(driver.FindElement(buttonSendMeLocator).Displayed, "Главная станица не отображается");
        }
        
        [Test]
        public void EmailValidation()
        {
            var errMsgReference = "Некорректный email";
            var emailAddress = "test1@localhost";
            driver.FindElement(emailInputLocator).SendKeys(emailAddress);
            driver.FindElement(buttonSendMeLocator).Click();
            var errMsg = driver.FindElement(errMsgLocator);
            Assert.Multiple(() =>
            {
                Assert.True(errMsg.Displayed, "Валидация email не отработала");
                Assert.AreEqual(errMsgReference, errMsg.Text, "Валидационное сообщение для email не соответствует эталону");
            });
            
        }

        [Test]
        public void MsgForBoy()
        {
            var resultMsgReference = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            var emailAddress = "test1@localhost.ru";
            driver.FindElement(emailInputLocator).SendKeys(emailAddress);
            driver.FindElement(buttonSendMeLocator).Click();
            var resultMsg = driver.FindElement(resultMsgForBoyLocator);
            Assert.Multiple(() =>
            {
                Assert.True(resultMsg.Displayed, "Сообщение для девочки не отправлено");
                Assert.AreEqual(resultMsgReference, resultMsg.Text, "Сообщение для девочки не соответствует эталону");
            });
        }

        [Test]
        public void MsgForGirl()
        {
            var resultMsgReference = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            var emailAddress = "test1@localhost.ru";
            driver.FindElement(radiobuttonByGirlLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(emailAddress);
            driver.FindElement(buttonSendMeLocator).Click();
            var resultMsg = driver.FindElement(resultMsgForBoyLocator);
            Assert.Multiple(() =>
                { 
                    Assert.True(resultMsg.Displayed, "Сообщение для мальчика не отправлено");
                    Assert.AreEqual(resultMsgReference, resultMsg.Text, "Со общение для мальчика не соответствует эталону");
                });
        }
        
        [Test]
        public void checkSendEmail()
        {
            var emailAddress = "test1@localhost.ru";
            driver.FindElement(emailInputLocator).SendKeys(emailAddress);
            driver.FindElement(buttonSendMeLocator).Click();
            var resultEmail = driver.FindElement(resultEmailLocator);
            Assert.Multiple(() =>
            {
                Assert.True(resultEmail.Displayed, "Email на который отправлено имя не отображается");
                Assert.AreEqual(emailAddress, resultEmail.Text, "Email на который отправлено имя не совпадает с введенным");
            });
        }
        
        [Test]
        public void SendAnotherEmail()
        {
            var sendAnotherEmailText= "указать другой e-mail";
            var emailAddress = "test1@localhost.ru";
            driver.FindElement(emailInputLocator).SendKeys(emailAddress);
            driver.FindElement(buttonSendMeLocator).Click();
            Assert.True(driver.FindElement(sendAnotherEmailLocator).Displayed, "Ссылка для смены email отсутствует");
            driver.FindElement(sendAnotherEmailLocator).Click();
            Assert.True(driver.FindElement(buttonSendMeLocator).Displayed, "Попытка смены email завершилась неудачей");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}