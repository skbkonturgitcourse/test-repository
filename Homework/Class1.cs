using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homework;

public class HomeworkTest

{
    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Minimize();
        driver.Navigate().GoToUrl(url_Site);
    }
    
    private ChromeDriver driver;
    private string correct_Email = "test@test.com";
    private string url_Site = "https://qa-course.kontur.host/selenium-practice/";
    private readonly By _emailInputLocator = By.Name("email");
    private readonly By _pickupNameButtonLocator = By.Id("sendMe");
    private readonly By _yourEmailLocator = By.ClassName("your-email");
    private readonly By _resultTextLocator = By.XPath("//*[@id='resultTextBlock']/div");
    
    private void CorrectEmailEnter(string correctEMail)
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        emailInput.SendKeys(correctEMail);
        var pickupNameButton = driver.FindElement(_pickupNameButtonLocator);
        pickupNameButton.Click();
    }
    
    private void ErrorAssert()
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        var pickupNameButton = driver.FindElement(_pickupNameButtonLocator);
        pickupNameButton.Click();
        var formError = driver.FindElement(By.ClassName("form-error"));
        var formErrorColor = formError.GetCssValue("color");
        var errorEmailInput = emailInput.GetAttribute("class");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(formError.Displayed, "Сообщение об ошибке ввода электронной почты не отображается");
            Assert.AreEqual("error", errorEmailInput,"Поле ввода электронной почты не выделено рамкой");
            Assert.AreEqual("Некорректный email", formError.Text, "Неверный текст ошибки. Ожидаемая ошибка - 'Некорректный email'");
            Assert.AreEqual("rgba(217, 21, 21, 1)", formErrorColor, "Сообщение об ошибке ввода электронной почты выделено неверным цветом. Ожидаемый цвет - '#d91515' или 'rgba(217 21 21 1)'");
        });
    }

    private void UI_Logo()
    {
        var logo = driver.FindElement(By.ClassName("logo")); 
        Assert.Multiple(() =>
        {
            Assert.IsTrue(logo.Displayed, "Логотип 'Контур' не отображается");
            Assert.AreEqual(41, logo.Size.Height, "Высота логотипа 'Контур' неверная. Ожидаемая высота - 41px");
            Assert.AreEqual(135, logo.Size.Width, "Ширина логотипа 'Контур' неверная. Ожидаемая ширина - 135px");
        });    
    }

    private void UI_Main_Content()
    {
        var mainContent = driver.FindElement(By.ClassName("main-content"));
        var mainContentBackgroundPath = mainContent.GetCssValue("background-image");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(mainContent.Displayed, "Картинка на заднем фоне не отображается");
            Assert.That(mainContentBackgroundPath, Does.Contain("https://qa-course.kontur.host/selenium-practice/img/parrot-bg.jpg"), "Картинка на заднем фоне неверная. Ожидаемая картинка - 'parrot-bg.jpg'");
        });
    }

    private void UI_Title()
    {
        var title = driver.FindElement(By.ClassName("title"));
        var titleFont = title.GetCssValue("font-weight");
        var titleSize = title.GetCssValue("font-size");
        var titleColor = title.GetCssValue("color");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(title.Displayed, "Заголовок 'Не знаешь как назвать?' не отображается");
            Assert.AreEqual(700, Int32.Parse(titleFont), "Жирность шрифта заголовка 'Не знаешь как назвать?' неверная. Ожидаемая жирность - 700");
            Assert.AreEqual("72px", titleSize, "Размер шрифта заголовка 'Не знаешь как назвать?' неверен. Ожидаемый размер - 72px");
            Assert.AreEqual("rgba(240, 189, 25, 1)", titleColor, "Цвет шрифта заголовка 'Не знаешь как назвать?' неверен. Ожидаемый цвет - '#f0bd19' или 'rgba(240 189 25 1)'");
            Assert.AreEqual("Не знаешь как назвать?", title.Text, "Текст заголовка 'Не знаешь как назвать?' неверен. Ожидаемый текст - 'Не знаешь как назвать?'");
        });
    }
    private void UI_Lesson_Course()
    {
        var lessonCourse = driver.FindElement(By.ClassName("lesson-source"));
        var lessonCourseFont = lessonCourse.GetCssValue("font-weight");
        var lessonCourseSize = lessonCourse.GetCssValue("font-size");
        var lessonCourseColor = lessonCourse.GetCssValue("color");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(lessonCourse.Displayed, "Название курса 'Тестирование программного обеспечения' не отображается");
            Assert.AreEqual("Тестирование программного обеспечения", lessonCourse.Text, "Текст названия курса 'Тестирование программного обеспечения' неверен. Ожидаемый текст - 'Тестирование программного обеспечения'");
            Assert.AreEqual(500, Int32.Parse(lessonCourseFont), "Жирность шрифта названия программного курса 'Тестирование программного обеспечения' неверна. Ожидаемая жирность - 500");
            Assert.AreEqual("12px", lessonCourseSize, "Размер шрифта названия курса 'Тестирование программного обеспечения' неверен. Ожидаемый размер шрифта - 12px");
            Assert.AreEqual("rgba(101, 113, 149, 1)", lessonCourseColor, "Цвет шрифта названия курса 'Тестирование программного обеспечения' неверен. Ожидаемый цвет шрифта - '#657195' или 'rgba(101 113 149 1)'");
        });    
    }

    private void UI_Subtitle()
    {
        var subtitle = driver.FindElement(By.ClassName("subtitle-bold"));
        var subtitleFont = subtitle.GetCssValue("font-weight");
        var subtitleSize = subtitle.GetCssValue("font-size");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(subtitle.Displayed, "Подзаголовок 'Мы подберём имя для твоего попугайчика!' не отображается");
            Assert.AreEqual("Мы подберём имя для твоего попугайчика!", subtitle.Text, "Текст подзаголовка 'Мы подберём имя для твоего попугайчика!' некорректен. Ожидаемый текст - 'Мы подберём имя для твоего попугайчика!'");
            Assert.AreEqual(500, Int32.Parse(subtitleFont), "Жирность шрифта подзаголовка 'Мы подберём имя для твоего попугайчика!' неверная. Ожидаемая жирность - 500");
            Assert.AreEqual("20px", subtitleSize, "Размер шрифта подзаголовка 'Мы подберём имя для твоего попугайчика!' неверен. Ожидаемый размер шрифта - 20px");
        });    
    }

    private void UI_GenderForm_Question()
    {
        var genderFormQuestion = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/form/div[2]"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(genderFormQuestion.Displayed, "Вопрос о поле попугайчика 'Кто у тебя?' не отображается");
            Assert.AreEqual("Кто у тебя?", genderFormQuestion.Text, "Текст вопроса о поле попугайчика 'Кто у тебя?' некорректен. Ожидаемый текст - 'Кто у тебя?'");
        });
    }

    private void UI_EmailForm_Question()
    {
        var emailFormQuestion = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/form/div[4]"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(emailFormQuestion.Displayed, "Вопрос 'На какой e-mail прислать варианты имён?' не отображается");
            Assert.AreEqual("На какой e-mail прислать варианты имён?", emailFormQuestion.Text, "Текст вопроса 'На какой e-mail прислать варианты имён?' неверный. Ожидаемый текст - 'На какой e-mail прислать варианты имён?'");
        });    
    }

    private void UI_MaleGender_Text()
    {
        var maleGenderText = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/form/div[3]/label[1]"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(maleGenderText.Displayed, "Текст 'мальчик' в выборе пола попугайчика не отображается");
            Assert.AreEqual("мальчик", maleGenderText.Text, "Текст 'мальчик' в выборе пола попугайчика неверное. Ожидаемый текст - 'мальчик'");
        });
    }

    private void UI_FemaleGender_Text()
    {
        var femaleGenderText = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/form/div[3]/label[2]"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(femaleGenderText.Displayed, "Текст 'девочка' в выборе пола попугайчика не отображается");
            Assert.AreEqual("девочка", femaleGenderText.Text, "Текст 'девочка' в выборе пола попугайчика неверное. Ожидаемый текст - 'девочка'");
        });
    }

    private void UI_EmailInput()
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        var emailInputPlaceholder = emailInput.GetAttribute("placeholder");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(emailInput.Displayed, "Поле ввода электронной почты не отображается");
            Assert.AreEqual(300, emailInput.Size.Width, "Ширина поля ввода электронной почты неверная. Ожидаемая ширина - 300px");
            Assert.AreEqual(40, emailInput.Size.Height, "Высота поля ввода электронной почты неверная. Ожидаемая ширина - 40px");
            Assert.AreEqual("e-mail", emailInputPlaceholder, "Поле ввода электронной почты не содержит подписи 'E-MAIL'. Ожидаемая подпись - 'E-MAIL'");
        });
    }

    private void UI_MainForm()
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        var mainForm = driver.FindElement(By.ClassName("main-form"));
        var mainFormBackgroundColor = mainForm.GetCssValue("background-color");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(mainForm.Displayed, "Фоновая форма не отображается");
            Assert.AreEqual("rgba(255, 255, 255, 1)", mainFormBackgroundColor, "У фоновой формы неверный цвет фона. Ожидаемый цвет фона - '#ffffff' или 'rgba(255 255 255 1)'");
            Assert.Greater(mainForm.Size.Width, emailInput.Size.Width, "Ширина фоновой формы меньше, чем ширина поля ввода электронной почты");
        });
    }
    
    private void UI_Pickup_Button()
    {
        var pickupNameButton = driver.FindElement(_pickupNameButtonLocator);
        var buttonBackground = pickupNameButton.GetCssValue("background-color");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(pickupNameButton.Displayed, "Кнопка 'Подобрать имя' не отображается");
            Assert.AreEqual("ПОДОБРАТЬ ИМЯ", pickupNameButton.Text, "Текст кнопки 'Подобрать имя' неверный. Ожидаемый текст - 'Подобрать имя'");
            Assert.AreEqual("rgba(233, 148, 21, 1)", buttonBackground, "Цвет кнопки 'Подобрать имя' неверен. Ожидаемый цвет - '#e99415' или 'rgba(233 148 21 1)'");
        });
    }

    [TestCase("тест@тест.рф", TestName = "Cyrillic_Language")]
    [TestCase("test@test.ru", TestName = "Latin_Language")]
    [TestCase("a@b.ru", TestName = "Minimal_Length")]
    [TestCase("testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest@test.uk", TestName = "Maximum_Length")]
    [TestCase("3101010@taxi.com", TestName = "Numbers")]
    [TestCase("TEST@TEST.COM", TestName = "Capital_Latin_Letters")]
    [TestCase("ТЕСТ@ТЕСТ.РФ", TestName = "Capital_Cyrillic_Letters")]
    [TestCase("test_test@test.com", TestName = "Allowed_SpecialSymbol")]
    [TestCase("Test_123@тест.рф", TestName = "AllowedMixture")]
    public void CorrectEmailInput_SuccessfulEmailInfoMessage(string eMail)
    {
        CorrectEmailEnter(eMail);
        var yourEmail = driver.FindElement(_yourEmailLocator);
        var yourEmailFont = yourEmail.GetCssValue("font-weight");
        Assert.Multiple(() =>
        {
            Assert.IsTrue(yourEmail.Displayed, "Текст электронной почты, на которую отправилось письмо, не отображается");
            Assert.AreEqual(eMail, yourEmail.Text, "Текст электронной почты, на которую отправилось письмо, не совпадает с электронной почтой, введённой пользователем");
            Assert.AreEqual(700, Int32.Parse(yourEmailFont), "Жирность шрифта текста электронной почты, на которую отправилось письмо, неверная. Ожидаемая жирность шрифта - 700");
        });
    }

    [Test]
    public void CorrectEmail_Male_InfoMessage()
    {
        driver.FindElement(By.Id("boy")).Click();
        CorrectEmailEnter(correct_Email);
        var resultText = driver.FindElement(_resultTextLocator);
        Assert.Multiple(() =>
        {
            Assert.IsTrue(resultText.Displayed, "Информационное поле об успешной отправке имени на электронную почту не отображается");
            Assert.That(resultText.Text, Does.Contain("мальчика"), "Информационное поле об успешной отправке имени на электронную почту содержит неверный пол попугайчика. Ожидаемый пол - 'мальчика'");
            Assert.That(resultText.Text, Does.Not.Contain("девочки"), "Информационное поле об успешной отправке имени на электронную почту содержит неверный пол попугайчика. Ожидаемый пол - 'мальчика', фактический - 'девочки'");
        });   
    }
    
    [Test]
    public void CorrectEmail_Female_InfoMessage()
    {
        driver.FindElement(By.Id("girl")).Click();
        CorrectEmailEnter(correct_Email);
        var resultText = driver.FindElement(_resultTextLocator);
        Assert.Multiple(() =>
        {
            Assert.IsTrue(resultText.Displayed, "Информационное поле об успешной отправке имени на электронную почту не отображается");
            Assert.That(resultText.Text, Does.Contain("девочки"), "Информационное поле об успешной отправке имени на электронную почту содержит неверный пол попугайчика. Ожидаемый пол - 'девочки'");
            Assert.That(resultText.Text, Does.Not.Contain("мальчика"), "Информационное поле об успешной отправке имени на электронную почту содержит неверный пол попугайчика. Ожидаемый пол - 'девочки', фактический - 'мальчика'");
        });
    }
    
    [Test]
    public void CorrectEmail_WorkableAnotherEmailLink()
    {
        CorrectEmailEnter(correct_Email);
        var anotherEmail = driver.FindElement(By.Id("anotherEmail"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(anotherEmail.Displayed, "Ссылка 'указать другой e-mail' не отображается");
            Assert.AreEqual(anotherEmail.Text, "указать другой e-mail", "Текст ссылки 'указать другой e-mail' неверный. Ожидаемый текст - 'указать другой e-mail'");
        });
        anotherEmail.Click();
        Assert.Multiple(() =>
        {
            Assert.IsFalse(anotherEmail.Displayed, "Ссылка 'указать другой e-mail' отображается. Ожидаемый результат - ссылка 'указать другой e-mail' не отображается");
            Assert.IsTrue(driver.FindElement(_pickupNameButtonLocator).Displayed, "Кнопка 'Подобрать имя' не отображается. Ожидаемый результат - кнопка 'Подобрать имя' отображается");
        }); 
    }
    
    [Test]
    public void InputtedEmail_RefreshPage_NoYourEmail()
    {
        CorrectEmailEnter(correct_Email);
        var yourEmail = driver.FindElement(_yourEmailLocator);
        Assert.AreEqual(correct_Email, yourEmail.Text, "Электронная почта в информационном сообщении об успешной отправке отличается от электронной почты, введённой пользователем");
        driver.Navigate().Refresh();
        Assert.Throws<StaleElementReferenceException>(() => Assert.AreEqual(correct_Email, yourEmail.Text), "Электронная почта в информационном сообщении об успешной отправке сохранилась после обновления страницы");
    }
    
    [Test]
    public void Empty_Email_Input()
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        var pickupNameButton = driver.FindElement(_pickupNameButtonLocator);
        pickupNameButton.Click();
        var formError = driver.FindElement(By.ClassName("form-error"));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(formError.Displayed, "Сообщение об ошибке ввода электронной почты не отображается");
            Assert.AreEqual("error", emailInput.GetAttribute("class"), "Поле ввода электронной почты не выделено рамкой");
            Assert.AreEqual("Введите email", driver.FindElement(By.ClassName("form-error")).Text, "Неверный текст ошибки. Ожидаемая ошибка - 'Введите email'");
            Assert.AreEqual("rgba(217, 21, 21, 1)", formError.GetCssValue("color"), "Сообщение об ошибке ввода электронной почты выделено неверным цветом. Ожидаемый цвет - '#d91515' или 'rgba(217 21 21 1)'");
        });
    }
    
    [TestCase("_test@test.com", TestName = "FirstSpecialSymbol")]
    [TestCase("test@test_.com", TestName = "LastSpecialSymbol")]
    [TestCase("@.", TestName = "EmptyNameAndDomain")]
    [TestCase("test@test.com test@test.ru", TestName = "DoubleEmail")]
    [TestCase("testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest.com", TestName = "ExceedMaximumTotalLength")]
    [TestCase("com", TestName = "SimpleText")]
    [TestCase("testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest@test.com", TestName = "ExceedMaximumUserNameLength")]
    [TestCase("test@test.@com", TestName = "TwoAtSignSymbols")]
    public void Incorrect_Email_Input(string mail)
    {
        var emailInput = driver.FindElement(_emailInputLocator);
        emailInput.SendKeys(mail);
        ErrorAssert();
    }
    
    [Test]
    public void UI_Logo_Test()
        {
            UI_Logo();
        }

    [Test]
    public void UI_Lesson_Course_Test()
        {
            UI_Lesson_Course();
        }

    [Test]
    public void UI_MainContent_Test()
        {
            UI_Main_Content();
        }

    [Test]
    public void UI_Title_Test()
        {
            UI_Title();
        }

    [Test]
    public void UI_Subtitle_Test()
        {
            UI_Subtitle();
        }

    [Test]
    public void UI_GenderForm_Question_Test()
        {
            UI_GenderForm_Question();
        }

    [Test]
    public void UI_EmailForm_Question_Test()
        {
            UI_EmailForm_Question();
        }

    [Test]
    public void UI_MaleGenderTextTest()
        {
            UI_MaleGender_Text();
        }

    [Test]
    public void UI_FemaleGenderTextTest()
        {
            UI_FemaleGender_Text();
        }

    [Test]
    public void UI_Email_Input_Test()
        {
            UI_EmailInput();
        }

    [Test]
    public void UI_MainForm_Test()
        {
            UI_MainForm();
        }

    [Test]
    public void UI_PickupButton_Test()
        {
            UI_Pickup_Button();
        }
    
    [TestCase("Galaxy Fold")]
    [TestCase("Nexus 5")]
    [TestCase("iPhone SE")]
    [TestCase("Surface Duo")]
    public void UI_SmokeTest_InitialPage_Mobile(string mobileName)
    {
        driver.Quit();
        var options = new ChromeOptions();
        options.EnableMobileEmulation(mobileName);
        driver = new ChromeDriver(options);
        driver.Manage().Window.Minimize();
        driver.Navigate().GoToUrl(url_Site);
        UI_Logo();
        UI_Lesson_Course();
        UI_Main_Content();
        UI_Title();
        UI_Subtitle();
        UI_GenderForm_Question();
        UI_EmailForm_Question();
        UI_MaleGender_Text();
        UI_FemaleGender_Text();
        UI_EmailInput();
        UI_MainForm();
        UI_Pickup_Button();
    }
    
   [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
