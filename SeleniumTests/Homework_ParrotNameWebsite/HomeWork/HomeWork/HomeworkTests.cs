using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HomeWork
//Тест 1 - провера переключателей Мальчик-Девочка
//Тест 2 - провекра переключаталей Девочка-Мальчик
//Тест 3 - заполнение имени мальчика - найти и проверить элементы до ввода, ввести данные, найти и проверить данные после ввода 
//Тест 4 - заполнение имени девочки - найти и проверить элементы до ввода, ввести данные, найти и проверить данные после ввода
//Тест 5 - тест 1 + указать другой email
//Тест 6 - тест 2 + указать другой email
//Тест 7 - не заполнять email
//Тест 8 - ввод некорректного email - поведение валидатора


{
    public class HomeworkTests
    {
        private ChromeDriver driver;
        private By MaleRadioButtonLokator = By.CssSelector("input[value='male']");
        private By FemaleRadioButtonLokator = By.CssSelector("input[value='female']");
        private By TitleLokator = By.XPath("//*[text()='Не знаешь как назвать?']");
        private By Subtitle_boldLokator = By.ClassName("subtitle-bold");
        private By sexFormQuestionLokator = By.XPath("//*[text()='Кто у тебя?']");
        private By emailFormQuestionLokator = By.XPath("//*[text()='На какой e-mail прислать варианты имён?']");
        private By emailInputLokator = By.Name("email");
        private By sendMeButtonLokator = By.Id("sendMe");
        private By resultTextLokator = By.ClassName("result-text");
        private By yourEmailLokator = By.ClassName("your-email");
        private By anotherEmailLokator = By.LinkText("указать другой e-mail");
        private By resultTextBlockLokator = By.Id("resultTextBlock");
        private By formErrorLokator = By.ClassName("form-error");

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void Switching_BoyToGirl_RadioButtons()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            
            var radioButtonMale = driver.FindElement(MaleRadioButtonLokator);
            var radioButtonFemale = driver.FindElement(FemaleRadioButtonLokator);
            
            radioButtonFemale.Click();
            
            var FemaleAtt = radioButtonFemale.GetAttribute("Checked");
            var MaleAtt = radioButtonMale.GetAttribute("Checked");
            
           
                Assert.Multiple(() => 
                        {
                            Assert.IsNull(MaleAtt, "Атрибут checked у радио-кнопки Мальчик должен быть null");
                            Assert.IsNotNull(FemaleAtt, "Радио-кнопка Девочка не имеет аттрибута checked");
                        }
                );
                
            

        }
        
        [Test]
        public void Switching_GirlToBoy_RadioButtons()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            
            var radioButtonMale = driver.FindElement(MaleRadioButtonLokator);
            var radioButtonFemale = driver.FindElement(FemaleRadioButtonLokator);
            
            radioButtonFemale.Click();
            radioButtonMale.Click();
            
            var FemaleAtt = radioButtonFemale.GetAttribute("Checked");
            var MaleAtt = radioButtonMale.GetAttribute("Checked");
            
           
            Assert.Multiple(() => 
                {
                    Assert.IsNull(FemaleAtt, "Атрибут checked у радио-кнопки Девочка должен быть null");
                    Assert.IsNotNull(MaleAtt, "Радио-кнопка Мальчик не имеет аттрибута checked");
                }
            );
                
            

        }
        
        [Test]
        public void ParrotName_Male_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            var title = driver.FindElement(TitleLokator);
            var subbtitle_bold = driver.FindElement(Subtitle_boldLokator);
            var sexFormQuestion = driver.FindElement(sexFormQuestionLokator);
            var radioButton = driver.FindElement(MaleRadioButtonLokator);
            var emailFormQuestion = driver.FindElement(emailFormQuestionLokator);
            var EmailInput = driver.FindElement(emailInputLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            var test_email = "test@mail.ru";

            radioButton.Click();
            EmailInput.SendKeys(test_email);
            SendMeName.Click();

            var resultText = driver.FindElement(resultTextLokator);
            var yourEmail = driver.FindElement(yourEmailLokator);

            var anotherEmail = driver.FindElement(anotherEmailLokator);

            Assert.Multiple(() =>
            {
                //Проверка заголовка
                Assert.IsTrue(title.Displayed, "Ой-ой, заголовок не отображается");
                Assert.AreEqual("Не знаешь как назвать?", title.Text, "Неверный подзаголовок");

                //Проверка подзаголовка
                Assert.IsTrue(subbtitle_bold.Displayed, "Ой-ой, подзаголовок не отображается");
                Assert.AreEqual("Мы подберём имя для твоего попугайчика!", subbtitle_bold.Text,
                    "Неверный подзаголовок");

                //Проверка вопрос про пол
                Assert.IsTrue(sexFormQuestion.Displayed, "Ой-ой, вопрос про пол не отображается");
                Assert.AreEqual("Кто у тебя?", sexFormQuestion.Text, "Текст вопроса некорректен");

                //Проверка текста про почту
                Assert.IsTrue(emailFormQuestion.Displayed, "Ой-ой, про почту про почту не отображается");
                Assert.AreEqual("На какой e-mail прислать варианты имён?", emailFormQuestion.Text,
                    "Текст вопроса некорректен");

                //Проверка текста про пол
                Assert.IsTrue(resultText.Text.Contains("вашего мальчика"),
                    "Поле содержит некорректный пол, некорректно определяется скриптом");

                //Проверека введённого значения почты
                Assert.IsTrue(yourEmail.Displayed, "Текст почты не отображается");
                Assert.AreEqual(test_email, yourEmail.Text, "Выводимый текст почты не совпадает с введённым значением");

                //Проверка гиперссылки и после нажатия "Указать другой email"
                Assert.AreEqual("указать другой e-mail", anotherEmail.Text, "Текст гиперссылки некорректен");

            });

        }

        [Test]
        public void ParrotName_Female_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            var title = driver.FindElement(TitleLokator);
            var subbtitle_bold = driver.FindElement(Subtitle_boldLokator);
            var sexFormQuestion = driver.FindElement(sexFormQuestionLokator);
            var radioButton = driver.FindElement(FemaleRadioButtonLokator);
            var emailFormQuestion = driver.FindElement(emailFormQuestionLokator);
            var EmailInput = driver.FindElement(emailInputLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            var test_email = "test@mail.ru";

            radioButton.Click();
            EmailInput.SendKeys(test_email);
            SendMeName.Click();

            var resultText = driver.FindElement(resultTextLokator);
            var yourEmail = driver.FindElement(yourEmailLokator);

            var anotherEmail = driver.FindElement(anotherEmailLokator);

            Assert.Multiple(() =>
            {
                //Проверка заголовка
                Assert.IsTrue(title.Displayed, "Ой-ой, заголовок не отображается");
                Assert.AreEqual("Не знаешь как назвать?", title.Text, "Неверный подзаголовок");

                //Проверка подзаголовка
                Assert.IsTrue(subbtitle_bold.Displayed, "Ой-ой, подзаголовок не отображается");
                Assert.AreEqual("Мы подберём имя для твоего попугайчика!", subbtitle_bold.Text,
                    "Неверный подзаголовок");

                //Проверка вопрос про пол
                Assert.IsTrue(sexFormQuestion.Displayed, "Ой-ой, вопрос про пол не отображается");
                Assert.AreEqual("Кто у тебя?", sexFormQuestion.Text, "Текст вопроса некорректен");

                //Проверка текста про почту
                Assert.IsTrue(emailFormQuestion.Displayed, "Ой-ой, про почту про почту не отображается");
                Assert.AreEqual("На какой e-mail прислать варианты имён?", emailFormQuestion.Text,
                    "Текст вопроса некорректен");

                //Проверка текста про пол
                Assert.IsTrue(resultText.Text.Contains("вашей девочки"),
                    "Поле содержит некорректный пол, некорректно определяется скриптом");

                //Проверека введённого значения почты
                Assert.IsTrue(yourEmail.Displayed, "Текст почты не отображается");
                Assert.AreEqual(test_email, yourEmail.Text, "Выводимый текст почты не совпадает с введённым значением");

                //Проверка гиперссылки и после нажатия "Указать другой email"
                Assert.AreEqual("указать другой e-mail", anotherEmail.Text, "Текст гиперссылки некорректен");

            });
        }

        [Test]
        public void ParrotName_Male_AnotherEmail_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            var radioButton = driver.FindElement(MaleRadioButtonLokator);
            var EmailInput = driver.FindElement(emailInputLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            var test_email = "test@mail.ru";
            var resultTextBlock = 0;

            radioButton.Click();
            EmailInput.SendKeys(test_email);
            SendMeName.Click();

            var anotherEmail = driver.FindElement(anotherEmailLokator);

            anotherEmail.Click();

            try
            {
                driver.FindElement(resultTextBlockLokator);
            }
            catch
            {
                resultTextBlock = 0;
            }

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(EmailInput.Text, "Поле Email должно очистить после нажатия гиперссылки");
                Assert.IsFalse(anotherEmail.Displayed, "Гиперссылка 'указать другой email' не должна отображаться");
                Assert.AreEqual(0, resultTextBlock, "Инфомрационное поле об отправке должно быть удалено");

            });
        }

        [Test]
        public void ParrotName_Female_AnotherEmail_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            var radioButton = driver.FindElement(FemaleRadioButtonLokator);
            var EmailInput = driver.FindElement(emailInputLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            var test_email = "test@mail.ru";
            var resultTextBlock = 0;

            radioButton.Click();
            EmailInput.SendKeys(test_email);
            SendMeName.Click();

            var anotherEmail = driver.FindElement(anotherEmailLokator);

            anotherEmail.Click();

            try
            {
                driver.FindElement(resultTextBlockLokator);
            }
            catch
            {
                resultTextBlock = 0;
            }

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(EmailInput.Text, "Поле Email должно очистить после нажатия гиперссылки");
                Assert.IsFalse(anotherEmail.Displayed, "Гиперссылка 'указать другой email' не должна отображаться");
                Assert.AreEqual(0, resultTextBlock, "Инфомрационное поле об отправке должно быть удалено");

            });
        }

        [Test]
        public void MaleEmptyEmailGetError()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            
            var radioButton = driver.FindElement(MaleRadioButtonLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            
            radioButton.Click();
            SendMeName.Click();
            
            var EmailInput = driver.FindElement(emailInputLokator);
            var getErrorClass = EmailInput.GetAttribute("class");
            var getCSSerrorClassBorder = driver.FindElement(emailInputLokator).GetCssValue("border");
            var formError = driver.FindElement(formErrorLokator);

            Assert.Multiple((() =>
                    {
                        Assert.IsTrue(EmailInput.Displayed, "Поле для ввода Email не отображается");
                        Assert.AreEqual("error", getErrorClass, "В поле Email не отобразился class=error");
                        Assert.AreEqual("1px solid rgb(217, 21, 21)", getCSSerrorClassBorder, "Некорректное CSS форматирование границы поля Email при ошибке");
                        Assert.IsTrue(formError.Displayed, "Текст ошибки не отобразился");
                        Assert.AreEqual("Введите email", formError.Text, "Текст ошибки не соответсвует само ошибке");
                    }
                    ));
            
        }
        
        [Test]
        public void FemaleEmptyEmailGetError()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            
            var radioButton = driver.FindElement(FemaleRadioButtonLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            
            radioButton.Click();
            SendMeName.Click();
            
            var EmailInput = driver.FindElement(emailInputLokator);
            var getErrorClass = EmailInput.GetAttribute("class");
            var getCSSerrorClassBorder = driver.FindElement(emailInputLokator).GetCssValue("border");
            var formError = driver.FindElement(formErrorLokator);

            Assert.Multiple((() =>
                    {
                        Assert.IsTrue(EmailInput.Displayed, "Поле для ввода Email не отображается");
                        Assert.AreEqual("error", getErrorClass, "В поле Email не отобразился class=error");
                        Assert.AreEqual("1px solid rgb(217, 21, 21)", getCSSerrorClassBorder, "Некорректное CSS форматирование границы поля Email при ошибке");
                        Assert.IsTrue(formError.Displayed, "Текст ошибки не отобразился");
                        Assert.AreEqual("Введите email", formError.Text, "Текст ошибки не соответсвует само ошибке");
                    }
                ));
            
        }

        [Test]
        public void IncorrectEmailGetError_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            
            var EmailInput = driver.FindElement(emailInputLokator);
            var SendMeName = driver.FindElement(sendMeButtonLokator);
            string inocorrect_test_email = "билиберда137@bilberda731.точкару";
            
            EmailInput.SendKeys(inocorrect_test_email);
            SendMeName.Click();

            var getCSSerrorClassBorder = driver.FindElement(emailInputLokator).GetCssValue("border");
            var formError = driver.FindElement(formErrorLokator);
            
            Assert.Multiple(() =>
                {
                    Assert.AreEqual("1px solid rgb(217, 21, 21)", getCSSerrorClassBorder, "Некорректное CSS форматирование границы поля Email при ошибке");
                    Assert.IsTrue(formError.Displayed, "Поле с ошибкой не появилось");
                    
                }
            );
            
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}