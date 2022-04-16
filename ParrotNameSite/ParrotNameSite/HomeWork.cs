using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParrotNameSite
{
    public class HomeWork
    {
        private ChromeDriver driver;

        private By radioButtonMaleLocator = By.Id("boy");
        private By radioButtonFemaleLocator = By.Id("girl");
        private By emailInputLocator = By.Name("email");
        private By buttonGetNameLocator = By.Id("sendMe");
        private By resultTextLocator = By.ClassName("result-text");
        private By checkEmailLocator = By.ClassName("your-email");
        private By linkAnotherEmailLocator = By.Id("anotherEmail");
        private string urlParrotNaming = "https://qa-course.kontur.host/selenium-practice/";
        private string testMail = "test@test.ru";
        private By errorMessage = By.ClassName("form-error");
        
        [SetUp]

        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            
        }
        //Проверка нажатия радиобаттон м
        //Проверка нажатия радиобаттон ж
        //Проверка отправки письма м
        //Проверка отправки письма ж
        //Проверка результирующего текста м
        //Проверка результирующего текста ж
        //Проверка ссылки м
        //Проверка ссылки ж
        //Проверка почты




        [Test]

        public void ParrotName_RadioMale_Click_Enabled()

        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonFemaleLocator).Click();
            driver.FindElement(radioButtonMaleLocator).Click();
            Assert.True(driver.FindElement(radioButtonMaleLocator).Selected,"Радиобаттон 'мальчик' не выбирается");
        }
        
        [Test]
        
        public void ParrotName_RadioFemale_Click_Enabled()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonMaleLocator).Click();
            driver.FindElement(radioButtonFemaleLocator).Click();
            Assert.True(driver.FindElement(radioButtonFemaleLocator).Selected,"Радиобаттон 'девочка' не выбирается");
        }
        
        [Test]
        
        public void ParrotName_SendEmailMale_Success()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonMaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            Assert.Multiple((() =>
            {
                Assert.False(driver.FindElement(buttonGetNameLocator).Displayed,
                            "Кнопка 'Подобрать имя' должна пропасть при отправке письма");
                Assert.True(driver.FindElement(resultTextLocator).Displayed,
                            "При отправке письма должен появиться подтверждающий текст");
                Assert.True(driver.FindElement(checkEmailLocator).Displayed,
                            "При отправке письма должен появиться email, на который отправлено письмо");
                Assert.True(driver.FindElement(linkAnotherEmailLocator).Displayed,
                            "При отправке письма должна появляться ссылка 'Указать другой e-mail'");
            }));
        }
        
        [Test]
        
        public void ParrotName_SendEmailFemale_Success()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonFemaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            Assert.Multiple((() =>
            {
                Assert.False(driver.FindElement(buttonGetNameLocator).Displayed,
                    "Кнопка 'Подобрать имя' должна пропасть при отправке письма");
                Assert.True(driver.FindElement(resultTextLocator).Displayed,
                    "При отправке письма должен появиться подтверждающий текст");
                Assert.True(driver.FindElement(checkEmailLocator).Displayed,
                    "При отправке письма должен появиться email, на который отправлено письмо");
                Assert.True(driver.FindElement(linkAnotherEmailLocator).Displayed,
                    "При отправке письма должна появляться ссылка 'Указать другой e-mail'");
            }));
        }
        
        [Test]
        
        public void ParrotName_ResultTextMale_Correct()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonMaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            var maleResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            Assert.AreEqual(maleResultText,driver.FindElement(resultTextLocator).Text,"Результирующий текст отличается от ожидаемого");
        }
        
        [Test]
        
        public void ParrotName_ResultTextFemale_Correct()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonFemaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            var maleResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            Assert.AreEqual(maleResultText,driver.FindElement(resultTextLocator).Text,"Результирующий текст отличается от ожидаемого");
        }
        
        [Test]
        
        public void ParrotName_LinkAnotherMail_Male_Click_Success()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonMaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            Assert.Multiple((() =>
            {
                Assert.IsEmpty(driver.FindElement(emailInputLocator).Text,"Поле должно очиститься");
                Assert.True(driver.FindElement(buttonGetNameLocator).Displayed,"Кнопка 'Подобрать имя' должна появиться");
                Assert.AreEqual(0, driver.FindElements(resultTextLocator).Count,"Результирующий текст должен был пропасть");
                Assert.AreEqual(0, driver.FindElements(checkEmailLocator).Count,"Строка проверки e-mail должна была пропасть");
                Assert.False(driver.FindElement(linkAnotherEmailLocator).Displayed,"Ссылка не должна отображаться");
            }));
        }
        
        [Test]
        
        public void ParrotName_LinkAnotherMail_Female_Click_Success()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonFemaleLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(testMail);
            driver.FindElement(buttonGetNameLocator).Click();
            driver.FindElement(linkAnotherEmailLocator).Click();
            Assert.Multiple((() =>
            {
                Assert.IsEmpty(driver.FindElement(emailInputLocator).Text,"Поле должно очиститься");
                Assert.True(driver.FindElement(buttonGetNameLocator).Displayed,"Кнопка 'Подобрать имя' должна появиться");
                Assert.AreEqual(0, driver.FindElements(resultTextLocator).Count,"Результирующий текст должен был пропасть");
                Assert.AreEqual(0, driver.FindElements(checkEmailLocator).Count,"Строка проверки e-mail должна была пропасть");
                Assert.False(driver.FindElement(linkAnotherEmailLocator).Displayed,"Ссылка не должна отображаться");
            }));
        }
        
        [Test]
        
        public void ParrotName_EmailValidation_Success()
        
        {
            driver.Navigate().GoToUrl(urlParrotNaming);
            driver.FindElement(radioButtonFemaleLocator).Click();
            var incorrectEmail = "test@test.";
            driver.FindElement(emailInputLocator).SendKeys(incorrectEmail);
            Assert.True(driver.FindElement(errorMessage).Displayed, "Кажется, что-то не так с валидацией, пропущен некорректный e-mail");
        }
        
        
        
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}