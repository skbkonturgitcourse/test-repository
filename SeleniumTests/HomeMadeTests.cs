using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class HomeMadeTests
{
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions(); //открываем браузер
        options.AddArgument("start-maximized"); //разворачиваем на полный экран
        driver = new ChromeDriver(options);
    }

    private ChromeDriver driver;

    [Test]
    public void Test1()
    {
        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
        
        //Переход на страницу
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Мальчик" и кликнуть по ней
        var searchRadioButtonBoy = driver.FindElement(By.Id("boy")); 
        searchRadioButtonBoy.Click(); 
       // bool unused = driver.FindElement(By.CssSelector("input[type='radio']:first-of-type")).Selected;
        

        // Найти радиокнопку "Девочка" и кликнуть по ней
       // var searchRadioButtonGirl = driver.FindElement(By.Id("girl"));
        //searchRadioButtonGirl.Click();
       // bool value = driver.FindElement(By.CssSelector("input[type='radio']:last-of-type")).Selected;
        
        // Найти поле ввода Email и указать e-mail адрес
       // var emailInput = driver.FindElement(By.Name("email"));
        var email = "test@mail.ru";
        driver.FindElement(By.Name("email")).SendKeys(email);
        
        // Нажать на кнопку "Подобрать имя"
       // var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();
        
        //Проверить, что появился текст отправленной заявки "Хорошо, мы пришлём имя для вашего мальчика на e-mail:"
        //var resultText = driver.FindElement(By.ClassName("result-text"));
        Assert.IsTrue(driver.FindElement(By.ClassName("result-text")).Displayed, "Сообщение об успехе отправки email не отображается");
        Assert.AreEqual(expectedResultText, driver.FindElement(By.ClassName("result-text")).Text, "Неверное сообщение об успехе отправки email");
        
        // Проверить email, на который отправлена заявка
        var resultEmail = driver.FindElement(By.ClassName("your-email"));
        Assert.AreEqual(email, resultEmail.Text, "Неверный email, на который отправлено имя попугая");
       
        //Проверить, что появилась ссылка "Указать другой email"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        Assert.IsTrue(linkAnotherEmail.Displayed, "Ссылка 'Указать другой e-mail' не отображается");
        
        //Кликнуть ссылку "Указать другой e-mail"
        linkAnotherEmail.Click();
        
        //Проверить, что поле ввода e-mail очистилось
        Assert.IsEmpty(driver.FindElement(By.Name("email")).Text, "Ожидали, что поле для ввода e-mail очистится");

        //Проверить, что появилась кнопка "Подобрать имя"
        Assert.IsTrue(driver.FindElement(By.Id("sendMe")).Displayed, "Ожидали, что кнопка 'Подобрать имя' отображается");
        
        //Проверить, что исчезла ссылка "Указать другой e-mail"
        Assert.IsFalse(linkAnotherEmail.Displayed, "Ожидали, что ссылка 'Указать другой e-mail' не отображается");
    }

    [TearDown]
    public void TearDown() 
    
    {
        driver.Quit();
    }
}