using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework;

public class ParrotNameTests
{
    //Открыть браузер
    //Перейти на страницу: https://qa-course.kontur.host/selenium-practice/
    //Найти радиобаттон "Мальчик" id="boy"  "Девочка" id = "girl"
    //var radioInput = driver.FindElement(by.Id("boy"));
    //var radioInput = driver.FindElement(by.Id("girl"));
    //Нажать радиобаттон
    //radioInput.Click();
    //Найти поле ввода e-mail class="email"
    // var emailInput = driver.FindElement(By.Name("email"));
    //Ввести e-mail
    //var email = "";
    //driver.FindElement(By.Name("email")).SendKeys(email);
    //Найти кнопку "Подобрать имя"
    //var buttonSendMe = driver.FindElement(By.Id("sendMe"));
    //Нажать кнопку "Подобрать имя"
    //driver.FindElement(By.Id("sendMe")).Click();
    //Проверить, что появился текст: "Хорошо, мы пришлём имя для вашего мальчика на e-mail:" class name="result-text"
    //var resultText = driver.FindElement(By.ClassName("result-text"));
    //Проверить, что появился текст: "Хорошо, мы пришлём имя для вашей девочки на e-mail:" class name="result-text"
    //var resultText = driver.FindElement(By.ClassName("result-text"));
    //Проверить, что e-mail совпадает с тем, что ввели class name = "your-email"
    //var resultEmail = driver.FindElement(By.ClassName("your-email"));
    //Проверить, что появилась ссылка "Указать другой e-mail"
    //var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
    [SetUp]
    public void SetUp()
    {
    //Открыть браузер, развернуть на полный экран
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }
    private ChromeDriver driver;
    private string urlChoosePN = "https://qa-course.kontur.host/selenium-practice/";
    private By emailInputLocator = By.Name("email");
    private By buttonSendMeLocator = By.Id("sendMe");
    private By resultEmailLocator = By.ClassName("your-email");
    private By resultTextLocator = By.ClassName("result-text");
    private By radioBoyLocator = By.Id("boy");
    private By radioGirlLocator = By.Id("girl");
    private By linkTextLocator = By.LinkText("указать другой e-mail");
    private By emptyEmailLocator = By.ClassName("form-error");


    [Test]
    public void ChoosePN_SendEmailBoy_Success()
    {
         var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
        //Перейти на страницу
        driver.Navigate().GoToUrl(urlChoosePN); 
        //Найти радиобаттон "Мальчик" 
        var radioInput = driver.FindElement(radioBoyLocator);
        //Нажать радиобаттон
        radioInput.Click();
        //Найти поле ввода e-mail 
        var emailInput = driver.FindElement(emailInputLocator);
        //Ввести biletik389@carsik.com
        var email = "biletik389@carsik.com";
        driver.FindElement(emailInputLocator).SendKeys(email);
        //Найти кнопку "Подобрать имя"
        var buttonSendMe = driver.FindElement(buttonSendMeLocator);
        //Нажать кнопку "Подобрать имя"
        driver.FindElement(buttonSendMeLocator).Click();
        //Проверить, что появился текст: "Хорошо, мы пришлём имя для вашего мальчика на e-mail:" 
        var resultText = driver.FindElement(resultTextLocator);
        Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed,
            "Сообщение об успехе создания заявки не отображается"); 
        //Проверить содержимое текста
       Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text, 
           "Неверное сообщение об успехе создания заявки");

        //Проверить, что e-mail совпадает с тем, что ввели 
        Assert.AreEqual(email, driver.FindElement(resultEmailLocator).Text, 
            "Неверный email на который будем отвечать");
        //Проверить, что появилась ссылка "указать другой e-mail"
        Assert.IsTrue(driver.FindElement(linkTextLocator).Displayed, 
            "Ссылка 'Указать другой e-mail' отображается");
        
      }
    
    
    [Test]
    public void ChoosePN_ClickLinkAnotherEmailBoy_Success()
    {
        //Действия повторяются, как в тесте "ChoosePN_SendEmailBoy_Success" до этапа проверки текста
        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
        driver.Navigate().GoToUrl(urlChoosePN); 
        var radioInput = driver.FindElement(radioBoyLocator);
        radioInput.Click();
        var emailInput = driver.FindElement(emailInputLocator);
        var email = "biletik389@carsik.com";
        driver.FindElement(emailInputLocator).SendKeys(email);
        var buttonSendMe = driver.FindElement(buttonSendMeLocator);
        driver.FindElement(buttonSendMeLocator).Click();
       
        //Кликнуть ссылку "указать другой e-mail"
        driver.FindElement(linkTextLocator).Click();
        
        Assert.Multiple(() =>
        {
            //Проверить, что поле очистилось
            Assert.IsEmpty(driver.FindElement(emailInputLocator).Text, "Ожидали, что поле для ввода e-mail очистится");
            //Проверить, что появился радиобаттон "Мальчик"
            Assert.IsTrue(driver.FindElement(radioBoyLocator).Displayed,
                "Ожидали, что радиобаттон 'Мальчик' отображается");
            //Проверить, что появился радиобаттон "Девочка"
            Assert.IsTrue(driver.FindElement(radioGirlLocator).Displayed,
                "Ожидали, что радиобаттон 'Девочка' отображается");
            //Проверить, что выбран радиобаттон "Мальчик"
            Assert.IsTrue(driver.FindElement(radioBoyLocator).Selected, "Ожидали, что радиобаттон 'Мальчик' выбран");
            //Проверить, что кнопка появилась
            Assert.IsTrue(driver.FindElement(buttonSendMeLocator).Displayed,
                "Ожидали, что кнопка 'Подобрать имя' отображается");
            //Проверить, что исчезла ссылка "указать другой e-mail"
            Assert.AreEqual(0, driver.FindElements(linkTextLocator).Count);
        });
    }
     
    [Test]
    public void ChoosePN_SendEmailGirl_Success()
    {
        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
        //Перейти на страницу
        driver.Navigate().GoToUrl(urlChoosePN); 
        //Найти радиобаттон "Девочка" 
        var radioInput = driver.FindElement(radioGirlLocator);
        //Нажать радиобаттон
        radioInput.Click();
        //Найти поле ввода e-mail 
        var emailInput = driver.FindElement(emailInputLocator);
        //Ввести fopixet387@carsik.com
        var email = "fopixet387@carsik.com";
        driver.FindElement(emailInputLocator).SendKeys(email);
        //Найти кнопку "Подобрать имя"
        var buttonSendMe = driver.FindElement(buttonSendMeLocator);
        //Нажать кнопку "Подобрать имя"
        driver.FindElement(buttonSendMeLocator).Click();
        //Проверить что появился текст: "Хорошо, мы пришлём имя для вашей девочки на e-mail:"
        var resultText = driver.FindElement(resultTextLocator);
        Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed,
            "Сообщение об успехе создания заявки не отображается"); 
        //Проверить содержимое текста
        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text, 
            "Неверное сообщение об успехе создания заявки");
        //Проверить что e-mail совпадает с тем, что ввели
        Assert.AreEqual(email, driver.FindElement(resultEmailLocator).Text, 
            "Неверный email на который будем отвечать");
        //проверяем что появилась ссылка "указать другой e-mail"
        Assert.IsTrue(driver.FindElement(linkTextLocator).Displayed, 
            "Ссылка 'Указать другой e-mail' отображается");
        
       }

    [Test]
    public void ChoosePN_ClickLinkAnotherEmailGirl_Success()
    {
        //Действия повторяются, как в тесте "ChoosePN_SendEmailGirl_Success" до этапа проверки текста
        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
        driver.Navigate().GoToUrl(urlChoosePN); 
        var radioInput = driver.FindElement(radioGirlLocator);
        radioInput.Click();
        var emailInput = driver.FindElement(emailInputLocator);
        var email = "fopixet387@carsik.com";
        driver.FindElement(emailInputLocator).SendKeys(email);
        var buttonSendMe = driver.FindElement(buttonSendMeLocator);
        driver.FindElement(buttonSendMeLocator).Click();
        //Кликнуть ссылку "указать другой e-mail"
        driver.FindElement(linkTextLocator).Click();
        
        Assert.Multiple(() =>
        {
            //Проверить, что поле очистилось
            Assert.IsEmpty(driver.FindElement(emailInputLocator).Text, "Ожидали, что поле для ввода e-mail очистится");

            //Проверить, что появился радиобаттон "Мальчик"
            Assert.IsTrue(driver.FindElement(radioBoyLocator).Displayed,
                "Ожидали, что радиобаттон 'Мальчик' отображается");
            //Проверить, что появился радиобаттон "Девочка"
            Assert.IsTrue(driver.FindElement(radioGirlLocator).Displayed,
                "Ожидали, что радиобаттон 'Девочка' отображается");
            //Проверить, что выбран радиобаттон "Девочка"
            Assert.IsTrue(driver.FindElement(radioGirlLocator).Selected, "Ожидали, что радиобаттон 'Девочка' выбран");
            //Проверить, что кнопка появилась
            Assert.IsTrue(driver.FindElement(buttonSendMeLocator).Displayed,
                "Ожидали, что кнопка 'Подобрать имя' отображается");
            //Проверить, что исчезла ссылка "указать другой e-mail"
            Assert.AreEqual(0, driver.FindElements(linkTextLocator).Count);
        });
    }
    
    [Test]
    public void ChoosePN_SendEmptyEmail_Success()
    {
      var expectedResultText = "Введите email";
        //Перейти на страницу
        driver.Navigate().GoToUrl(urlChoosePN); 
        //Найти радиобаттон "Мальчик" 
        var radioInput = driver.FindElement(radioBoyLocator);
        //Нажать радиобаттон
        radioInput.Click();
        //Найти поле ввода e-mail 
        var emailInput = driver.FindElement(emailInputLocator);
        //Оставить поле ввода e-mail пустым
        var email = "";
        driver.FindElement(emailInputLocator).SendKeys(email);
        //Найти кнопку "Подобрать имя"
        var buttonSendMe = driver.FindElement(buttonSendMeLocator);
        //Нажать кнопку "Подобрать имя"
        driver.FindElement(buttonSendMeLocator).Click();
        //Проверить, что появился текст: "Введите email" 
        var resultText = driver.FindElement(emptyEmailLocator);
        Assert.IsTrue(driver.FindElement(emptyEmailLocator).Displayed,
            "Сообщение о необходимости ввести e-mail не отображается"); 
        //Проверить содержимое текста
        Assert.AreEqual(expectedResultText, driver.FindElement(emptyEmailLocator).Text, 
            "Неверное сообщение о необходимости ввести e-mail");
        
        //Проверить, что текст располагается под полем ввода e-mail
        //Проверить, что цвет текста - красный
        // String cssValue = driver.FindElement(emptyEmailLocator).GetCssValue("color");
        //Assert.AreEqual("#D91515", driver.FindElement(emptyEmailLocator).Text,
        //    "Цвет текста 'Введите email' не соответствует");
        //Не смогла разобраться 
        
        //Проверить, что кнопка "Подобрать имя" не исчезла
        Assert.IsTrue(driver.FindElement(buttonSendMeLocator).Displayed, 
            "Кнопка 'Подобрать имя' отображается");
        
    }
    
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

}