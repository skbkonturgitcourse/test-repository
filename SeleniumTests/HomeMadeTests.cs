using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class HomeMadeTests
{
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions(); //открываем браузер и разворачиваем на полный экран
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
    }

    private ChromeDriver driver;

    [Test]
    public void NameParrot_Boy_SendLatinEmail_Success()
    {
        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";

        //Переход на страницу
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Мальчик" и кликнуть по ней
        var searchRadioButtonBoy = driver.FindElement(By.Id("boy"));
        searchRadioButtonBoy.Click();

        // Найти поле ввода Email и указать e-mail адрес
        // var emailInput = driver.FindElement(By.Name("email"));
        var email = "test@mail.ru";
        driver.FindElement(By.Name("email")).SendKeys(email);

        // Нажать на кнопку "Подобрать имя"
        // var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();

        //Проверить, что появился текст отправленной заявки "Хорошо, мы пришлём имя для вашего мальчика на e-mail:"
        //var resultText = driver.FindElement(By.ClassName("result-text"));
        Assert.IsTrue(driver.FindElement(By.ClassName("result-text")).Displayed,
            "Сообщение об успехе отправки email не отображается");
        Assert.AreEqual(expectedResultText, driver.FindElement(By.ClassName("result-text")).Text,
            "Неверное сообщение об успехе отправки email");

        // Проверить email, на который отправлена заявка
        var resultEmail = driver.FindElement(By.ClassName("your-email"));
        Assert.AreEqual(email, resultEmail.Text, "Неверный email, на который отправлено имя попугая");

        //Проверить, что появилась ссылка "Указать другой email"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        Assert.IsTrue(linkAnotherEmail.Displayed, "Ссылка 'Указать другой e-mail' не отображается");
    }

    [Test]
    public void NameParrot_Boy_ClickLinkAnotherLatinEmail_Success()
    {
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
        var searchRadioButtonBoy = driver.FindElement(By.Id("boy"));
        searchRadioButtonBoy.Click();
        var email = "test@mail.ru";
        driver.FindElement(By.Name("email")).SendKeys(email);
        driver.FindElement(By.Id("sendMe")).Click();

        //Кликнуть ссылку "Указать другой e-mail"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        driver.FindElement(By.LinkText("указать другой e-mail")).Click();

        //Проверить, что поле ввода e-mail очистилось
        Assert.IsEmpty(driver.FindElement(By.Name("email")).Text, "Ожидали, что поле для ввода e-mail очистится");

        //Проверить, что появилась кнопка "Подобрать имя"
        Assert.IsTrue(driver.FindElement(By.Id("sendMe")).Displayed,
            "Ожидали, что кнопка 'Подобрать имя' отображается");

        //Проверить, что исчезла ссылка "Указать другой e-mail"
        Assert.AreEqual(0, driver.FindElements(By.LinkText("указать другой e-mail")).Count);
    }

    [Test]
    public void NameParrot_Boy_SendCyrillicEmail_Success()
    {
        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";

        //Переход на страницу
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Мальчик" и кликнуть по ней
        var searchRadioButtonBoy = driver.FindElement(By.Id("boy"));
        searchRadioButtonBoy.Click();

        // Найти поле ввода Email и указать e-mail адрес
        // var emailInput = driver.FindElement(By.Name("email"));
        var email = "тест@почта.рф";
        driver.FindElement(By.Name("email")).SendKeys(email);

        // Нажать на кнопку "Подобрать имя"
        // var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();

        //Проверить, что появился текст отправленной заявки "Хорошо, мы пришлём имя для вашего мальчика на e-mail:"
        //var resultText = driver.FindElement(By.ClassName("result-text"));
        Assert.IsTrue(driver.FindElement(By.ClassName("result-text")).Displayed,
            "Сообщение об успехе отправки email не отображается");
        Assert.AreEqual(expectedResultText, driver.FindElement(By.ClassName("result-text")).Text,
            "Неверное сообщение об успехе отправки email");

        // Проверить email, на который отправлена заявка
        var resultEmail = driver.FindElement(By.ClassName("your-email"));
        Assert.AreEqual(email, resultEmail.Text, "Неверный email, на который отправлено имя попугая");

        //Проверить, что появилась ссылка "Указать другой email"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        Assert.IsTrue(linkAnotherEmail.Displayed, "Ссылка 'Указать другой e-mail' не отображается");
    }


    [Test]
    public void NameParrot_Girl_SendLatinEmail_Success()
    {
        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";

        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Девочка" и кликнуть по ней
        var searchRadioButtonGirl = driver.FindElement(By.Id("girl"));
        searchRadioButtonGirl.Click();

        //Найти поле ввода Email и указать email адрес
        //var emailInput = driver.FindElement(By.Name("email"));
        var email = "test@mail.ru";
        driver.FindElement(By.Name("email")).SendKeys(email);

        // Нажать на кнопку "Подобрать имя"
        // var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();

        //Проверить, что появился текст отправленной заявки "Хорошо, мы пришлём имя для вашей девочки на e-mail:"
        //var resultText = driver.FindElement(By.ClassName("result-text"));
        Assert.IsTrue(driver.FindElement(By.ClassName("result-text")).Displayed,
            "Сообщение об успехе отправки email не отображается");
        Assert.AreEqual(expectedResultText, driver.FindElement(By.ClassName("result-text")).Text,
            "Неверное сообщение об успехе отправки email");

        // Проверить email, на который отправлена заявка
        var resultEmail = driver.FindElement(By.ClassName("your-email"));
        Assert.AreEqual(email, resultEmail.Text, "Неверный email, на который отправлено имя попугая");

        //Проверить, что появилась ссылка "Указать другой email"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        Assert.IsTrue(linkAnotherEmail.Displayed, "Ссылка 'Указать другой e-mail' не отображается");
    }

    [Test]
    public void NameParrot_Girl_ClickLinkAnotherEmail_Success()
    {
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        var searchRadioButtonGirl = driver.FindElement(By.Id("girl"));
        searchRadioButtonGirl.Click();

        var email = "test@mail.ru";
        driver.FindElement(By.Name("email")).SendKeys(email);

        driver.FindElement(By.Id("sendMe")).Click();

        //Кликнуть ссылку "Указать другой e-mail"
        var linkAnotherEmail = driver.FindElement(By.LinkText("указать другой e-mail"));
        driver.FindElement(By.LinkText("указать другой e-mail")).Click();

        //Проверить, что поле ввода e-mail очистилось
        Assert.IsEmpty(driver.FindElement(By.Name("email")).Text, "Ожидали, что поле для ввода e-mail очистится");

        //Проверить, что появилась кнопка "Подобрать имя"
        Assert.IsTrue(driver.FindElement(By.Id("sendMe")).Displayed,
            "Ожидали, что кнопка 'Подобрать имя' отображается");

        //Проверить, что исчезла ссылка "Указать другой e-mail"
        Assert.AreEqual(0, driver.FindElements(By.LinkText("указать другой e-mail")).Count);
    }

    [Test]
    public void NameParrot_Boy_UnableSendEmptyEmail_Success()
    {
        //Переход на страницу 
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Мальчик" и кликнуть по ней
        var searchRadioButtonBoy = driver.FindElement(By.Id("boy"));
        searchRadioButtonBoy.Click();

        //Проверить, что поле ввода e-mail пустое
        Assert.IsEmpty(driver.FindElement(By.Name("email")).Text, "Ожидали, что поле для ввода e-mail пустое");

        //Нажать на кнопку "Подобрать имя"
        //var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();

        //Проверить, что появилось сообщение об ошибке class = "form-error"
        var error = driver.FindElement(By.ClassName("form-error"));
        Assert.IsTrue(driver.FindElement(By.ClassName("form-error")).Displayed,
            "Ожидали, что появится сообщение 'Введите email'");
    }

    [Test]
    public void NameParrot_Girl_UnableSendEmptyEmail_Success()
    {
        //Переход на страницу 
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        // Найти радиокнопку "Девочка" и кликнуть по ней
        var searchRadioButtonGirl = driver.FindElement(By.Id("girl"));
        searchRadioButtonGirl.Click();

        //Проверить, что поле ввода e-mail пустое
        Assert.IsEmpty(driver.FindElement(By.Name("email")).Text, "Ожидали, что поле для ввода e-mail пустое");

        //Нажать на кнопку "Подобрать имя"
        //var buttonSendMe = driver.FindElement(By.Id("sendMe"));
        driver.FindElement(By.Id("sendMe")).Click();

        //Проверить, что появилось сообщение об ошибке class = "form-error"
        var error = driver.FindElement(By.ClassName("form-error"));
        Assert.IsTrue(driver.FindElement(By.ClassName("form-error")).Displayed,
            "Ожидали, что появится сообщение 'Введите email'");
    }

    [Test]
    public void NameParrot_Boy_RadioButtonGirl_IsntChecked()
    {
        //Переход на страницу 
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

        //Найти радио кнопки "Мальчик" и "Девочка"
        var buttonBoy = driver.FindElement(By.Id("boy"));
        var buttonGirl = driver.FindElement(By.Id("girl"));

        buttonBoy.Click();
        Assert.AreEqual("true", buttonBoy.GetAttribute("checked"), "Должна быть выбрана радиобаттон Мальчик");
        Assert.AreEqual(null, buttonGirl.GetAttribute("checked"), "Девочка не должна быть выбрана");
       
        buttonGirl.Click();
        Assert.AreEqual("true", buttonGirl.GetAttribute("checked"), "Должна быть выбрана радиобаттон Девочка");
        Assert.AreEqual(null, buttonBoy.GetAttribute("checked"), "Мальчик не должен быть выбран");
    }


    [TearDown]
    public void TearDown()

    {
        driver.Quit();
    }
}