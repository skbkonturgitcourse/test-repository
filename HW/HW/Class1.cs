using System.Net.Mime;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HW
{
    public class Class1
    {
        [SetUp]
        public void SetUp()
        {
//открываем браузер, разворачиваем его на полный экран
            var driver = new ChromeDriver();
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        private By radioBoyInputLokator = By.Id("boy");
        private By radioGirlInputLokator = By.Id("girl");
        private By emailInputLokator = By.ClassName("email");
        private By buttonSendMeLokator = By.Id("sendMe");
        private By resultTextLokator = By.Name("result-text");
        private By resultEmailLokator = By.TagName("pre");
        private By linkAnotherEmailLokator = By.LinkText("указать другой e-mail");
        private string urlFixPc = "https://qa-course.kontur.host/selenium-practice/";

        [Test]
        public void FixPc_SendEmailWithBoy_Success()
        {
            var driver = new ChromeDriver();
            var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPc);
//проверяем что есть радиобаттоны "мальчик" и "девочка"            
            Assert.IsTrue(driver.FindElement(radioBoyInputLokator).Displayed,"мальчик");
            Assert.IsTrue(driver.FindElement(radioGirlInputLokator).Displayed, "девочка");
//выбрать мальчика            
            driver.FindElement(radioBoyInputLokator).Click();
//ввести емайл
            var email = "kaltunopsa@vusra.com";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonSendMeLokator).Click();
//проверяем что появился текст "Зяавка принята"
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успехе создания заявки");
//проверяем что емейл совпадает с тем что отправляли
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный емейл на кторый будем отвечать");
//проверяем что появилась ссылка "указать другой емейл"
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }
        
        [Test]
        public void FixPc_SendEmailWithGirl_Success()
        {
            var driver = new ChromeDriver();
            var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPc);
//проверяем что есть радиобаттоны "мальчик" и "девочка"            
            Assert.IsTrue(driver.FindElement(radioBoyInputLokator).Displayed,"мальчик");
            Assert.IsTrue(driver.FindElement(radioGirlInputLokator).Displayed, "девочка");
//выбрать девочку          
            driver.FindElement(radioGirlInputLokator).Click();
//ввести емайл
            var email = "kaltunopsa@vusra.com";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonSendMeLokator).Click();
//проверяем что появился текст "Зяавка принята"
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успехе создания заявки");
//проверяем что емейл совпадает с тем что отправляли
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный емейл на кторый будем отвечать");
//проверяем что появилась ссылка "указать другой емейл"
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }
        
        [Test]
        public void FixPc_SendWrongEmailWithBoy_Fail()
        {
            var driver = new ChromeDriver();
            var expectedResultText = "Некорректный email";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPc);
//выбрать мальчика            
            driver.FindElement(radioBoyInputLokator).Click();
//ввести некорректный емайл
            var email = "ааааа";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonSendMeLokator).Click();
//проверить что появился текст "Некорректный емайл"
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение о неккоректности емайла отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Некорректный email");
        }
        
        [Test]
        public void FixPc_SendWrongEmailWithGirl_Fail()
        {
            var driver = new ChromeDriver();
            var expectedResultText = "Некорректный email";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPc);
//выбрать девочку            
            driver.FindElement(radioGirlInputLokator).Click();
//ввести некорректный емайл
            var email = "ааааа";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonSendMeLokator).Click();
//проверить что появился текст "Некорректный емайл"
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение о неккоректности емайла отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Некорректный email");
        }

        [Test]
        public void FixPc_SendNewEmail_Success()
        {
            var driver = new ChromeDriver();
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPc);
//выбрать девочку            
            driver.FindElement(radioGirlInputLokator).Click();
//указать алрес мыла
            var email = "kaltunopsa@vusra.com";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonSendMeLokator).Click();
//Кликнуть ссылку "указать другой емаил"
            driver.FindElement(linkAnotherEmailLokator).Click();
//проверим что поле очистило
            Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                "Ожидали что поле для ввода емейла осистится");
//проверим что кнопка появилась
            Assert.IsTrue(driver.FindElement(buttonSendMeLokator).Displayed,
                "Ожидали, что кнопка 'Отправтье мне' отображается");
//проверим что исчезда ссылка "указать другой емаил"
            Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
        }
        

        [TearDown]
        public void TearDown()
        {
            var driver = new ChromeDriver();
            driver.Quit();
        }
    }
}