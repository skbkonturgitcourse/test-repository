using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Popugi;

public class SeleniumTests
{
    // 1. Тест на валидный email для Девочки/ Мальчика
    // 2. Проверяем, что можно вернуться к начальной форме после отправки email для Девочки/ Мальчика
    // 3. Не указываем email для Девочки/ Мальчика
    // 4. В поле email указать текст на кириллице для Девочки/ Мальчика
    // 5. Тест на невалидную почту (te..st@test.ru) для Девочки/ Мальчика
    // 6. Локальная часть длинее 64 символов (1234567890123456789012345678901234567890123456789012345678901234+x@test.com) для Девочки/ Мальчика
    // 7. Тест на валидную почту с поддоменом (test@test.test.com) для Девочки/ Мальчика
    
                [SetUp]
                public void SetUp()
                {
                    var options = new ChromeOptions();
                        options.AddArgument("start-maximized");
                        driver = new ChromeDriver(options);
                }
                
                private ChromeDriver driver;
                private By emailInputLokator = By.Name("email");
                private By buttonchooseANameLokator = By.Id("sendMe"); 
                private By buttonSelectSexBLokator = By.Id("boy");
                private By buttonSelectSexGLokator = By.Id("girl");
                private By resultTextLokator = By.ClassName("result-text"); 
                private By resultEmailLokator = By.ClassName("your-email"); 
                private By formErrorLokator = By.ClassName("form-error"); 
                private By linkAnotherEmailLokator = By.LinkText("указать другой e-mail");
                private string urlNamePopug = "https://qa-course.kontur.host/selenium-practice/";
                
                [Test]
                public void NamePopug_SendEmailForG_Success() // 1. Тест на валидный email для Девочки 
                {
                        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
                        var email = "test@mail.ru";
                        driver.Navigate().GoToUrl(urlNamePopug);
                        driver.FindElement(buttonSelectSexGLokator).Click(); 
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                        driver.FindElement(buttonchooseANameLokator).Click();
                        Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected, 
                            "Выбран неверный пол попуга");
                        Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                                "Сообщение об успехе создания заявки отображается");
                        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                                "Неверное сообщение об успехе создания заявки");
                        Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                        "Неверный емейл на кторый будут отвечать");
                        Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                                "Ссылка 'Указать другой e-mail' отображается");
                }
                
                [Test]
                public void NamePopug_SendEmailForB_Success() // 1. Тест на валидный email для Мальчика 
                {
                    var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
                    var email = "test@mail.ru";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click(); 
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                        "Сообщение об успехе создания заявки отображается");
                    Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                        "Неверное сообщение об успехе создания заявки");
                    Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                        "Неверный емейл на кторый будут отвечать");
                    Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                        "Ссылка 'Указать другой e-mail' отображается");
                }
               
                [Test]
                public void NamePopug_ClicklinkAnotherEmailForG_Success() // 2. Проверяем, что можно вернуться к начальной форме после отправки email для Девочки
                {
                        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
                        var email = "test@mail.ru";
                        driver.Navigate().GoToUrl(urlNamePopug);
                        driver.FindElement(buttonSelectSexGLokator).Click();
                        driver.FindElement(emailInputLokator).SendKeys(email);
                        driver.FindElement(buttonchooseANameLokator).Click();
                        Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                            "Сообщение об успехе создания заявки отображается");
                        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                            "Неверное сообщение об успехе создания заявки");
                        driver.FindElement(linkAnotherEmailLokator).Click();
                        Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                                "Ожидали что поле для ввода емейла очистится");
                        Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                                "Ожидаем, что кнопка 'Подобрать имя' отображается");
                        Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }
                
                [Test]
                public void NamePopug_ClicklinkAnotherEmailForB_Success() // 2. Проверяем, что можно вернуться к начальной форме после отправки email для Мальчика
                {
                    var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
                    var email = "test@mail.ru";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click();
                    driver.FindElement(emailInputLokator).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed,
                        "Сообщение об успехе создания заявки отображается");
                    Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                        "Неверное сообщение об успехе создания заявки");
                    driver.FindElement(linkAnotherEmailLokator).Click();
                    Assert.IsEmpty(driver.FindElement(emailInputLokator).Text,
                        "Ожидали что поле для ввода емейла очистится");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }
                
                [Test]
                public void NamePopug_SkipEmail_G_Fall() // 3. Не указываем email для Девочки
                {
                    var expectedResultTextError = "Введите email";
                    var email = ""; 
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexGLokator).Click(); 
                    driver.FindElement(emailInputLokator).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Текст отличается от 'Введите email'"); 
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }
                
                [Test]
                public void NamePopug_SkipEmail_B_Fall() // 3. Не указываем email для Мальчика
                {
                    var expectedResultTextError = "Введите email";
                    var email = ""; 
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click(); 
                    
                    driver.FindElement(emailInputLokator).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Текст отличается от 'Введите email'"); 
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }
                
                
                [Test] 
                public void NamePopug_CyrillicTextForG_Fall() // 4. В поле email указать текст на кириллице для Девочки
                {
                    var email = "тест";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexGLokator).Click(); 
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                    
                } 
                
                [Test] 
                public void NamePopug_CyrillicTextForB_Fall() // 4. В поле email указать текст на кириллице для Мальчика
                {
                    var email = "тест";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click(); 
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                    
                }

                [Test]
                public void NamePopug_2DotEmailForG_Fall() // 5. Тест на невалидную почту (te..st@test.ru) для Девочки 
                {
                    var email = "te..st@test.ru";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexGLokator).Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected,
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }

                [Test]
                public void NamePopug_2DotEmailForB_Fall() // 5. Тест на невалидную почту (te..st@test.ru) для Мальчика 
                {
                    var email = "te..st@test.ru";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected,
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }
                
                [Test]
                public void NamePopug_LPMoreThen64SEmailForG_Fall() // 6. Локальная часть длинее 64 символов, напр.:
                    // 1234567890123456789012345678901234567890123456789012345678901234+x@test.com для Девочки 
                {
                    var email = "1234567890123456789012345678901234567890123456789012345678901234+x@test.com";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexGLokator).Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected,
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }

                [Test]
                public void NamePopug_LPMoreThen64SEmailForB_Fall() // 6. Локальная часть длинее 64 символов, напр.:
                    // 1234567890123456789012345678901234567890123456789012345678901234+x@test.com для Мальчика 
                {
                    var email = "1234567890123456789012345678901234567890123456789012345678901234+x@test.com";
                    var expectedResultTextError = "Некорректный email";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email);
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected,
                        "Выбран неверный пол попуга");
                    Assert.AreEqual(expectedResultTextError, driver.FindElement(formErrorLokator).Text,
                        "Появляется ошибка 'Некорректный email'");
                    Assert.IsTrue(driver.FindElement(buttonchooseANameLokator).Displayed,
                        "Ожидаем, что кнопка 'Подобрать имя' отображается");
                    Assert.AreEqual(0, driver.FindElements(linkAnotherEmailLokator).Count);
                }

                [Test]
                public void NamePopug_2DomainEmailForG_Success() // 7. Тест на валидную почту с поддоменом (test@test.test.com) для Девочки 
                {
                    var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
                    var email = "test@test.test.com";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexGLokator).Click(); 
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexGLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, 
                        "Сообщение об успехе создания заявки отображается");
                    Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                        "Неверное сообщение об успехе создания заявки");
                    Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                        "Неверный емейл на кторый будут отвечать");
                    Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                        "Ссылка 'Указать другой e-mail' отображается");
                }

                [Test]
                public void NamePopug_2DomainEmailForB_Success() // 7. Тест на валидную почту с поддоменом (test@test.test.com) для Мальчика 
                {
                    var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
                    var email = "test@test.test.com";
                    driver.Navigate().GoToUrl(urlNamePopug);
                    driver.FindElement(buttonSelectSexBLokator).Click(); 
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(e => e.FindElement(emailInputLokator)).SendKeys(email); 
                    driver.FindElement(buttonchooseANameLokator).Click();
                    Assert.IsTrue(driver.FindElement(buttonSelectSexBLokator).Selected, 
                        "Выбран неверный пол попуга");
                    Assert.IsTrue(driver.FindElement(resultTextLokator).Displayed, 
                        "Сообщение об успехе создания заявки отображается");
                    Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLokator).Text,
                        "Неверное сообщение об успехе создания заявки");
                    Assert.AreEqual(email, driver.FindElement(resultEmailLokator).Text,
                        "Неверный емейл на кторый будут отвечать");
                    Assert.IsTrue(driver.FindElement(linkAnotherEmailLokator).Displayed,
                        "Ссылка 'Указать другой e-mail' отображается");
                }
                
                [TearDown]
                public void TearDown()
                {
                      driver.Quit();
                }
}