using System.Reflection;

namespace Roaming.Tests;

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class StepTwo
{
    private string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
    private ChromeDriver driver;
    private By missStepLocator { get; } = By.XPath(".//div[contains(@class, 'roaming-skip-step')]/span");
    public By uploadLocator { get; } = By.Id("file-upload-button");
    public By nextLocator { get; } = By.XPath(".//*[@value= 'Следующий шаг]'");
    public By validationLocator { get; } = By.XPath(".//div[contains(@class, 'js-file-upload-error')]");
    public By fileNameLocator { get; } = By.XPath(".//span[contains(@class, 'file__name')]");
    public By fileSizeLocator { get; } = By.XPath(".//span[contains(@class, 'file__size')]");

    public bool CurrentStepIsSecond() => driver.FindElement(By.CssSelector(".progress.active")).Text == "2";

    public void waitStepLoad() => new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(e => e.FindElement(missStepLocator));

    public StepTwo(ChromeDriver driver)
    {
        this.driver = driver;
    }

    public void AddInvalidImage()
    {
        waitStepLoad();
        var image = dir + @"\Files\download.xlsx";
        driver.FindElement(uploadLocator).SendKeys(image);
    }

    public void AddValidImage()
    {
        waitStepLoad();
        var image = dir + @"\Files\download.tiff";
        driver.FindElement(uploadLocator).SendKeys(image);
    }

    public bool CheckFileSizeAndName() =>
        driver.FindElement(fileNameLocator).Text == "download.tiff" &&
        driver.FindElement(fileSizeLocator).Text == "4.2 КБ";

    public string GetValidationText()
    {
        return driver.FindElement(validationLocator).Text;
    }

    public StepThree Next()
    {
        waitStepLoad();
        driver.FindElement(missStepLocator).Click();
        return new StepThree(driver);
    }
}