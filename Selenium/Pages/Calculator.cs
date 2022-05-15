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

        //Finding elements
        private IWebElement CountryElement => _webDriver.FindElement(By.CssSelector("select[name='Country']")); // another selector: "select.select150"

        private IReadOnlyCollection<IWebElement> RateElements => _webDriver.FindElements(By.CssSelector("input[name='VAT']"));

        private IReadOnlyCollection<IWebElement> FindPriceElements => _webDriver.FindElements(By.CssSelector("input[name='find']"));

        private IWebElement FindNetPriceElement => _webDriver.FindElement(By.CssSelector("label[for='F1']"));

        private IWebElement NetPriceElement => _webDriver.FindElement(By.Id("NetPrice")); // Net

        private IWebElement FindVatSumElement => _webDriver.FindElement(By.CssSelector("label[for='F2']"));

        private IWebElement VatSumElement => _webDriver.FindElement(By.Id("VATsum")); // VAT

        private IWebElement FindPriceElement => _webDriver.FindElement(By.CssSelector("label[for='F3']"));

        private IWebElement PriceElement => _webDriver.FindElement(By.Id("Price")); // Gross

        private IWebElement ResetButtonElement => _webDriver.FindElement(By.CssSelector("input[name='clear']"));


        public void SelectCountry(string country)
        {
            var selectOptionList = CountryElement.FindElements(By.TagName("option"));

            foreach (IWebElement option in selectOptionList)
            {
                if (option.Text.Equals(country))
                {
                    option.Click();
                    return;
                }
            }
            throw new InvalidSelectorException($"Country option of {country} not found");
        }

        public void SelectRate(int rate)
        {
            foreach (IWebElement el in RateElements)
            {
                int elRate = int.Parse(el.GetAttribute("value"));
                if (elRate == rate)
                {
                    _webDriver.FindElement(By.CssSelector($"label[for='VAT_{rate}']")).Click();
                    return;
                }
            }
            throw new InvalidSelectorException($"VAT rate value of {rate} not found");
        }

        public void EnterNetPrice(string number)
        {
            //Select option
            FindNetPriceElement.Click();
            //Clear text box
            NetPriceElement.Clear();
            //Enter text
            NetPriceElement.SendKeys(number);
        }

        public void EnterVatSum(string number)
        {
            //Select option
            FindVatSumElement.Click();
            //Clear text box
            VatSumElement.Clear();
            //Enter text
            VatSumElement.SendKeys(number);
        }

        public void EnterPrice(string number)
        {
            //Select option
            FindPriceElement.Click();
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

        public bool EnsurePageIsLoaded()
        {
            try
            {
                var waitForDocumentReady = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
                waitForDocumentReady.Until((wdriver) =>
                    (_webDriver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));
                return true;
            }
            catch (TimeoutException timeoutException)
            {
                return false;
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
#pragma warning disable CS8603 // Possible null reference return.
            return wait.Until(driver =>
            {
                T? result = getResult();
                if (!isResultAccepted(result))
                    return default;

                return result;
            });
#pragma warning restore CS8603 // Possible null reference return.

        }
    }
}
