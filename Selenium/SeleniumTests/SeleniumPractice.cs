using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class SeleniumPractice
{
    private const string PageUrl = "https://qa-course.kontur.host/selenium-practice/";

#pragma warning disable CS8618
    private ChromeDriver browser;
    private static PracticePage page;
#pragma warning restore CS8618

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        browser = new ChromeDriver(options);
        page = new PracticePage(browser);
        browser.Navigate().GoToUrl(PageUrl);
    }
    
    [TearDown]
    public void TearDown()
    {
        browser.Close();
        browser.Dispose();
    }

    #region ElementsTextTests

    [Test]
    public void Should_display_text_default()
    {
        // note(d.stukov): test optimization
        var testCases = new (IWebElement Element, string Text)[]
        {
            (page.Title, "Не знаешь как назвать?"),
            (page.Subtitle, "Мы подберём имя для твоего попугайчика!"),
            (page.GenderQuestion, "Кто у тебя?"),
            (page.EmailQuestion, "На какой e-mail прислать варианты имён?"),
            (page.GenderBoyLabel, "мальчик"),
            (page.GenderGirlLabel, "девочка")
        };
        using (new AssertionScope())
        {
            foreach (var (element, text) in testCases)
            {
                element.Displayed.Should().BeTrue();
                element.Text.Should().Be(text);
            }
        }
    }
    
    [Test]
    public void Should_display_text_step_1()
    {
        var testCases = new (IWebElement Element, string Text)[]
        {
            (page.Step1SubmitButton, "ПОДОБРАТЬ ИМЯ")
        };
        using (new AssertionScope())
        {
            Should_display_text_default();
            foreach (var (element, text) in testCases)
            {
                element.Displayed.Should().BeTrue();
                element.Text.Should().Be(text);
            }
        }
    }

    [Test]
    public void Should_display_text_step_2()
    {
        page.FormStep2();
        var testCases = new (IWebElement Element, string Text)[]
        {
            (page.Step2ResultText, "Хорошо, мы пришлём имя для вашего мальчика на e-mail:"),
            (page.Step2ResultEmail, page.DefaultEmail),
            (page.Step2AnotherEmailButton, "указать другой e-mail")
        };
        using (new AssertionScope())
        {
            Should_display_text_default();
            
            foreach (var (element, text) in testCases)
            {
                element.Displayed.Should().BeTrue();
                element.Text.Should().Be(text);
            }
        }
    }

    [TestCaseSource(nameof(ParrotSexData))]
    public void Should_display_different_text_when_parrot_sex_changes(Func<IWebElement> parrotGenderSelector, string text)
    {
        parrotGenderSelector().Click();
        page.EmailInput.SendKeys(page.DefaultEmail);
        page.Step1SubmitButton.Click();

        page.Step2ResultText.Text.Should().Be(text);
    }

    private static IEnumerable<TestCaseData> ParrotSexData()
    {
        yield return new TestCaseData(() => page.GenderBoyRadioButton, "Хорошо, мы пришлём имя для вашего мальчика на e-mail:")
        {
            TestName = "Boy"
        };
        yield return new TestCaseData(() => page.GenderGirlRadioButton, "Хорошо, мы пришлём имя для вашей девочки на e-mail:")
        {
            TestName = "Girl"
        };
    }

    #endregion

    #region EmailValidationTests

    [Test]
    public void Should_display_error_when_submit_without_email()
    {
        page.GenderBoyRadioButton.Click();
        page.Step1SubmitButton.Click();

        using (new AssertionScope())
        {
            page.Step1EmailValidationError.Displayed.Should().BeTrue();
            page.Step1EmailValidationError.Text.Should().Be("Введите email");
        }
    }
    
    [Test]
    public void Should_display_error_when_submit_incorrect_email()
    {
        page.GenderBoyRadioButton.Click();
        page.EmailInput.SendKeys("incorrect_email");
        page.Step1SubmitButton.Click();

        using (new AssertionScope())
        {
            page.Step1EmailValidationError.Displayed.Should().BeTrue();
            page.Step1EmailValidationError.Text.Should().Be("Некорректный email");
        }
    }

    #endregion

    #region ScenarioTests

    [Test]
    public void Should_not_clear_email_in_emailInput_after_click_step1_submitButton()
    {
        page.FormStep2();
        page.EmailInputValue.Should().Be(page.DefaultEmail);
    }
    
    [Test]
    public void Should_clear_emailInput_when_click_anotherEmailButton()
    {
        page.FormStep2();
        page.Step2AnotherEmailButton.Click();
        page.EmailInputValue.Should().BeEmpty();
    }

    [Test]
    public void Should_change_gender_after_click_label_text()
    {
        // useless test because of for="choiceFirst" and etc identifier
        using (new AssertionScope())
        {
            page.GenderBoyLabel.Click();
            page.GenderBoyRadioButton.Selected.Should().BeTrue();
        
            page.GenderGirlLabel.Click();
            page.GenderGirlRadioButton.Selected.Should().BeTrue();
        
            page.GenderBoyLabel.Click();
            page.GenderBoyRadioButton.Selected.Should().BeTrue();
        }
    }

    #endregion
}