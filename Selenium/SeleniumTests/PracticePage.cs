using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests;

public class PracticePage
{
    private readonly WebDriverWait wait;

    public PracticePage(IWebDriver browser)
    {
        wait = new WebDriverWait(browser, TimeSpan.FromSeconds(1));
    }

    #region PageElements

    public IWebElement Title => FindWithWait(By.ClassName("title"));
    public IWebElement Subtitle => FindWithWait(By.ClassName("subtitle-bold"));
    public IWebElement GenderQuestion => FindWithWait(By.XPath("(//div[@class='formQuestion'])[1]"));
    public IWebElement GenderBoyRadioButton => FindWithWait(By.Id("boy"));
    public IWebElement GenderBoyLabel => FindWithWait(By.XPath("(//label[@for='choiceFirst'])[1]"));
    public IWebElement GenderGirlRadioButton => FindWithWait(By.Id("girl"));
    public IWebElement GenderGirlLabel => FindWithWait(By.XPath("(//label[@for='choiceSecond'])[1]"));
    public IWebElement EmailQuestion => FindWithWait(By.XPath("(//div[@class='formQuestion'])[2]"));
    public IWebElement EmailInput => FindWithWait(By.Name("email"));
    public IWebElement Step1SubmitButton => FindWithWait(By.Id("sendMe"));
    public IWebElement Step1EmailValidationError => FindWithWait(By.ClassName("form-error"));
    public IWebElement Step2ResultText => FindWithWait(By.ClassName("result-text"));
    public IWebElement Step2ResultEmail => FindWithWait(By.ClassName("your-email"));
    public IWebElement Step2AnotherEmailButton => FindWithWait(By.Id("anotherEmail"));

    #endregion
    
    public string DefaultEmail => "my@test.email";
    public string EmailInputValue => EmailInput.GetAttribute("value");

    public void FormStep2()
    {
        GenderBoyRadioButton.Click();
        EmailInput.SendKeys(DefaultEmail);
        Step1SubmitButton.Click();
    }

    private IWebElement FindWithWait(By locator) 
        => wait.Until(browser => browser.FindElement(locator));
}