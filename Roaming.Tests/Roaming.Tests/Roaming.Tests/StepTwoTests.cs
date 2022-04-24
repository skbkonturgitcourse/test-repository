namespace Roaming.Tests;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

public class StepTwoTests
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
    public void UploadConfirmationXlsx_IsInvalid()
    {
        var secondStep = walker.ToSecondStep();
        secondStep.AddInvalidImage();
        Assert.That(secondStep.GetValidationText() == "Для загрузки доступны файлы формата: tiff, jpg, jpeg, png, pdf");
    }
    
    [Test]
    public void UploadConfirmationTiff_Success()
    {
        var secondStep = walker.ToSecondStep();
        secondStep.AddValidImage();
        Assert.Multiple(() => 
        {
            Assert.That(secondStep.GetValidationText() == "", "На валидный файл .tiff сработала валидация");
            Assert.That(secondStep.CheckFileSizeAndName(), "Отображается неправильное имя или размер файла");
        });
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}