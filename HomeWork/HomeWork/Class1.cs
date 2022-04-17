using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HomeWork
{
    public class AutoTestsHomeWork
    {

        [SetUp]
        public void SetUpOpenChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options); 
        }

        [SetUp]
        public void SetupGoToUrl()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
        }
        private By RadioButtonBoyLokator = By.Id("boy");
        private ChromeDriver driver;
        private By EmailInputLokator = By.Name("email");
        private By ButtonSendMeLokator = By.Id("sendMe");
        private By ResultTextLokator = By.ClassName("result-text");
        private By YourEmailLokator = By.ClassName("your-email");
        private By LinkAnotherEmailLokator = By.Id("anotherEmail");
        private By RadioButtonGirlLokator = By.Id("girl");
        private By ErrorEmailInputLokator = By.ClassName("error");
        private By EmailInputErrorMessageLokator = By.ClassName("form-error");


        [Test]
        public void NameForParrotChoice_SendEmailForBoy_Success()
        {
            driver.FindElement(RadioButtonBoyLokator).Click();

            var email = "123abc@mail.ru";
            driver.FindElement(EmailInputLokator).SendKeys(text: email);

            driver.FindElement(ButtonSendMeLokator).Click();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(ResultTextLokator).Displayed,
                    "Текст об успехе заявки на имя не отображается");

                var resulttextBoy = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
                Assert.AreEqual(resulttextBoy, driver.FindElement(ResultTextLokator).Text,
                    "Неверное отображение текста об успехе заявки на имя для мальчика");

                Assert.AreEqual(email, driver.FindElement(YourEmailLokator).Text,
                    "Неверное отображение адреса email на который отправили имя");

                Assert.IsTrue(driver.FindElement(LinkAnotherEmailLokator).Displayed,
                    "Не отображается ссылка на новую заявку");

                Assert.IsFalse(driver.FindElement(ButtonSendMeLokator).Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' исчезнет");
            });
        }

        [Test]
        public void NameForParrotChoice_SendEmailForGirl_Success()
        {
           driver.FindElement(RadioButtonGirlLokator).Click();

            var email = "123abc@mail.ru";
            driver.FindElement(EmailInputLokator).SendKeys(text: email);

            driver.FindElement(ButtonSendMeLokator).Click();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(ResultTextLokator).Displayed,
                    "Текст об успехе заявки на имя не отображается");

                var resulttextGirl = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
                Assert.AreEqual(resulttextGirl, driver.FindElement(ResultTextLokator).Text,
                    "Неверное отображение текста об успехе заявки на имя для девочки");

                Assert.AreEqual(email, driver.FindElement(YourEmailLokator).Text,
                    "Неверное отображение адреса email на который отправили имя");

                Assert.IsTrue(driver.FindElement(LinkAnotherEmailLokator).Displayed,
                    "Не отображается ссылка на новую заявку");

                Assert.IsFalse(driver.FindElement(ButtonSendMeLokator).Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' исчезнет");
            });
        }

        [Test]
        public void NameForParrotChoice_EmailInputNoValue_ErrorNoValue()
        {
           driver.FindElement(ButtonSendMeLokator).Click();

           Assert.Multiple(() =>
           {
               Assert.IsTrue(driver.FindElement(ErrorEmailInputLokator).Displayed, "Ожидали сообщение об ошибке ввода");

               var NoValueErorText = "Введите email";
               Assert.AreEqual(NoValueErorText, driver.FindElement(EmailInputErrorMessageLokator).Text,
                   "Ожидали сообщение об ошибке 'Введите email'");

               Assert.IsTrue(driver.FindElement(ButtonSendMeLokator).Displayed,
                   "Ожидали, что кнопка 'Подобрать имя' не исчезнет");
           });
        }

        [Test]
        public void NameForParrotChoice_EmailInputIncorrectEmail_ErrorIncorrectEmail()
        {
          driver.FindElement(EmailInputLokator).SendKeys(text: "123abc");
            
            driver.FindElement(ButtonSendMeLokator).Click();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(ErrorEmailInputLokator).Displayed,
                    "Ожидали сообщение об ошибке ввода");

                var ResultTextErorIncorrectEmail = "Некорректный email";
                Assert.AreEqual(ResultTextErorIncorrectEmail, driver.FindElement(EmailInputErrorMessageLokator).Text,
                    "Ожидали сообщение об ошибке 'Некорректный email'");

                Assert.IsTrue(driver.FindElement(ButtonSendMeLokator).Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' не исчезнет");
            });
        }

        [Test]
        public void  NameForParrotChoice_ClickAnotherEmailForBoy_Success()
        {
         driver.FindElement(RadioButtonBoyLokator).Click();

            driver.FindElement(EmailInputLokator).SendKeys(text: "123abc@mail.ru");

            driver.FindElement(ButtonSendMeLokator).Click();
            
            driver.FindElement(LinkAnotherEmailLokator).Click();

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(driver.FindElement(EmailInputLokator).Text, "Ожидали, что поле email очистится");
                Assert.IsTrue(driver.FindElement(ButtonSendMeLokator).Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' не исчезнет");
                Assert.IsFalse(driver.FindElement(LinkAnotherEmailLokator).Displayed,
                    "Ожидали, что ссылка 'указать другой email' исчезла");
            });
        }

        [Test]
        public void NameForParrotChoice_ClickAnotherEmailForGirl_Success()

        {
         driver.FindElement(RadioButtonGirlLokator).Click();

            driver.FindElement(EmailInputLokator).SendKeys(text: "123abc@mail.ru");

            driver.FindElement(ButtonSendMeLokator).Click();
            
            driver.FindElement(LinkAnotherEmailLokator).Click();

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(driver.FindElement(EmailInputLokator).Text, "Ожидали, что поле email очистится");
                Assert.IsTrue(driver.FindElement(ButtonSendMeLokator).Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' не исчезнет");
                Assert.IsFalse(driver.FindElement(LinkAnotherEmailLokator).Displayed,
                    "Ожидали, что ссылка 'указать другой email' исчезла");
            });
        }

    [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

