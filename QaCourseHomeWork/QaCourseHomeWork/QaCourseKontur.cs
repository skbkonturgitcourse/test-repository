namespace QaCourseHomeWork;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Internal.Execution;

public class QaCourseKontur
{
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

    }
    
    private ChromeDriver driver;
    private By textTitleLocator = By.ClassName("title");//Заголовок,"Не знаешь, как назвать?" 
    private By subtitleLocator = By.ClassName("subtitle-bold");//Подзаголовок, Мы подберем имя для твоего попугайчика!
    private By radioButtonBoyLocator = By.Id("boy");
    private By textQuestionGenderLocator = By.XPath("//div[text()='Кто у тебя?']");
    private By textQuestionMailLocator = By.XPath("//div[contains(text(),'На какой')]");
    private By errorEmptyMailLocator = By.XPath("//*[text()='Введите email']");
    private By errorInvalidMailLocator = By.XPath("//*[text()='Некорректный email']");
    private By radioButtonGirlLocator = By.Id("girl");
    private By buttonPickUpNameLocator = By.Id("sendMe");
    private By resultTextLocator = By.ClassName("result-text");
    private By linkAnotherEmailLocator = By.LinkText("указать другой e-mail");
    private By expectedEmailLocator = By.ClassName("your-email");
    private By emailInputLocator = By.Name("email");

    [Test]
    public void QaCourse_SendNameBoy_Success()
    {
        Assert.Multiple(() =>
        
        {
            var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            var email = "test@mail.ru";
            driver.FindElement(emailInputLocator).SendKeys(email);//Найти поле ввода почты и указать почту
            driver.FindElement(buttonPickUpNameLocator).Click();//Найти кнопку 'Подобрать имя' и нажать на нее
            Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
            Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после отправки почты");//Найти текст после нажатия кнопки и проверить содержимое 
            Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
            Assert.AreEqual(email, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что указана та почта, которую ввели
            Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed);//Найти ссылку указать другую почту
        });  
    }
    

        [Test]
    public void QaCourse_SendNameGirl_Success()
    {
        Assert.Multiple(() =>
        
            {
        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
        var email = "test@mail.ru";
        driver.FindElement(radioButtonGirlLocator).Click(); //Найти переключатель 'девочка' и нажать на него
        driver.FindElement(emailInputLocator).SendKeys(email); //Найти поле ввода почты и указать почту
        driver.FindElement(buttonPickUpNameLocator).Click(); //Найти кнопку 'Подобрать имя' и нажать на нее
        Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после нажатия кнопки 'Подобрать имя'");//Найти текст и проверить сожержимое текста
        Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
        Assert.AreEqual(email, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что для отправки указана введенная почта 
        Assert.IsTrue(driver.FindElement(linkAnotherEmailLocator).Displayed);//Найти ссылку указать другую почту
            });  
    }

    [Test]
    public void QaCourse_ClickAnotherMailBoy_Success()
    {   
        Assert.Multiple(() =>
        
            {
        var expectedResultText = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
        var email = "test@mail.ru";
        driver.FindElement(emailInputLocator).SendKeys(email);//Найти поле ввода почты и указать почту
        driver.FindElement(buttonPickUpNameLocator).Click();//Найти кнопку 'Подобрать имя' и нажать на нее
        Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после нажатия кнопки 'Подобрать имя'");
        Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
        Assert.AreEqual(email, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что для отправки указана введенная почта
        driver.FindElement(linkAnotherEmailLocator).Click(); //Кликнуть по ссылке 'указать другой емаил'
        Assert.IsEmpty(driver.FindElement(emailInputLocator).Text, "Ожидали, что поле ввода емейла очистится");//проверить что поле ввода емейла очистилось
        Assert.IsTrue(driver.FindElement(buttonPickUpNameLocator).Displayed, "Ожидали, что кнопка 'Подобрать имя' отображается");//проверить что кнопка 'Подобрать имя' отображается
        Assert.AreEqual(expected:0, actual:driver.FindElements(linkAnotherEmailLocator).Count); //проверить что исчезла ссылка 'указать другой емаил'
            }); 
            }
    
    [Test]
    public void QaCourse_ClickAnotherMailGirl_Success()
    {    
        Assert.Multiple(() =>
        
            {
        var expectedResultText = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
        var email = "test@mail.ru";
        driver.FindElement(radioButtonGirlLocator).Click(); //Найти переключатель 'девочка' и нажать на него
        driver.FindElement(emailInputLocator).SendKeys(email);//Найти поле ввода почты и указать почту
        driver.FindElement(buttonPickUpNameLocator).Click();//Найти кнопку 'Подобрать имя' и нажать на нее
        Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
        Assert.AreEqual(expectedResultText, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после нажатия кнопки 'Подобрать имя'");
        Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
        Assert.AreEqual(email, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что для отправки указана введенная почта
        driver.FindElement(linkAnotherEmailLocator).Click(); //Кликнуть по ссылке 'указать другой емаил'
        Assert.IsEmpty(driver.FindElement(emailInputLocator).Text, "Ожидали, что поле ввода емейла очистится");//проверить что поле ввода емейла очистилось
        Assert.IsTrue(driver.FindElement(buttonPickUpNameLocator).Displayed, "Ожидали, что кнопка 'Подобрать имя' отображается");//проверить что кнопка 'Подобрать имя' отображается
        Assert.AreEqual(expected:0, actual:driver.FindElements(linkAnotherEmailLocator).Count); //проверить что исчезла ссылка 'указать другой емаил'
            }); 
    
            }
    
    
    [Test]
    public void QaCourse_EmptyMailBoy_Error()
    
    {
        Assert.Multiple(() =>
        {
        var expectedResultText = "Введите email";
        driver.FindElement(buttonPickUpNameLocator).Click();//Найти и нажать на кнопку 'Подобрать имя'
        Assert.IsTrue(driver.FindElement(errorEmptyMailLocator).Displayed);
        Assert.AreEqual(expectedResultText, driver.FindElement(errorEmptyMailLocator).Text,"Неверный текст ошибки");//Найти текст ошибки и проверить содержимое 
            }); 
            } 
    
    [Test]
    public void QuCourse_InvalidMailBoy_Error()
    
    {
        Assert.Multiple(() =>
            {
        var expectedResultText = "Некорректный email";
        var email = "testlmail.ru";
        
            driver.FindElement(emailInputLocator).SendKeys(email); //Найти поле ввода почты и указать почту
            driver.FindElement(buttonPickUpNameLocator).Click(); //Найти кнопку 'Подобрать имя' и нажать на нее
            Assert.IsTrue(driver.FindElement(errorInvalidMailLocator).Displayed);
            Assert.AreEqual(expectedResultText, driver.FindElement(errorInvalidMailLocator).Text, "Неверный текст ошибки"); //Найти текст ошибки и проверить содержимое 

            }); 
            }
    
    [Test]
    public void QaCourse_SendingNameBoyAfterBugFixEmrtyMail_Success()
    
    {
        Assert.Multiple(() =>
        {
            var expectedResultTextError = "Введите email";
            var email = "test@mail.ru";
            var expectedResulTextSuccess = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            
            driver.FindElement(buttonPickUpNameLocator).Click();//Найти и нажать на кнопку 'Подобрать имя'
            Assert.IsTrue(driver.FindElement(errorEmptyMailLocator).Displayed);
            Assert.AreEqual(expectedResultTextError, driver.FindElement(errorEmptyMailLocator).Text,"Неверный текст ошибки");//Найти текст ошибки и проверить содержимое 
            driver.FindElement(emailInputLocator).SendKeys(email);//Найти поле ввода почты и указать почту
            driver.FindElement(buttonPickUpNameLocator).Click();//Найти кнопку 'Подобрать имя' и нажать на нее
            Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
            Assert.AreEqual(expectedResulTextSuccess, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после нажатия кнопки 'Подобрать имя'");
            Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
            Assert.AreEqual(email, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что для отправки указана введенная почта
        }); 
    }  
    [Test]
    public void QaCourse_SendingNameBoyAfterCorrectingErrorInMail_Success()
    
    {
        Assert.Multiple(() =>
        {
            var expectedResultText = "Некорректный email";
            var emailError = "test@mail";
            var emailFix =".ru";
            var emailSuccess = "test@mail.ru";
            var expectedResulTextSuccess = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            
            driver.FindElement(emailInputLocator).SendKeys(emailError); //Найти поле ввода почты и указать почту
            driver.FindElement(buttonPickUpNameLocator).Click(); //Найти кнопку 'Подобрать имя' и нажать на нее
            Assert.IsTrue(driver.FindElement(errorInvalidMailLocator).Displayed);
            driver.FindElement(emailInputLocator).SendKeys(emailFix);//Найти поле ввода почты и внести исправление в почту
            driver.FindElement(buttonPickUpNameLocator).Click();//Найти кнопку 'Подобрать имя' и нажать на нее
            Assert.IsTrue(driver.FindElement(resultTextLocator).Displayed);
            Assert.AreEqual(expectedResulTextSuccess, driver.FindElement(resultTextLocator).Text,"Неверный текст сообщения после нажатия кнопки 'Подобрать имя'");
            Assert.IsTrue(driver.FindElement(expectedEmailLocator).Displayed);
            Assert.AreEqual(emailSuccess, driver.FindElement(expectedEmailLocator).Text,"Неверный емейл на который будем отвечать");//Проверить что для отправки указана введенная почта
        }); 
    }  
    
    [Test]
    public void AllElementsOnHomePage()
    {
        Assert.Multiple(() =>
        
        {
            Assert.IsTrue(driver.FindElement(textTitleLocator).Displayed, "Заголовок 'Не знаешь как назвать?' - отображается"); 
            Assert.IsTrue(driver.FindElement(subtitleLocator).Displayed, "Подзаголовок 'Мы подберем имя' - отображается");
            Assert.IsTrue(driver.FindElement(radioButtonBoyLocator).Displayed, "RadioButton Мальчик - отображается");
            Assert.IsTrue(driver.FindElement(radioButtonGirlLocator).Displayed, "RadioButton Девочка - отображается");
            Assert.IsTrue(driver.FindElement(textQuestionGenderLocator).Displayed, "Вопрос 'кто у тебя' - отображается");
            Assert.IsTrue(driver.FindElement(textQuestionMailLocator).Displayed, "Вопрос 'На какой емейл' - отображается");
            Assert.IsTrue(driver.FindElement(emailInputLocator).Displayed, "Поле ввода почты - отображается");
            Assert.IsTrue(driver.FindElement(buttonPickUpNameLocator).Displayed, "Кнопка 'Подобрать имя' - отображается");
        
        });  
    }

    [TearDown]
    public void TearDown()
    {
       driver.Quit();
    }
}