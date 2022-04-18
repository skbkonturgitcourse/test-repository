using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.PageObjects;


namespace TestHomeworkKorobkin.PageObjects
{
    public class MainPage
    {
        private IWebDriver _webDriver;
        public IWebElement SendButton => _webDriver.FindElement(By.Id("sendMe"));
        public IWebElement ResultText => _webDriver.FindElement(By.ClassName("result-text"));
        public IWebElement ResultEmail => _webDriver.FindElement(By.CssSelector("pre[class='your-email']"));
        public IWebElement AnotherEmailLink => _webDriver.FindElement(By.LinkText("указать другой e-mail"));
        public IWebElement ValidationMessage => _webDriver.FindElement(By.CssSelector("pre[class='form-error']"));
        public IWebElement ValidationRedFrame => _webDriver.FindElement(By.CssSelector("input[class='error']"));
        public IWebElement BoyButton => _webDriver.FindElement(By.Id("boy"));
        public IWebElement GirlButton => _webDriver.FindElement(By.Id("girl"));
        public IWebElement Email => _webDriver.FindElement(By.Name("email"));
        public MainPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public void FillAllFieldsAndClickNextButton(Gender gender, string email)
        {
            // Выбрать пол
            (gender == Gender.Boy ? BoyButton : GirlButton).Click();
            //Указать email
            Email.SendKeys(email);
            //Нажать кнопку "Подобрать имя"
            SendButton.Click();
        }
        public bool IsAnotherMailLinkExist()
        {
            return _webDriver.FindElements(By.LinkText("указать другой e-mail")).Count > 0;
        }

    }
}