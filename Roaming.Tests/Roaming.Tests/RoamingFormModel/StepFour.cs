namespace Roaming.Tests;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class StepFour
{
    private ChromeDriver driver;
    private By nextLocator { get; } = By.XPath(".//*[text() = 'Отправить еще заявку']");
    private By headerLocator { get; } = By.TagName("h2");
    public void waitStepLoad() => new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(e => e.FindElement(nextLocator));

    public StepFour(ChromeDriver driver)
    {
        this.driver = driver;
    }

    public string GetHeaderText()
    {
        waitStepLoad();
        return driver.FindElement(headerLocator).Text;
    }

    public StepOne StartAgain()
    {
        waitStepLoad();
        driver.FindElement(nextLocator).Click();
        return new StepOne(driver);
    }
}