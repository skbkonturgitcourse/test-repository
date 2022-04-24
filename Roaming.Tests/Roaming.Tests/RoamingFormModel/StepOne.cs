namespace Roaming.Tests;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class StepOne
{
    private ChromeDriver driver;
    private By companyLocator { get; } = By.Id("Company");
    private By innLocator { get; } = By.Id("Inn");
    private By kppLoactor { get; } = By.Id("Kpp");
    private By emailLocator { get; } = By.Id("Liame");
    private By phoneLocator { get; } = By.Id("Phone");
    private By nameLocator { get; } = By.Id("Name");
    private By validationErrors = By.ClassName("field-validation-error");
    private By phoneCommentLocator = By.ClassName("form-input-comment");
    private By nextLocator { get; } = By.XPath(".//input[@value='Следующий шаг']");

    public StepOne(ChromeDriver driver)
    {
        this.driver = driver;
        this.driver.Navigate().GoToUrl("https://qa-course.kontur.host/roaming");
        // сначала ждал загрузки страницы в конструкторе
        // но тогда все тесты с проверкой валидацией после нажатия кнопки Следующий шаг падают на ожидании
    }

    public string[] GetValidations()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        return wait.Until(x => x.FindElements(validationErrors))
            .Select(n => n.Text)
            .ToArray();
    }

    public string GetPhoneComment()
    {
        return driver.FindElement(phoneCommentLocator).Text;
    }

    public StepOne FillData(string company = TestData.company, string inn = TestData.inn, 
        string email = TestData.email, string phone = TestData.phone)
    {
        driver.FindElement(companyLocator).SendKeys(company);
        driver.FindElement(innLocator).SendKeys(inn);
        driver.FindElement(emailLocator).SendKeys(email);
        driver.FindElement(phoneLocator).SendKeys(phone);
        return this;
    }
    public StepTwo Next()
    {
        FillData();
        return NextStep();
    }

    // Тут мой корявый fluent-интерфейс :) 
    // Надо тоже научиться делать по-нормальному
    public StepTwo NextStep()
    {
        driver.FindElement(nextLocator).Click();
        return new StepTwo(driver);
    }
}