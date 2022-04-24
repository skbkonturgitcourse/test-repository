namespace Roaming.Tests;
using OpenQA.Selenium.Chrome;

public static class ConfigureDriver
{
    public static ChromeDriver GetDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        return new ChromeDriver(options);
    }
}

