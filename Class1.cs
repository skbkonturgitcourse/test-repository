using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homeworktests;

public class HomeWork
{

[SetUp]
public void SetUp()
{
//открываем браузер, разворачиваем его на полный экран
var options = new ChromeOptions();
options.AddArgument("start-maximized");
driver = new ChromeDriver(options);
}

private ChromeDriver driver;

private By emailInputLokator = By.Name("email");
private By buttonWruteToMeLokator = By.Id("sendMe");
private By resultTextLokator = By.ClassName("result-text");
private By resultEmailLokator = By.ClassName("your-email");
private By radiobutton = By.Id ("girl");
private By ErrorMessage = By.TagName("pre");
private string SeleniumLesson = "https://qa-course.kontur.host/selenium-practice/";

[Test]
public void SuccessMan()
{
// Открыть браузер
// Перейти на страницу https://qa-course.kontur.host/selenium-practice/
// Ввести в поле корректный емаил
// Нажать кнопку "Подобрать имя"
// Убедиться, что выводится сообщение об отправке имени,с нужным полом и на емаил, который мы указали 

var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
driver.Navigate().GoToUrl(SeleniumLesson);

//указать адрес емайла
var email = "test1@mail.ru";
driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
driver.FindElement(buttonWruteToMeLokator).Click();
//проверяем что появился текст "Мы пришлем вам имя"
Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, "Сообщение об успехе создания заявки отображается");
//проверяем что текст корректный
Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text, "Неверное сообщение об успехе создания заявки");
//проверяем что емейл совпадает с тем что отправляли
Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text, "Неверный емейл на который будем отвечать");
}

[Test]
public void SuccessWoman()
{
// Открыть браузер
// Перейти на страницу https://qa-course.kontur.host/selenium-practice/
// Ввыбрать женский пол
// Ввести в поле корректный емаил
// Нажать кнопку "Подобрать имя"
// Убедиться, что выводится сообщение об отправке имени,с нужным полом и на емаил, который мы указали 
    var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
    driver.Navigate().GoToUrl(SeleniumLesson);
// Выбрать женский пол
    driver.FindElement(radiobutton).Click();
//указать адрес емаила
    var email = "test1@mail.ru";
    driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
    driver.FindElement(buttonWruteToMeLokator).Click();
//проверяем что появился текст "Зяавка принята"
    Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, "Сообщение об успехе создания заявки отображается");
//проверяем что текст корректный
    Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text, "Неверное сообщение об успехе создания заявки");
//проверяем что емейл совпадает с тем что отправляли
    Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text, "Неверный емейл на который будем отвечать");
}

[Test]
public void Emptyemail()
{ 
// Открыть браузер
// Перейти на страницу https://qa-course.kontur.host/selenium-practice/
// Не вводить емаил
// Нажать кнопку "Подобрать имя"
// Убедиться, что выводится сообщение об ошибке 
    var expectedResultText1 = "Введите email";
    driver.Navigate().GoToUrl(SeleniumLesson);
//Нажать кнопку
    driver.FindElement(buttonWruteToMeLokator).Click();
//проверяем что появился текст "Введите емаил"
    Assert.IsTrue(driver.FindElement(ErrorMessage).Displayed, "Сообщение об ошибке отображается");
//проверяем что текст корректный
    Assert.AreEqual(expectedResultText1, driver.FindElement(ErrorMessage).Text, "Неверное сообщение об ошибке");
}

[Test]
public void InvalideEmail()
{ 
// Открыть браузер
// Перейти на страницу https://qa-course.kontur.host/selenium-practice/
// Ввести в поле некорректный емаил
// Нажать кнопку "Подобрать имя"
// Убедиться, что выводится сообщение об ошибке 
    var expectedResultText = "Некорректный email";
    driver.Navigate().GoToUrl(SeleniumLesson);

//указать корректный адрес емаила
    var email = "test1@mail";
    driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
    driver.FindElement(buttonWruteToMeLokator).Click();
//проверяем, что появился текст ошибки
    Assert.IsTrue(driver.FindElement(ErrorMessage).Displayed, "Сообщение об ошибке отображается");
//проверяем, что текст корректный
    Assert.AreEqual(expectedResultText, driver.FindElement(ErrorMessage).Text, "Неверное сообщение об ошибке");
}

[Test]
public void ClickLinkAnotherEmailSuccess()
{ 
// Открыть браузер
// Перейти на страницу https://qa-course.kontur.host/selenium-practice/
// Ввести в поле корректный емаил
// Нажать кнопку "Подобрать имя"
// Нажать на ссылку "Указать другой емаил"
// Проверить что поле емайла очищенно
// Убедиться, что кнопка "Подбрать имя" появилась и сслыка "Указать другой емаил" исчезла
   
//перейти по урлу
    driver.Navigate().GoToUrl(SeleniumLesson);

//Указать алрес емайла 
    var email = "test1@mail.ru";
    driver.FindElement(emailInputLokator).SendKeys(email);
//Нажать кнопку
    driver.FindElement(buttonWruteToMeLokator).Click();
//Кликнуть ссылку "указать другой емаил"
    driver.FindElement(By.LinkText("указать другой e-mail")).Click();
//Проверим что поле очищено
    Assert.IsEmpty(driver.FindElement(emailInputLokator).Text, "Ожидали что поле для ввода емейла очистится");
//Проверим что кнопка появилась
    Assert.IsTrue(driver.FindElement(buttonWruteToMeLokator).Displayed, "Ожидали, что кнопка 'Подобрать имя' отображается");
//Проверим что исчезла ссылка "указать другой емаил"
    Assert.AreEqual(0, driver.FindElements(By.LinkText("указать другой e-mail")).Count);
}

[TearDown]
public void TearDown()
{
driver.Quit();
}
}