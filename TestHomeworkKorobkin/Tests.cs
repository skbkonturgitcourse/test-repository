using System;
using System.Security.Policy;
using TestHomeworkKorobkin;
using TestHomeworkKorobkin.PageObjects;
using NUnit.Framework;
using SeleniumTests.PageObjects;

namespace SeleniumTests
{
    public class Cian : TestBase
    {
        [TestCase(Gender.Boy, TestName = "Выбираем имя мальчику, указывем валидный e-mail")]
        [TestCase(Gender.Girl, TestName = "Выбираем имя девочке, указывем валидный e-mail")]
        public void ThinkUpParrotsName_Success(Gender gender)
        {
            var mainPage = new MainPage(WebDriver);

            mainPage.FillAllFieldsAndClickNextButton(gender, ConstantsForTests.email);

            CheckRadiobuttons(gender: gender, mainPage);
            CheckSecondStep(gender: gender, email: ConstantsForTests.email,mainPage);
        }
        
        [TestCase(Gender.Girl, TestName = "Выбираем любой пол, указывем e-mail на кириллице")]
        public void ThinkUpParrotsNameKirillianEmail_Success(Gender gender)
        {
            var mainPage = new MainPage(WebDriver);
            var randomGender = GenderRandomizer();
            
            mainPage.FillAllFieldsAndClickNextButton(randomGender, ConstantsForTests.kirillianMail);

            CheckRadiobuttons(gender: randomGender, mainPage);
            CheckSecondStep(gender: randomGender, email: ConstantsForTests.kirillianMail,mainPage);
        }


        [TestCase(TestName = "Выбираем любой пол, указывем валидный e-mail, переходим по ссылке 'указать другой e-mail'")]
        public void ThinkUpParrotsClickNameAnotherEmail_Success()
        {
            
            var mainPage = new MainPage(WebDriver);
            var randomGender = GenderRandomizer();
            
            mainPage.FillAllFieldsAndClickNextButton(randomGender, email: ConstantsForTests.email);
            CheckRadiobuttons(randomGender, mainPage);
            mainPage.AnotherEmailLink.Click();
            
            CheckReturningToFirstStep(mainPage);
        }

        [TestCase(TestName = "Выбираем любой пол, не указывем e-mail")]
        public void ValidationError_WhenEmptyEmail()
        {
            var mainPage = new MainPage(WebDriver);
            var randomGender = GenderRandomizer();

            mainPage.FillAllFieldsAndClickNextButton(randomGender, email: string.Empty);

            CheckEmailValidation(
                ConstantsForTests.expectedValidationMessageEmptyEMail, mainPage);
        }

        [TestCase(TestName = "Выбираем любой пол, указывем невалидный e-mail")]
        public void ValidationError_WhenWrongEmail()
        {
            var mainPage = new MainPage(WebDriver);
            var randomGender = GenderRandomizer();
            
            mainPage.FillAllFieldsAndClickNextButton(randomGender, email: ConstantsForTests.wrongEmail);

            CheckEmailValidation(
                errorMessage: ConstantsForTests.expectedValidationMessageWrongEMail, mainPage);
        }

        private void CheckRadiobuttons(Gender gender, MainPage page)
        {
            var isBoy = gender == Gender.Boy;
            Assert.That((isBoy ? page.BoyButton : page.GirlButton).Selected
                        && (!isBoy ? page.GirlButton : page.BoyButton).Selected, "Нажат некорректный радиобаттон");
        }

        private void CheckEmailValidation(string errorMessage, MainPage mainPage)
        {
            Assert.Multiple(() =>
            {
                //Проверим, что валидационное сообщение отображается
                Assert.IsTrue(mainPage.ValidationMessage.Displayed,
                    "Валидационное сообщение не отображается");
                //Проверим, что выводится верное валидационное сообщение
                Assert.AreEqual(errorMessage, mainPage.ValidationMessage.Text,
                    "Неверное валидационное сообщение");
                //Поле с ошибкой подсвечивается красной рамкой.
                Assert.IsTrue(mainPage.ValidationRedFrame.Displayed,
                    "Поле с ошибкой не подсвечивается красной рамкой.");
            });
        }

        public void CheckSecondStep(Gender gender, string email, MainPage mainPage)
        {
            Assert.Multiple(() =>
            {
                //Проверяем что появился текст о принятии запроса
                Assert.IsTrue(mainPage.ResultText.Displayed,
                    "Сообщение об успехе создания запроса не отображается");
                //Проверяем, что email совпадает с тем что указывали в запросе
                Assert.AreEqual(email, mainPage.ResultEmail.Text,
                    "Неверный email на который будем отвечать");
                //Проверяем, что появилась ссылка "указать другой e-mail"
                Assert.IsTrue(mainPage.AnotherEmailLink.Displayed,
                    "Ссылка 'указать другой e-mail' не отображается");

                //Проверяем, что выводится верное сообщение о создании запрос
                Assert.AreEqual(
                    gender == Gender.Boy
                        ? ConstantsForTests.expectedResultTextForBoys
                        : ConstantsForTests.expectedResultTextForGirls,
                    mainPage.ResultText.Text,
                    "Неверное сообщение об успешном создании запроса");
            });
        }


        public void CheckReturningToFirstStep(MainPage mainPage)
        {
            Assert.Multiple(() =>
            {
                //Проверяем, что поле ввода email стало пустым
                Assert.IsEmpty(mainPage.Email.Text,
                    "Ожидали что поле для ввода email будет очищено");
                //Проверияем,что появилась кнопка "Подобрать имя" 
                Assert.IsTrue(mainPage.SendButton.Displayed,
                    "Ожидали, что кнопка 'Подобрать имя' отображается");
                //Проверяем, что исчезда ссылка "указать другой e-mail'"
                Assert.IsFalse(mainPage.IsAnotherMailLinkExist(),
                    "Ссылка указать другой 'e-mail не исчезла'");
            });
        }
        public static Gender GenderRandomizer()
        {
            Random rnd = new Random();
            Gender randomGender = (Gender) rnd.Next(2);
            return randomGender;
        }
    }
}