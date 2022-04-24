namespace Roaming.Tests;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

public class StepsWalker
{
    private ChromeDriver driver;

    public StepsWalker(ChromeDriver driver)
    {
        this.driver = driver;
    }

    public StepOne ToFirstStep()
    {
        return new StepOne(driver);
    }

    public StepTwo ToSecondStep()
    {
        return new StepOne(driver).Next();
    }

    public StepThree ToThirdStep()
    {
        return new StepOne(driver).Next().Next();
    }

    public StepThreeList ToThirdListStep ()
    {
        return new StepOne(driver).Next().Next().ToListUpload();
    }
    
    public StepFour ToFourthStep()
    {
        return new StepOne(driver).Next().Next().Next();
    }
}