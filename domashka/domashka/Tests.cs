using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace domashka
{
    public class Tests
    {
        private ChromeDriver driver;
        private readonly By _emailInput = By.Name("email");
        private readonly By _sendMeButton = By.Id("sendMe");
        private const string email = "qastudent@mail.ru";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
        }

        [Test]
        public void ParrotNames_For_Boy_Post_Success()
        {
            var boyCheckbox = driver.FindElement(By.Id("boy"));
            var emailInput = driver.FindElement(this._emailInput);
            var sendButton = driver.FindElement(_sendMeButton);
            
            boyCheckbox.Click();
            emailInput.SendKeys(email);
            sendButton.Click();
            
            var resultText = driver.FindElement(By.ClassName("result-text")).Text;
            var yourEmail = driver.FindElement(By.ClassName("your-email")).Text;
            Assert.Multiple(() =>
            {
                 Assert.AreEqual("Хорошо, мы пришлём имя для вашего мальчика на e-mail:", resultText );
                 Assert.AreEqual(email,yourEmail);
            });
           
            driver.Quit();
        }
        
        [Test]
        public void ParrotNames_For_Girl_Post_Success()
        {
            var girlCheckbox = driver.FindElement(By.Id("girl"));
            var emailInput = driver.FindElement(this._emailInput);
            var sendButton = driver.FindElement(_sendMeButton);
            
            girlCheckbox.Click();
            emailInput.SendKeys(email);
            sendButton.Click();
            
            var resultText = driver.FindElement(By.ClassName("result-text")).Text;
            var yourEmail = driver.FindElement(By.ClassName("your-email")).Text;
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Хорошо, мы пришлём имя для вашей девочки на e-mail:", resultText );
                Assert.AreEqual(email,yourEmail);
            });
            
            driver.Quit();
        }

        [Test]
        public void Email_Unset_Correct()
        {
            var girlCheckbox = driver.FindElement(By.Id("girl"));
            var emailInput = driver.FindElement(this._emailInput);
            var sendButton = driver.FindElement(_sendMeButton);

            girlCheckbox.Click();
            emailInput.SendKeys(email);
            sendButton.Click();

            var anotherEmail = driver.FindElement(By.Id("anotherEmail"));
            anotherEmail.Click();
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(emailInput.Text, "Ожидаем, что поле станет пустым");
                Assert.IsTrue(girlCheckbox.Selected);
            });

            driver.Quit();
        }

        [Test]
        public void Incorrect_Email_Text()
        {
            var emailiNput = driver.FindElement(this._emailInput);
            var sendButton = driver.FindElement(_sendMeButton);

            emailiNput.SendKeys("Моя почта");
            sendButton.Click();

            var errorText = driver.FindElement(By.ClassName("form-error")).Text;
            var errorBorder = driver.FindElement(By.ClassName("error"));

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Некорректный email", errorText);
                Assert.IsTrue(errorBorder.Displayed);

            });

            driver.Quit();
        }

        [Test]
            public void Empty_EmailInput_Error()
            {
                
                var sendButton = driver.FindElement(_sendMeButton);
                
                sendButton.Click();

                var errorText = driver.FindElement(By.ClassName("form-error")).Text;
                var errorBorder = driver.FindElement(By.ClassName("error"));
                
                Assert.Multiple((() =>
                {
                    Assert.AreEqual("Введите email", errorText);
                    Assert.IsTrue(errorBorder.Displayed);
                    
                }));
                
                driver.Quit();
            }

        }
     
}