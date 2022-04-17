using System;
using NUnit.Framework.Internal.Execution;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace HomeWork
{
    public class MyFirstAutotest
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }
        
        private ChromeDriver driver;

        private By emailInputLokator = By.Name("email");
        private By buttonSendMeLokator = By.Id("sendMe");
        private By resultTextLokator = By.ClassName("result-text");
        private By resultEmailLokator = By.ClassName("your-email");
        private By linkAnotherEmailLokator = By.LinkText("указать другой e-mail");
        private By CheckBoxBoyLokator = By.Id("boy");
        private By CheckBoxGirlLokator = By.Id("girl");
        private By ResultWarningLokator = By.ClassName("form-error");
        private By FirstTextOnPageLokator = By.ClassName("title");
        private By SecondTextOnPageLokator = By.ClassName("subtitle-bold");
        private By ThirdTextOnPageLokator = By.ClassName("formQuestion");
        private By FourthTextOnPageLokator = By.XPath("/html/body/div[2]/div/div/div/div/form/div[4]");
        private By FifthTextOnPageLokator = By.XPath("/html/body/div[2]/div/div/div/div/form/div[3]/label[1]");
        private By SixthTextOnPageLokator = By.XPath("/html/body/div[2]/div/div/div/div/form/div[3]/label[2]");
        private string urlFixPc = "https://qa-course.kontur.host/selenium-practice/";

        [Test]
        public void NameSelection_SendEmailForBoy_Success()
        {
            var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            driver.Navigate().GoToUrl(urlFixPc);
            driver.FindElement(CheckBoxBoyLokator).Click();
            var email = "popug@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успехе создания заявки");
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный email на который будем отвечать");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }

        [Test]
        public void NameSelection_SendEmailForGirl_Success()
        {
            var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            driver.Navigate().GoToUrl(urlFixPc);
            driver.FindElement(CheckBoxGirlLokator).Click();
            var email = "popug@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успехе создания заявки");
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный email на который будем отвечать");
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }
        
        [Test]
        public void NameSelection_ClickLinkAnotherEmail_Success()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var email = "popug@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            driver.FindElement(linkAnotherEmailLokator).Click();
            Assert.IsEmpty(driver.FindElement(emailInputLokator).Text, 
                "Ожидали что поле для ввода email очистится");
            Assert.IsTrue(driver.FindElement(buttonSendMeLokator).Displayed,
                "Ожидали, что кнопка 'ПОДОБРАТЬ ИМЯ' отображается");
            Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count,
                "Ожидали, что ссылка 'Указать другой e-mail' пропала");
        }

        [Test]
        public void NameSelection_DisplayingTextOnThePage_Success()
        {
            var FirstTextOnPage = "Не знаешь как назвать?";
            var SecondTextOnPage = "Мы подберём имя для твоего попугайчика!";
            var ThirdTextOnPage = "Кто у тебя?";
            var FourthTextOnPage = "На какой e-mail прислать варианты имён?";
            var FifthTextOnPage = "мальчик";
            var SixthTextOnPage = "девочка";
            driver.Navigate().GoToUrl(urlFixPc);
            Assert.IsTrue(driver.FindElement(FirstTextOnPageLokator).Displayed,
                "Текст 'Не знаешь как назвать?' отображается");
            Assert.AreEqual(FirstTextOnPage, driver.FindElement(FirstTextOnPageLokator).Text,
                "Текст 'Не знаешь как назвать?' написан неверно");
            Assert.IsTrue(driver.FindElement(SecondTextOnPageLokator).Displayed,
                "Текст 'Мы подберём имя для твоего попугайчика!' отображается");
            Assert.AreEqual(SecondTextOnPage, driver.FindElement(SecondTextOnPageLokator).Text,
                "Текст 'Мы подберём имя для твоего попугайчика!' написан неверно");
            Assert.IsTrue(driver.FindElement(ThirdTextOnPageLokator).Displayed,
                "Текст 'Кто у тебя?' отображается");
            Assert.AreEqual(ThirdTextOnPage, driver.FindElement(ThirdTextOnPageLokator).Text,
                "Текст 'Кто у тебя?' написан неверно");
            Assert.IsTrue(driver.FindElement(FourthTextOnPageLokator).Displayed,
                "Текст 'На какой e-mail прислать варианты имён?' отображается");
            Assert.AreEqual(FourthTextOnPage, driver.FindElement(FourthTextOnPageLokator).Text,
                "Текст 'На какой e-mail прислать варианты имён?' написан неверно");
            Assert.IsTrue(driver.FindElement(FifthTextOnPageLokator).Displayed,
                "Текст гендера 'мальчик' отображается");
            Assert.AreEqual(FifthTextOnPage, driver.FindElement(FifthTextOnPageLokator).Text,
                "Текст гендера 'мальчик' написан неверно");
            Assert.IsTrue(driver.FindElement(SixthTextOnPageLokator).Displayed,
                "Текст гендера 'девочка' отображается");
            Assert.AreEqual(SixthTextOnPage, driver.FindElement(SixthTextOnPageLokator).Text,
                "Текст гендера 'девочка' написан неверно");
        }
        
        [Test]
        public void NameSelection_SendingEmptyFieldEmail_Succes()
        {
            var expectedWarning = "Введите email";
            driver.Navigate().GoToUrl(urlFixPc);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Предупреждение о том, что email невведен отображается");
            Assert.AreEqual(expectedWarning, driver.FindElement(ResultWarningLokator).Text,
                "Предупреждение о том, что email невведен - написано неверно");
        }
        
        [Test]
        public void NameSelection_SendingEmailWithoutNameAndDomain_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var email = "@.";
            driver.FindElement(emailInputLokator).SendKeys(email);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Ожидали, что появится предупреждение о некорректном email");
        }

        [Test]
        public void NameSelection_SendingEmailWithoutDomenain_Succes()
        {
            var expectedWarning = "Некорректный email";
            driver.Navigate().GoToUrl(urlFixPc);
            var OneSymbolEmail = "Popug";
            driver.FindElement(emailInputLokator).SendKeys(OneSymbolEmail);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Ожидали, что появится предупреждение о некорректном email");
            Assert.AreEqual(expectedWarning, driver.FindElement(ResultWarningLokator).Text,
                "Предупреждение о том, что email некорректен - написано неверно");
        }

        [Test]
        public void NameSelection_SendingOneSumbolEmail_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailOfOneSymbol = "M@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailOfOneSymbol);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [Test]
        public void NameSelection_Sending255SumbolEmail_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailOf255Symbols = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailOf255Symbols);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [Test]
        public void NameSelection_Sending256SumbolEmail_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailOf256Symbols = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailOf256Symbols);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [Test]
        public void NameSelection_Sending257SumbolEmail_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailOf257Symbols = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailOf257Symbols);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Ожидали, что появится предупреждение о некорректном email");
        }
        
        [Test]
        public void NameSelection_SendingEmailWithSpacesInTheName_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailWithSpaces = "     @mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailWithSpaces);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Ожидали, что появится предупреждение о некорректном email");
        }
        
        [Test]
        public void NameSelection_SendingEmailWithSpacesInTheDomen_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailWithSpaces = "popug@   .  ";
            driver.FindElement(emailInputLokator).SendKeys(emailWithSpaces);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(ResultWarningLokator).Displayed,
                "Ожидали, что появится предупреждение о некорректном email");
        }
        
        [Test]
        public void NameSelection_SendingEmailWithSpecialSymbols_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailWithSymbols = "Popug!#.$%&‘*+—/=?^_`{|}~@google.com";
            driver.FindElement(emailInputLokator).SendKeys(emailWithSymbols);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [Test]
        public void NameSelection_SendingEmailWithUpperCase_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailWithUpperCase = "POPUG@GOOGLE.COM";
            driver.FindElement(emailInputLokator).SendKeys(emailWithUpperCase);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [Test]
        public void NameSelection_SendingEmailConsistingOfNumbers_Succes()
        {
            driver.Navigate().GoToUrl(urlFixPc);
            var emailOfNumbers = "1234567890@ya.ru";
            driver.FindElement(emailInputLokator).SendKeys(emailOfNumbers);
            driver.FindElement(buttonSendMeLokator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        
    }
}