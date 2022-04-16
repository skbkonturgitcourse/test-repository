
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class FirstSeleniumTests
{

        [SetUp]
        public void SetUp()
        {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                driver = new ChromeDriver(options);
        }
        
        private static ChromeDriver driver;
        private string randomizerNameUrl = "https://qa-course.kontur.host/selenium-practice/";
        private string correctEmail = "1@m.ru";
        private string wrongEmail = "12345";
        private string messageEmptyEmailInput = "Введите email";
        private string messageWrongEmailInput = "Некорректный email";
        private By emailInputLokator = By.Name("email");
        private By buttonSendMeLokator = By.Id("sendMe");
        private By formErrorLokator = By.ClassName("form-error");
        private By resultTextLokator = By.ClassName("result-text");
        private By radioButtonBoyLokator = By.Id("boy");
        private By radioButtonGirlLokator = By.Id("girl");
        private By linkAnotherLocationLokator = By.LinkText("указать другой e-mail");
        private By yourEmailLokator = By.ClassName("your-email");
        private By formLokator = By.Id("form");
        private By textFormQuestionLokator1 = By.CssSelector("#form > div:nth-child(2)");
        private By textFormQuestionLokator2 = By.CssSelector("#form > div:nth-child(4)");
        private By subtitleBoldLokator = By.ClassName("subtitle-bold");
        private string boyText = "мальчик";
        private string girlText = "девочка";
        private string genderText = "Кто у тебя?";
        private string subtitleBoldText = "Мы подберём имя для твоего попугайчика!";
        private string whatsYourEmailText = "На какой e-mail прислать варианты имён?";
        private string nameForYourBoyText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
        private string nameForYourGirlText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";

        private bool CheckElementsBeforeSendEmail()
        {
                Assert.True(driver.FindElement(emailInputLokator).Displayed, $"Ожидали поле ввода email");
                Assert.True(driver.FindElement(emailInputLokator).Text.Equals(""), $"Ожидали пустое поле ввода email");
                Assert.True(driver.FindElement(buttonSendMeLokator).Displayed, $"Ожидали кнопку");
                Assert.True(driver.FindElement(buttonSendMeLokator).Text.Contains("ПОДОБРАТЬ ИМЯ"), "Ожидали текст кнопки с uppercase");
                Assert.False(driver.FindElement(formErrorLokator).Displayed, "Ожидали, что сообщение об ошибке ввода email не появилось");
                return true;
        }

        private bool CheckElementsAfterSendEmail()
        {
                Assert.True(driver.FindElement(resultTextLokator).Displayed,
                        "Ожидали текст с результатом после нажатия кнопки");
                Assert.True(driver.FindElement(yourEmailLokator).Displayed,
                        "Ожидали текст с email, на который было отправлено сообщение");
                Assert.True(driver.FindElement(yourEmailLokator).Text.Contains(correctEmail),
                        $"Ожидали email '{correctEmail}'");
                Assert.True(driver.FindElement(linkAnotherLocationLokator).Displayed, 
                        "Ожидали гиперссылку");
                Assert.True(driver.FindElement(linkAnotherLocationLokator).Text.Contains("указать другой e-mail"),
                        "Ожидали гиперссылку с текстом 'указать другой e-mail'");
                return true;
        }

        [Test]
        public void CheckForElements()
        {
                driver.Navigate().GoToUrl(randomizerNameUrl);
                Assert.Multiple(() =>
                {
                        Assert.True(driver.FindElement(formLokator).Displayed, 
                                "Ожидали, что форма отображается");
                        Assert.True(driver.FindElement(subtitleBoldLokator).Displayed, 
                                "Ожидали, что заголовок формы отображается");
                        Assert.True(driver.FindElement(subtitleBoldLokator).Text
                                .Contains(subtitleBoldText), 
                                $"Ожидали текст '{subtitleBoldText}'");
                        Assert.True(driver.FindElement(textFormQuestionLokator1).Displayed, 
                                $"Ожидали, что текст '{genderText}' отображается");
                        Assert.True(driver.FindElement(textFormQuestionLokator1).Text.Contains(genderText), $"Ожидали текст '{genderText}'");
                        Assert.True(driver.FindElement(radioButtonGirlLokator).Displayed, 
                                $"Ожидали, что радиокнопка '{girlText}' отображается");
                        Assert.True(driver.FindElement(radioButtonBoyLokator).Displayed, 
                                $"Ожидали, что радиокнопка '{boyText}' отображается");
                        Assert.True(driver.FindElement(radioButtonBoyLokator).Selected, 
                                $"Ожидали, что радиокнопка '{boyText}' выбрана");
                        Assert.True(driver.FindElement(By.CssSelector("#form > div.parrot > label:nth-child(2)")).Text
                                .Contains(boyText), $"Ожидали текст '{boyText}' ");
                        Assert.True(driver.FindElement(By.CssSelector("#form > div.parrot > label:nth-child(4)")).Text
                                .Contains(girlText), $"Ожидали текст '{girlText}'");
                        Assert.True(driver.FindElement(textFormQuestionLokator2).Displayed, 
                                $"Ожидали что текст '{whatsYourEmailText}' отображается");
                        Assert.True(driver.FindElement(textFormQuestionLokator2).Text
                                .Contains(whatsYourEmailText), $"Ожидали текст '{whatsYourEmailText}'");
                        Assert.True(CheckElementsBeforeSendEmail());
                });

        }
        
        [Test]
        public void CheckEmptyEmailInput()
        {
                
                driver.Navigate().GoToUrl(randomizerNameUrl);
                driver.FindElement(buttonSendMeLokator).Click();
                Assert.True(driver.FindElement(formErrorLokator).Displayed, 
                        $"Ожидали, что сообщение об ошибке ввода email не появилось");
                Assert.True(driver.FindElement(formErrorLokator).Text.Contains(messageEmptyEmailInput), 
                        $"Ожидали сообщение об отправке пустого поля email");
                
        }
        
        [Test]
        public void CheckWrongEmailInput()
        {
                driver.Navigate().GoToUrl(randomizerNameUrl);
                driver.FindElement(emailInputLokator).SendKeys(wrongEmail);
                driver.FindElement(buttonSendMeLokator).Click();
                Assert.True(driver.FindElement(formErrorLokator).Displayed, 
                        $"Ожидали, что сообщение об ошибке ввода email не появилось");
                Assert.True(driver.FindElement(formErrorLokator).Text.Contains(messageWrongEmailInput), 
                        $"Ожидали сообщение об некорректном вводе email");
        }
        
        [Test]
        public void CheckSendResultForBoy()
        {
                driver.Navigate().GoToUrl(randomizerNameUrl);
                Assert.True(driver.FindElement(radioButtonBoyLokator).Selected, 
                        $"Ожидали, что радиокнопка '{boyText}' выбрана");
                driver.FindElement(emailInputLokator).SendKeys(correctEmail);
                driver.FindElement(buttonSendMeLokator).Click();
                Assert.True(CheckElementsAfterSendEmail(), 
                        "Часть элементов формы не появилась после отправки email");
                Assert.True(driver.FindElement(resultTextLokator).Text.Contains(nameForYourBoyText),
                        $"Ожидали текст '{nameForYourBoyText}'" + $"Вернулось '{driver.FindElement(resultTextLokator).Text}'");
                driver.FindElement(linkAnotherLocationLokator).Click();
                Assert.True(CheckElementsBeforeSendEmail(), "Часть элементов формы не появилась после нажатия 'указать другой email'");
        }
        
        
        [Test]
        public void CheckSendResultForGirl()
        {
                driver.Navigate().GoToUrl(randomizerNameUrl);
                Assert.True(driver.FindElement(emailInputLokator).Displayed, 
                        $"Ожидали поле ввода email");
                Assert.True(driver.FindElement(buttonSendMeLokator).Displayed, 
                        $"Ожидали кнопку ");
                driver.FindElement(radioButtonGirlLokator).Click();
                Assert.True(driver.FindElement(radioButtonGirlLokator).Selected, 
                        $"Ожидали, что радиокнопка '{girlText}' выбрана");
                driver.FindElement(emailInputLokator).SendKeys(correctEmail);
                driver.FindElement(buttonSendMeLokator).Click();
                Assert.True(CheckElementsAfterSendEmail(), 
                        "Часть элементов формы не появилась после отправки email");
                Assert.True(driver.FindElement(resultTextLokator).Text.Contains(nameForYourGirlText),
                        $"Ожидали текст '{nameForYourGirlText}'" + $"Вернулось '{driver.FindElement(resultTextLokator).Text}'");
                driver.FindElement(linkAnotherLocationLokator).Click();
                Assert.True(CheckElementsBeforeSendEmail(), 
                        "Часть элементов формы не появилась после нажатия 'указать другой email'");
        }
  
        [TearDown]
        public void TearDown()
        {
                driver.Quit();
        }
}