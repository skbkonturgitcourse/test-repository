using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HomeWorkParrot;

public class HomeWorkParrot
{
        private ChromeDriver driver;
        private string urlFixParrot = "https://qa-course.kontur.host/selenium-practice/";
        private By radioButtonGirlEnter = By.Id("girl");
        private By radioButtonBoyEnter = By.Id("boy");
        private By emailInputLokator = By.Name("email");
        private By buttonSendMeLokator = By.Id("sendMe");
        private By resultTextLokator = By.ClassName("result-text");
        private By resultEmailLokator = By.ClassName("your-email");
        private By resulTextErrorLokator = By.ClassName("form-error");
        private By linkAnotherEmailLocator = By.LinkText("указать другой e-mail");

        [SetUp]
        public void SetUp()
        { 
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }
        
        [Test]
        public void FixParrot_SendGenderGirlAndEmail_Success()
        {
            var expectedResultTextForGirl = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            var email = "parrot@mail.ru";
            
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonGirlEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успешной подаче заявки на имя попугая отображается");
            Assert.AreEqual(expectedResultTextForGirl, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успешном создании заявки");
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный емейл на который будем отвечать");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }
    
        [Test]
        public void FixParrot_SendGenderBoyAndEmail_Success()
        {
            var expectedResultTextForBoy = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            var email = "parrot@mail.ru";
            
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonBoyEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успешной подачи заявки на имя попугая отображается");
            Assert.AreEqual(expectedResultTextForBoy, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успешном создании заявки");
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный емейл на который будем отвечать");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }
        
        [Test] 
        public void FixParrot_ClickLinkAnotherIfGenderGirl_Success()
        {
            var email = "parrot@mail.ru";
            
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonGirlEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            
            Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                "Ожидание, что поле для ввода e-mail очистится");
            Assert.IsTrue(driver.FindElement(buttonSendMeLokator).Displayed,
                "Ожидание, что кнопка 'Подобрать имя' отобразится");
            Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLocator).Count);
        }
    
        [Test] 
        public void FixParrot_ClickLinkAnotherIfGenderBoy_Success()
        {
            var email = "parrot@mail.ru";
            
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonBoyEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            
            Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                "Ожидание, что поле для ввода e-mail очистится");
            Assert.IsTrue(driver.FindElement(buttonSendMeLokator).Displayed,
                "Ожидание, что кнопка 'Подобрать имя' отобразится");
            Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLocator).Count);
        }
        
        [Test] 
        public void FixParrot_SendEmptyEmail_Success()
        {
            var expectedResultTextEnterEmail = "Введите email";
            
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonBoyEnter).Click();
            driver.FindElement(buttonSendMeLokator).Click();
            
            Assert.AreEqual(expectedResultTextEnterEmail, driver.FindElement(resulTextErrorLokator).Text,
                "При отправке пустого e-email, сообщение об ошибке");
        }
        
        [Test] 
        public void FixParrot_SendEmailAsAMask_Error()
        {
            var expectedResultTextForEmailAsAMask = "Некорректный email";
            var email = "@.";
        
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonGirlEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            
            Assert.AreEqual(expectedResultTextForEmailAsAMask, driver.FindElement(resulTextErrorLokator).Text,
                "При отправке некорректного e-mail нет сообщения об ошибке");
        }

        [Test] 
        public void FixParrot_RadioButtonAlwaysIsBoy_Error()
        {
            var expectedResultRadioButtonAlwaysBoy = "RadioButton на мальчике при подаче новой заявки";
            var email = "parrot@mail.ru";
        
            driver.Navigate().GoToUrl(urlFixParrot);
            driver.FindElement(radioButtonGirlEnter).Click();
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            
            Assert.AreEqual(expectedResultRadioButtonAlwaysBoy, driver.FindElement(radioButtonGirlEnter).Text,
                "При подаче новой заявки, RadioButton не возвращается на мальчика");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
}
