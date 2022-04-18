using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestHomeworkKorobkin;

public class TestBase
{
    public IWebDriver WebDriver;

    [OneTimeSetUp]
    public void DoBeforeAllTheTests()
    {
        WebDriver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        WebDriver.Quit();
    }

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        WebDriver = new ChromeDriver(options);
        WebDriver.Navigate().GoToUrl(TestSettings.HostPrefix);
    }
}