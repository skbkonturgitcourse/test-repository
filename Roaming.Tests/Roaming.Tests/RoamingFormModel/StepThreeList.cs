namespace Roaming.Tests;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class StepThreeList
{
    private ChromeDriver driver;
    private string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
    public By uploadLocator = By.Id("file-upload-button");
    public By sendLocator = By.XPath(".//div[@id = 'next-step-btn']//input");
    public By uploadFixedListLocator = By.XPath(".//div[@id = 'showAll']//div[@class = 'js-file-upload-text']");
    public By validationTextLocator = By.XPath(".//div[@id = 'file-upload-text']/div");
    public void waitStepLoad() => new WebDriverWait(driver, TimeSpan.FromSeconds(10))
        .Until(e => e.FindElement(uploadLocator));
    
    public StepThreeList(ChromeDriver driver)
    {
        this.driver = driver;
    }


    public string GetValidationText()
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(2))
            .Until(ExpectedConditions.ElementToBeClickable(uploadFixedListLocator));
        return driver.FindElement(validationTextLocator).Text;
    }
    
   
    public void AddValidList()
    {
        waitStepLoad();
        driver.FindElement(uploadLocator).SendKeys(dir + @"\Files\contragents.xlsx");
    }
    public void AddInvalidList()
    {
        waitStepLoad();
        driver.FindElement(uploadLocator).SendKeys(dir + @"\Files\emptyList.xlsx");
    }

    public StepFour Next()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        wait.Until(ExpectedConditions.ElementToBeClickable(sendLocator)).Click();
        return new StepFour(driver);
    }
}