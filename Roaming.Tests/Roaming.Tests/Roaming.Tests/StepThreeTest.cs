namespace Roaming.Tests;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

public class StepThreeTest
{
    private ChromeDriver driver;
    private StepsWalker walker;
    
    [SetUp]
    public void SetUp()
    {
        driver = ConfigureDriver.GetDriver();
        walker = new StepsWalker(driver);
    }

    [TestCase(10)]
    public void AddContragents_Success(int count)
    {
        var thirdStep = walker.ToThirdStep();
        thirdStep.AddContragents(count);
        Assert.Multiple(() =>
        {
            Assert.That(thirdStep.GetFoldedContragentsCount() == count, $"Свернутых форм меньше, чем {count}");
            Assert.That(thirdStep.Next().GetHeaderText().ToLower().Contains("готово"), "Мы не дошли до четвёртого шага");
        });
    }
    
    [Test]
    public void AdInvalidList_Validation()
    {
        var step = walker.ToThirdListStep();
        step.AddInvalidList();
        Assert.That(step.GetValidationText().Contains("ошибк"));
    }

    [Test]
    public void AddValidList_Success()
    {
        var step = walker.ToThirdListStep();
        step.AddValidList();
        Assert.That(step.Next().GetHeaderText().ToLower().Contains("готово"), "Мы не дошли до четвёртого шага");
    }

    [Test]
    public void PhoneSavesInDB_Success()
    {
        var email = "email@email.ru";
        var phone = new Random().Next(111111111, 999999999) + "9";
        walker.ToFirstStep()
            .FillData(email: email, phone: phone)
            .NextStep();
        var checkDb = new CheckDB();
        Assert.That(phone == checkDb.GetInfo(email).Phone);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
    
}