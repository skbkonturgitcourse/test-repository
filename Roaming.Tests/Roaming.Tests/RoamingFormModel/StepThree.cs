namespace Roaming.Tests;

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


public class StepThree
{
    private ChromeDriver driver;
    private By companyLocator { get; } = By.Id("Form_Company");
    private By innLocator { get; } = By.Id("Form_Inn");
    private By kppLoactor { get; } = By.Id("Form_Kpp");
    public By operatorLocator { get; } = By.Id("operator");
    private By nameLocator { get; } = By.Id("Form_Name");
    private By phoneLocator { get; } = By.Id("Form_Phone");
    public static By addLocator { get; } = By.XPath(".//span[contains(@class, 'js-test-submit-btn')]"); 
    public  By nextLocator { get; } = By.XPath(".//input[@value = 'Отправить']");
    public  By tabByListLocator { get; } = By.XPath(".//*[text() = 'Списком']");
    public By shortRequisites { get; } = By.XPath(".//div[contains(@class, 'contractor-short__requisites')]");
    public void waitStepLoad() => new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(e => e.FindElement(companyLocator));
    public StepThree(ChromeDriver driver)
    {
        this.driver = driver;
    }

    public int GetFoldedContragentsCount() =>
        driver.FindElements(shortRequisites).Count();
    
    public void AddContragents(int count)
    {
        waitStepLoad();
        if (count > 10) throw new Exception("Создавайте не больше 10 контрагентов");
        for (int i = 0; i < count; i++)
        {
            AddData(inn:TestData.inns12[i]);
            driver.FindElement(addLocator).Click();
        }
    }
    
    public void AddData(string company = TestData.company, string inn = TestData.inn)
    {
        waitStepLoad();
        driver.FindElement(companyLocator).SendKeys(company);
        driver.FindElement(innLocator).SendKeys(inn);
        var select = new SelectElement(driver.FindElement(operatorLocator));
        select.SelectByIndex(6);
    }

    public StepFour Next()
    {
        waitStepLoad();
        AddData();
        driver.FindElement(nextLocator).Click();
        return new StepFour(driver);
    }

    public StepThreeList ToListUpload()
    {
        waitStepLoad();
        driver.FindElement(tabByListLocator).Click();
        return new StepThreeList(driver);
    }
}