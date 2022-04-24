using OpenQA.Selenium.Support.UI;

namespace Roaming.Tests;

using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

public class StepFourTests
{
    private ChromeDriver driver;
    private StepsWalker walker;
    [SetUp]
    public void SetUp()
    {
        driver = ConfigureDriver.GetDriver();
        walker = new StepsWalker(driver);
    }
    
    [Test]
    public void SendForm_Success()
    {
        var finalStep = walker.ToFourthStep();
        Assert.That(finalStep.GetHeaderText().Contains("Готово!"));
    }
    
    [Test]
    public void SendForm_DataSavedToDB()
    {
        // передавать почту
        walker.ToFourthStep();
        var checkDb = new CheckDB();
        var requestInfo = checkDb.GetInfo("fqkfqlkf@fqfff.ru");
        Assert.That(requestInfo.Phone, !Is.Null);
    }

 

  

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
    
    // [Test]
    // public void UserInfoStep_PhoneComment_RightText()
    // {
    //     Assert.That(
    //         driver.FindElement(phoneCommentLocator).Text ==
    //         "Например: (343) 3441010 или 89051234567"
    //     );
    // }
}


// private const string url = "https://qa-course.kontur.host/selenium-practice/";
// private By emailInputLocator = By.XPath(".//*[@id = 'form']/input");
// private By emailButtonLocator = By.Id("sendMe");
// private By emailRadioMaleLocator = By.Id("boy");
// private By emailRadioFemaleLocator = By.Id("girl");
// private By radioMaleTextLocator = By.XPath(".//*[@id = 'form']//label[1]");
// private By radioFemaleTextLocator = By.XPath(".//*[@id = 'form']//label[2]");
// private By emailValidationLocator = By.XPath(".//*[@id = 'form']/pre");
// private By successTextLocator = By.XPath(".//*[@id = 'resultTextBlock']/div[@class='result-text']"); 
// private By successEmailLocator = By.XPath(".//*[@id = 'resultTextBlock']/pre");
// private By useAnotherEmailLinkLocator = By.XPath(".//*[@id = 'resultTextBlock']/following-sibling::a");