using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Selenium.Support.Drivers;
using Selenium.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace SpecFlowTest.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        //Page Object for Calculator
        private readonly CalculatorPageObject _calculatorPageObject;

        public CalculatorStepDefinitions(BrowserDriver browserDriver)
        {
            _calculatorPageObject = new CalculatorPageObject(browserDriver.Current);
        }

        [Given("I Navigate to VAT calculator")]
        public void GivenNavigateToVATCalculator()
        {
            _calculatorPageObject.EnsureCalculatorIsOpenAndReset();
            //throw new PendingStepException();
        }

        [Given("Selected the (.*) country")]
        public void GivenSelectedTheCountry(string country)
        {
            _calculatorPageObject.SelectCountry(country);
        }

        [Given("Selected the rate of (.*)%")]
        public void GivenSelectedTheRateOf(int rate)
        {
            _calculatorPageObject.SelectRate(rate);
        }

        [Given("the net price is (.*)")]
        public void GivenTheNetPriceIs(string number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterNetPrice(number);
        }

        [Given("the vat sum is (.*)")]
        public void GivenTheVatSumIs(string number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterVatSum(number);
        }

        [Given("the price is (.*)")]
        public void GivenThePriceIs(string number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterPrice(number);
        }

        /*
        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //delegate to Page Object
            _calculatorPageObject.ClickAdd();
        }
        */

        [Then("the net price should be (.*)")]
        public void ThenTheNetPriceShouldBe(float expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyNetPrice();

            actualResult.Should().Be(expectedResult.ToString("0.00"));
        }

        [Then("the vat sum should be (.*)")]
        public void ThenTheVatSumShouldBe(float expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyVatSum();

            actualResult.Should().Be(expectedResult.ToString("0.00"));
        }

        [Then("the price should be (.*)")]
        public void ThenThePriceShouldBe(float expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyPrice();

            actualResult.Should().Be(expectedResult.ToString("0.00"));
        }

        [Then(@"the net price should not be a number")]
        public void ThenTheNetPriceShouldNotBeANumber()
        {
            var actualResult = _calculatorPageObject.WaitForNonEmptyNetPrice();
            actualResult.Should().Be("NaN");
        }


        [Then(@"the vat sum should not be a number")]
        public void ThenTheVatSumShouldNotBeANumber()
        {
            var actualResult = _calculatorPageObject.WaitForNonEmptyVatSum();
            actualResult.Should().Be("NaN");
        }

        [Then(@"the price should not be a number")]
        public void ThenThePriceShouldNotBeANumber()
        {
            var actualResult = _calculatorPageObject.WaitForNonEmptyPrice();
            actualResult.Should().Be("NaN");
        }
    }
}