namespace Roaming.Tests;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

public class StepOneTests
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
    public void DontFillRequiredFields_RightValidationText()
    {
        var firstStep = walker.ToFirstStep().FillData("", "", "");
        Assert.That(firstStep.GetValidations().Count(x => x == "Заполните поле") == 3);
    }

    [Test]
    public void InvalidInn_Validation()
    {
        var testInn = "abcdef"; 
        var validations = walker.ToFirstStep().FillData(inn: testInn).GetValidations();
        Assert.That(validations.Length == 1, $"Невалидный только ИНН {testInn}. Получили валидаций: {validations.Length}");
    }

    [Test]
    public void EmailWithUpperCase_Success()
    {
        var testEmail = "test@test.ru";
        var step = walker.ToFirstStep().FillData(email: testEmail);
        var stepTwo = step.NextStep();
        try
        {
            stepTwo.waitStepLoad();
            Assert.That(stepTwo.CurrentStepIsSecond());
        }
        catch
        {
            Assert.That(false, "Не удалось добраться до второго шага");
        }
         
        // Похоже на говнокод, но я не успел придумать по-нормальному, как проверить, что страница загрузилась
        // и при этом не обкладывать всё try catch
        // разве что ожидать фиксированное время - но тогда прогон тестов будет занимать кучу времени
    }
    [Test]
    public void LongEmail_Success()
    {
        var testEmail = "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttest@test.ru";
        var stepOne = walker.ToFirstStep().FillData(email: testEmail);
        var stepTwo = stepOne.NextStep();
        try
        {
            stepTwo.waitStepLoad();
            Assert.That(stepTwo.CurrentStepIsSecond());
        }
        catch
        {
            Assert.That(false, "Не удалось добраться до второго шага");
        }
    }
    
    [Test]
    public void EmailWithShorSecondLevelDomain_Success()
    {
        var testEmail = "test@t.ru";
        var stepOne = walker.ToFirstStep().FillData(email: testEmail);
        var stepTwo = stepOne.NextStep();
        try
        {
            stepTwo.waitStepLoad();
            Assert.That(stepTwo.CurrentStepIsSecond());
        }
        catch
        {
            Assert.That(false, "Не удалось добраться до второго шага");
        }
    }
    
    [Test]
    public void PhoneStartsWithPlus7_Success()
    {
        var phone = "+79321292698";
        var stepOne = walker.ToFirstStep().FillData(phone: phone);
        var stepTwo = stepOne.NextStep();
        try
        {
            stepTwo.waitStepLoad();
            Assert.That(stepTwo.CurrentStepIsSecond());
        }
        catch
        {
            Assert.That(false, "Не удалось добраться до второго шага");
        }
    }
    
    [Test]
    public void PhoneStartsWith7_Success()
    {
        var phone = "79321292698";
        var stepOne = walker.ToFirstStep().FillData(phone: phone);
        var stepTwo = stepOne.NextStep();
        try
        {
            stepTwo.waitStepLoad();
            Assert.That(stepTwo.CurrentStepIsSecond());
        }
        catch
        {
            Assert.That(false, "Не удалось добраться до второго шага");
        }
    }
    
    [Test]
    public void PhoneComment_RightText()
    {
        Assert.That(walker.ToFirstStep().GetPhoneComment() == "Например: (343) 3441010 или 89051234567");
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}