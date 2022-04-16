using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V95.CSS;

namespace ParrotNameTests
{
    /// <summary>
    /// Тесты сайта подбора имени для попугайчиков.
    /// </summary>
    public class ParrotNamePageTests
    {
        private ChromeDriver driver;

        private string urlParrotName = "https://qa-course.kontur.host/selenium-practice/";

        [SetUp]
        public void SetUp()
        {
            //Открываем браузер, разворачиваем его на полный экран, переходим по ссылке
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(urlParrotName);
        }

        private readonly By _emailInputLocator = By.Name("email");
        private readonly By _buttonSendMeLocator = By.Id("sendMe");
        private readonly By _radioBoyLocator = By.Id("boy");
        private readonly By _radioGirlLocator = By.Id("girl");
        private readonly By _resultTextLocator = By.ClassName("result-text");
        private readonly By _resultEmailLocator = By.ClassName("your-email");
        private readonly By _linkAnotherEmailLocator = By.Id("anotherEmail");
        private readonly By _formErrorLocator = By.ClassName("form-error");

        private string correctEmail = "test@mail.ru";
        private string incorrectEmail = "27tjdwuc3";
        private readonly string _longCorrectEmail = new string('a', 100) + "@site.ru";
        private string _expectedResultText;

        private void SendRequestAfterChooseGender(string email)
        {
            //Ввести адрес email
            driver.FindElement(_emailInputLocator).SendKeys(email);
            //Нажать на кнопку
            driver.FindElement(_buttonSendMeLocator).Click();
        }

        private void CompareTextsAfterSendRequest(string expectedText)
        {
            //Проверяем, что появился текст "Заявка принята"
            Assert.IsTrue(driver.FindElement(_resultTextLocator).Displayed,
                "Текст об успехе создания заявки не отображается");
            Assert.AreEqual(expectedText, driver.FindElement(_resultTextLocator).Text,
                "Неверный текст об успехе создания заявки");
            //Проверяем, что email совпадает с тем, что вводили
            Assert.AreEqual(correctEmail, driver.FindElement(_resultEmailLocator).Text,
                "Неверный email, на который будем отвечать");
            //Проверяем, что появилась ссылка "указать другой email"
            Assert.IsTrue(driver.FindElement(_linkAnotherEmailLocator).Displayed,
                "Ссылка 'указать другой e-mail' не отображается");
        }

        /// <summary>
        /// Тест отправки заявки для попугая-мальчика.
        /// </summary>
        [Test]
        public void ParrotName_RequestForBoy_Success()
        {
            _expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            //Нажать на переключатель "Мальчик"
            driver.FindElement(_radioBoyLocator).Click();
            Assert.IsFalse(driver.FindElement(_radioGirlLocator).Selected, "Нажали на радиобаттон 'Мальчик', " +
                                                                           "но радиобаттон 'Девочка' активен");
            SendRequestAfterChooseGender(correctEmail);
            CompareTextsAfterSendRequest(_expectedResultText);
        }

        /// <summary>
        /// Тест отправки заявки для попугая-девочки.
        /// </summary>
        [Test]
        public void ParrotName_RequestForGirl_Success()
        {
            _expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            //Нажать на переключатель "Девочка"
            driver.FindElement(_radioGirlLocator).Click();
            Assert.IsFalse(driver.FindElement(_radioBoyLocator).Selected, "Нажали на радиобаттон 'Девочка', " +
                                                                          "но радиобаттон 'Мальчик' активен");
            SendRequestAfterChooseGender(correctEmail);
            CompareTextsAfterSendRequest(_expectedResultText);
        }

        /// <summary>
        /// Тест работы ссылки "Указать другой email".
        /// </summary>
        [Test]
        public void ParrotName_ClickLinkAnotherEmail_Success()
        {
            //Отправить заявку
            driver.FindElement(_radioBoyLocator).Click();
            SendRequestAfterChooseGender(correctEmail);
            //Кликнуть на ссылку "Указать другой email"
            driver.FindElement(_linkAnotherEmailLocator).Click();
            //Проверяем, что поле ввода email очистилось
            Assert.IsEmpty(driver.FindElement(_emailInputLocator).Text,
                "Поле ввода email не очистилось после нажатия на " +
                "ссылку 'Указать другой email");
            //Проверяем, что появилась кнопка "Подобрать имя"
            Assert.IsTrue(driver.FindElement(_buttonSendMeLocator).Displayed, "Кнопка 'Подобрать имя' не отображается");
            //Проверяем, что исчезла ссылка "Указать другой email"
            if (0 != driver.FindElements(_linkAnotherEmailLocator).Count)
            {
                Assert.IsFalse(driver.FindElement(_linkAnotherEmailLocator).Displayed,
                    "Ссылка 'указать другой email' не исчезла" +
                    "после нажатия на неё");
            }
        }

        /// <summary>
        /// Тест валидации некорректного email.
        /// </summary>
        [Test]
        public void ParrotName_RequestIncorrectEmail_Fail()
        {
            //Отправить заявку с некорректной почтой
            driver.FindElement(_radioBoyLocator).Click();
            SendRequestAfterChooseGender(incorrectEmail);
            Assert.IsTrue(driver.FindElement(_formErrorLocator).Displayed,
                "Ошибка о некорректном email не отображается");
            Assert.AreEqual(driver.FindElement(_formErrorLocator).Text, "Некорректный email",
                "Неверный текст ошибки о некорректном email");
            Assert.AreNotEqual(0, driver.FindElements(By.ClassName("error")).Count,
                "Поле ввода email при ошибке должно обводиться красным цветом");
        }

        /// <summary>
        /// Тест валидации пустого email.
        /// </summary>
        [Test]
        public void ParrotName_RequestEmptyEmail_Fail()
        {
            //Отправить заявку с пустой почтой
            driver.FindElement(_radioBoyLocator).Click();
            SendRequestAfterChooseGender("");
            Assert.IsTrue(driver.FindElement(_formErrorLocator).Displayed,
                "Ошибка о некорректном email не отображается");
            Assert.AreEqual(driver.FindElement(_formErrorLocator).Text, "Введите email",
                "Неверный текст ошибки о некорректном email");
            Assert.AreNotEqual(0, driver.FindElements(By.ClassName("error")).Count,
                "Поле ввода email при ошибке должно обводиться красным цветом");
        }

        /// <summary>
        /// Позитивный тест на длинный email.
        /// </summary>
        [Test]
        public void ParrotName_RequestLongCorrectEmail_Success()
        {
            //Отправить заявку с корректной длинной почтой
            driver.FindElement(_radioBoyLocator).Click();
            SendRequestAfterChooseGender(_longCorrectEmail);
            var elements = driver.FindElements(_formErrorLocator).Where(x => x.Displayed == true).ToArray();
            Assert.AreEqual(0, elements.Length, "При вводе корректного, но длинного email, происходит ошибка");
        }

        /// <summary>
        /// Тест сохранения введённого email при нажатии на переключатель другого пола.
        /// </summary>
        [Test]
        public void ParrotName_EmailIsNotEmptyAfterChangeGender()
        {
            driver.FindElement(_radioBoyLocator).Click();
            //Ввести адрес email
            driver.FindElement(_emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(_radioGirlLocator).Click();
            Assert.IsNotEmpty(driver.FindElement(_emailInputLocator).GetAttribute("value"),
                "При клике на другой пол введённый email исчезает");
            Assert.AreEqual(correctEmail, driver.FindElement(_emailInputLocator).GetAttribute("value"),
                "При клике на другой пол введённый email меняется");
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}