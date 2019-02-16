using CalculatorTester.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace CalculatorTester.Pages
{
    class CalculatorPage : Page
    {
        private const string titlePath = @"//div[@class='header']/h1";
        private const string buttonsPath = @"//div[@class='buttons']";
        private const string operationsPath = @"//div[@class='buttons']/select";
        private const string resultClassName = "result-val";

        [FindsBy(How=How.XPath, Using = @"//div[@class='header']/h1")]    
        public IWebElement Title { get; set; }

        [FindsBy(How=How.Id, Using = "logout")]
        public IWebElement Logout { get; set; }

        [FindsBy(How = How.ClassName, Using = "result-val")]                
        public IWebElement Result { get; set; }

        [FindsBy(How = How.ClassName, Using = "input-val")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.ClassName, Using = "buttons")]
        public IWebElement ButtonsContaner { get; set; }

        [FindsBy(How = How.XPath, Using = @"//div[@class='buttons']/select")] 
        public IWebElement OperationSelect { get; set; }

        public CalculatorPage(IWebDriver driver) : base(driver) { }

        public void WaitForTitle()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titlePath)));            
        }

        public void WaitForResult()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(resultClassName)));
        }

        public bool AlertIsPresent()
        {
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                return true;
            } catch (TimeoutException)
            {
                return false;
            }
        }

        public void ClickButton(Button button)
        {
            var path = buttonsPath + "/button[text()='" + button.GetDescription() + "']";
            var element = driver.FindElement(By.XPath(path));
            element.Click();
        }

        public void ClickButton(int button)
        {
            if (button <= 0 || button >= 9) throw new ArgumentOutOfRangeException("Button value must be between 0 and 9");

            var path = buttonsPath + "/button[text()='" + button + "']";
            var element = driver.FindElement(By.XPath(path));
            element.Click();
        }
        
        public void SelectOperation(Operation operation)
        {
            var select = new SelectElement(OperationSelect);
            select.SelectByValue(operation.GetDescription());
        }

    }
}
