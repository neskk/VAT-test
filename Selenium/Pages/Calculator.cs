using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Pages
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPageObject
    {
        //The URL of the calculator to be opened in the browser
        private const string CalculatorUrl = "http://www.calkoo.com/?lang=3&page=8";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public CalculatorPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement CountryElement => _webDriver.FindElement(By.CssSelector("select[name='Country']")); // another selector: "select.select150"

        private IWebElement VatElement => _webDriver.FindElement(By.CssSelector("input[name='VAT']"));

        private IReadOnlyCollection<IWebElement> FindPriceElements => _webDriver.FindElements(By.CssSelector("input[name='find']"));

        private IWebElement NetPriceElement => _webDriver.FindElement(By.Id("NetPrice")); // Net

        private IWebElement VatSumElement => _webDriver.FindElement(By.Id("VATsum")); // VAT

        private IWebElement PriceElement => _webDriver.FindElement(By.Id("Price")); // Gross

        private IWebElement ResetButtonElement => _webDriver.FindElement(By.CssSelector("input[name='clear']"));


        public void EnterNetPrice(string number)
        {
            //Clear text box
            NetPriceElement.Clear();
            //Enter text
            NetPriceElement.SendKeys(number);
        }

        public void EnterVatSum(string number)
        {
            //Clear text box
            VatSumElement.Clear();
            //Enter text
            VatSumElement.SendKeys(number);
        }

        public void EnterPrice(string number)
        {
            //Clear text box
            PriceElement.Clear();
            //Enter text
            PriceElement.SendKeys(number);
        }

        public void ClickReset()
        {
            //Click the Reset button
            ResetButtonElement.Click();
        }

        public void EnsureCalculatorIsOpenAndReset()
        {
            //Open the calculator page in the browser if not opened yet
            if (_webDriver.Url != CalculatorUrl)
            {
                _webDriver.Url = CalculatorUrl;
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Click the rest button
                ResetButtonElement.Click();

                //Wait until the result is empty again
                WaitForEmptyResult();
            }
        }

        public string WaitForNonEmptyNetPrice()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => NetPriceElement.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string WaitForNonEmptyVatSum()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => VatSumElement.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string WaitForNonEmptyPrice()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => PriceElement.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string WaitForEmptyNetPrice()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => NetPriceElement.GetAttribute("value"),
                result => result == string.Empty);
        }

        public string WaitForEmptyVatSum()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => VatSumElement.GetAttribute("value"),
                result => result == string.Empty);
        }

        public string WaitForEmptyPrice()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => PriceElement.GetAttribute("value"),
                result => result == string.Empty);
        }

        public void WaitForEmptyResult()
        {
            WaitForEmptyNetPrice();
            WaitForEmptyVatSum();
            WaitForEmptyPrice();
        }


        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            return wait.Until(driver =>
            {
                T? result = getResult();
                if (!isResultAccepted(result))
                    return default;

                return result;
            });

        }
    }
}
