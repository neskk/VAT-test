﻿using System;
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

        [Given("the net price is (.*)")]
        public void GivenTheNetPriceIs(float number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterNetPrice(number.ToString());
        }

        [Given("the vat sum is (.*)")]
        public void GivenTheVatSumIs(float number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterVatSum(number.ToString());
        }

        [Given("the price is (.*)")]
        public void GivenThePriceIs(float number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterPrice(number.ToString());
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

            actualResult.Should().Be(expectedResult.ToString());
        }

        [Then("the vat sum should be (.*)")]
        public void ThenTheVatSumShouldBe(float expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyVatSum();

            actualResult.Should().Be(expectedResult.ToString());
        }

        [Then("the price should be (.*)")]
        public void ThenThePriceShouldBe(float expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyPrice();

            actualResult.Should().Be(expectedResult.ToString());
        }
    }
}