using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace CalculatorTester.Pages
{
    class LoginPage : Page
    { 
        private string loginFormPath = @"//div[@class='login-form']";

        [FindsBy(How = How.XPath, Using = @"//div[@class='login-form']/input[@type='text']")]
        public IWebElement UserField { get; set; }
        
        [FindsBy(How = How.XPath, Using = @"//div[@class='login-form']/input[@type='password']")]
        public IWebElement PasswordField { get; set; }
        
        [FindsBy(How = How.XPath, Using = @"//div[@class='login-form']/button")]
        public IWebElement SubmitButton { get; set; }

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void LoginToPage(string userame, string password)
        {
            SetUserName(userame);
            SetPassword(password);
            ClickSubmit();           
        }

        public void WaitForLoginPage()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(loginFormPath)));
        }

        private void SetUserName(string userame)
        {
            UserField.SendKeys(userame);
        }

        private void SetPassword(string password)
        {
            PasswordField.SendKeys(password);
        }

        private void ClickSubmit()
        {
            SubmitButton.Click();
        }
        
    }
}
