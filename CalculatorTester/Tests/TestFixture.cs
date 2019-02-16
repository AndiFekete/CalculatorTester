using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Configuration;

using CalculatorTester.Pages;
using CalculatorTester.Utils;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Collections.Specialized;

namespace CalculatorTester
{
    [TestFixture]
    public class TestFixture : IDisposable
    {
        private const string testUserName = "testuser";
        private const string testUserPw = "testpassword";
        private const string testpageUrl = @"https://webautomationhw.realeyesit.com/";

        private IWebDriver driver;
        private LoginPage loginPage;
        private CalculatorPage calculatorPage;
        private Random random;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            Configure();
            
            loginPage = new LoginPage(driver);
            calculatorPage = new CalculatorPage(driver);
            PageFactory.InitElements(driver, loginPage);
            PageFactory.InitElements(driver, calculatorPage);

            random = new Random();
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            driver.Close();
        }

        [SetUp]
        public void SetUp()
        {
            driver.Navigate().GoToUrl(testpageUrl);
        }

        [TearDown]
        public void TearDown()
        {
            calculatorPage.Logout.Click();
            loginPage.WaitForLoginPage();
        }

        [Test]
        public void DivideByZeroTest()
        {
            loginPage.LoginToPage(testUserName, testUserPw);
            calculatorPage.WaitForTitle();

            calculatorPage.ClickButton(random.Next(0,9));
            calculatorPage.SelectOperation(Operation.Divide);
            calculatorPage.ClickButton(0);
            calculatorPage.ClickButton(Button.Equal);
            
            Assert.IsTrue(calculatorPage.AlertIsPresent());
            if (calculatorPage.AlertIsPresent())
            {
                Assert.AreEqual("Cannot divide by 0", driver.SwitchTo().Alert().Text);
                driver.SwitchTo().Alert().Dismiss();
            }
        }

        [Test]
        public void MultiplyTest()
        {
            loginPage.LoginToPage(testUserName, testUserPw);
            calculatorPage.WaitForTitle();

            int a = random.Next(0, 9);
            int b = random.Next(0, 9);

            calculatorPage.ClickButton(a);
            calculatorPage.SelectOperation(Operation.Multiply);
            calculatorPage.ClickButton(b);
            calculatorPage.ClickButton(Button.Equal);

            calculatorPage.WaitForResult();
            int result = int.Parse(calculatorPage.Result.Text);
            Assert.AreEqual(a * b, result);
        }

        public void Dispose()
        {
            driver.Dispose();
        }

        private void Configure()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            Browser browser;
            Enum.TryParse(appSettings["PreferedBrowser"], out browser);

            if (!Convert.ToBoolean(appSettings["IsRemote"]))
            {
                switch (browser)
                {
                    case Browser.Firefox:
                        driver = new FirefoxDriver();
                        break;
                    case Browser.Chrome:
                        driver = new ChromeDriver();
                        break;
                    default:
                        driver = new FirefoxDriver();
                        break;
                }
            }
            else
            {
                /*
                * please note that this could be done much nicer if browserstack didn't need it's options to be global
                */
                DriverOptions driverOptions;
                NameValueCollection settings = ConfigurationManager.GetSection("remoteTestingOptions")
                                                as NameValueCollection;
                switch (browser)
                {
                    case Browser.Firefox:
                        FirefoxOptions foptions = new FirefoxOptions();
                        foreach (String key in settings)
                        {
                            foptions.AddAdditionalCapability(key, settings[key], true);
                        }
                        driverOptions = foptions;
                        break;
                    case Browser.Chrome:
                        ChromeOptions coptions = new ChromeOptions();
                        foreach (String key in settings)
                        {
                            coptions.AddAdditionalCapability(key, settings[key], true);
                        }
                        driverOptions = coptions;
                        break;
                    default:
                        FirefoxOptions options = new FirefoxOptions();
                        foreach (String key in settings)
                        {
                            options.AddAdditionalCapability(key, settings[key], true);
                        }
                        driverOptions = options;
                        break;
                }
                
                driver = new RemoteWebDriver(new Uri(appSettings["Server"]), driverOptions);
            }

            
        }
    }
}
