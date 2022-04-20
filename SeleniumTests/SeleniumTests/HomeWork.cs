using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class HomeWork
    {
        private ChromeDriver driver;
        private string NameOfTheParrot = "https://qa-course.kontur.host/selenium-practice/";
        private By radioBoyLocator = By.Id("boy");
        private By radioGirlLocator = By.Id("girl");
        private By emailInputLocator = By.Name("email");
        private By buttonSendLocator = By.Id("sendMe");
        private By resultTextLocator = By.Id("resultTextBlock");
        private By formError = By.ClassName("form-error");

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void NameOfTheParrotGirl_ClickLinkAnotherEmail_success()
        {
            
            driver.Navigate().GoToUrl(NameOfTheParrot);
            driver.FindElement(this.radioGirlLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys("1@1.ru");
            driver.FindElement(buttonSendLocator).Click();
            
            var resultText = driver.FindElement(resultTextLocator);
            //wait.Until(c => c.FindElement(resultTextLocator).Displayed.Equals(true));
            Assert.AreEqual("Хорошо, мы пришлём имя для вашей девочки на e-mail:\r\n1@1.ru", resultText.Text);
        }

        [Test]
        public void EmptyEmail_ValidationOnClickSend_error()
        {
            driver.Navigate().GoToUrl(NameOfTheParrot);
            driver.FindElement(buttonSendLocator).Click();
            var textValidation = driver.FindElement(formError);
            //wait.Until(c => textValidation.Displayed.Equals(true));
            Assert.AreEqual("Введите email", textValidation.Text);
        }

        [Test]
        public void IncorrectEmail_ValidationOnClickSend_error()
        {
            driver.Navigate().GoToUrl(NameOfTheParrot);
            driver.FindElement(emailInputLocator).SendKeys("111111");
            driver.FindElement(buttonSendLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formError).Text);
        }

        [Test]
        public void NameOfTheParrot_ClickOnAnotherEmail_success()
        {
            // Перейти на страницу https://qa-course.kontur.host/selenium-practice/
            driver.Navigate().GoToUrl(NameOfTheParrot);

            driver.FindElement(this.radioGirlLocator).Click();
            // Ввести в поле "Почта" корректный адрес почты
            driver.FindElement(emailInputLocator).SendKeys("1@1.ru");

            // Нажать кнопку "Подобрать имя"
            driver.FindElement(buttonSendLocator).Click();

            driver.FindElementById("anotherEmail").Click();
            Assert.Multiple(() =>{
                Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text);
                Assert.AreEqual(true, driver.FindElement(this.radioGirlLocator).Selected);
            });
        }

        [Test]
        public void NameOfTheParrotBoy_reselectionRadio_success()
        {
            driver.Navigate().GoToUrl(NameOfTheParrot);
            driver.FindElement(this.radioGirlLocator).Click();
            Assert.AreEqual(true, driver.FindElement(this.radioGirlLocator).Selected);
            driver.FindElement(this.radioBoyLocator).Click();
            Assert.AreEqual(true, driver.FindElement(this.radioBoyLocator).Selected);
            driver.FindElement(emailInputLocator).SendKeys("1@1.ru");

            // Нажать кнопку "Подобрать имя"
            driver.FindElement(buttonSendLocator).Click();
            Assert.AreEqual("Хорошо, мы пришлём имя для вашего мальчика на e-mail:\r\n1@1.ru", driver.FindElement(resultTextLocator).Text);
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
