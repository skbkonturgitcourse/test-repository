using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class FirstSeleniumTests
    {
// Открыть браузер
// Перейти на страницу https://ru.wikipedia.org/
// Ввести в поле поиска слово «Selenium»
// Нажать кнопку поиска
// Убедиться, что перешли на страницу про Selenium
//Страница_Действие_ОжидаемыйРезультат

        [SetUp]
        public void SetUp()
        {
//открываем браузер, разворачиваем его на полный экран
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }


        private ChromeDriver driver;
        private readonly By sarchInputLocator = By.Name("search");

        [Test]
        public void Wikipedia_Search_Success()
        {
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
            var searchInput = driver.FindElement(sarchInputLocator);
            searchInput.SendKeys("Selenium");

            var searchButton = driver.FindElement(By.Name("go"));
            searchButton.Click();

            var expectedText = "Selenium — Википедия";
            Assert.True(driver.Title.Contains($"{expectedText}"), $"Ожидали Title = '{expectedText}'\r\n" +
                                                                  $"Получили '{driver.Title}'");
        }

        private By emailInputLokator = By.ClassName("email");
        private By buttonWruteToMeLokator = By.Id("write-to-me");
        private By resultTextLokator = By.Name("result-text");
        private By resultEmailLokator = By.TagName("pre");
        private By linkAontherEmailLolator = By.LinkText("указать другой e-mail");
        private string urlFixPC = "https://qa-course.kontur.host/selenium-lesson/";

        [Test]
        public void FixPC_SendEmail_Success()
        {
            var expectedResultText = "Заявка успешно создана!!! Мы скоро вам напишем по этому e-mail:";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPC);

//указать алрес мыла
            var email = "test@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonWruteToMeLokator).Click();
//проверяем что появился текст "Зяавка принята"
            Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                "Сообщение об успехе создания заявки отображается");
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                "Неверное сообщение об успехе создания заявки");
//проверяем что емейл совпадает с тем что отправляли
            Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                "Неверный емейл на кторый будем отвечать");
//проверяем что появилась ссылка "указать другой емейл"
            Assert.IsTrue(driver.FindElement(linkAontherEmailLolator).Displayed,
                "Ссылка 'Указать другой e-mail' отображается");
        }

        [Test]
        public void FixPC_ClickAnotherEmail_Success()
        {
            var expectedResultText = "Заявка успешно создана!!! Мы скоро вам напишем по этому e-mail:";
//перейти по урлу
            driver.Navigate().GoToUrl(urlFixPC);

//указать алрес мыла
            var email = "test@mail.ru";
            driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
            driver.FindElement(buttonWruteToMeLokator).Click();
//Кликнуть ссылку "указать другой емаил"
            driver.FindElement(linkAontherEmailLolator).Click();
//проверим что поле очистило
            Assert.IsEmpty(driver.FindElement(emailInputLokator).Text, "Ожидали что поле для ввода емейла осистится");
//проверим что кнопка появилась
            Assert.IsTrue(driver.FindElement(buttonWruteToMeLokator).Displayed,
                "Ожидали, что кнопка 'Наишите мне' отображается");
//проверим что исчезда ссылка "указать другой емаил"
            Assert.AreEqual(0, driver.FindElements(linkAontherEmailLolator).Count);
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}