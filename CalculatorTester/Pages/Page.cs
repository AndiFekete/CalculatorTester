using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CalculatorTester.Pages
{
    class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
    }
}
