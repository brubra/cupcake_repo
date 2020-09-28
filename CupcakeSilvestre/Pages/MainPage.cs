using Microsoft.VisualBasic.CompilerServices;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace CupcakeSilvestre.Pages
{
    public class MainPage
    {
        private static By headerText = By.ClassName("progress-bar-text");
        private static By buyButton = By.ClassName("botao-comprar");
        private static By subTotalText = By.ClassName("subtotal");
        private static By shoppingEmptyText = By.ClassName("vazio");
        private static By buyButtonId;     
        IWebDriver webDriver;

        public MainPage(IWebDriver driver)
        {
            this.webDriver = driver;
        }

        public String GetRawSubtotal()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return webDriver.FindElement(subTotalText).Text;
        }

        public String GetCleanSubtotal()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return webDriver.FindElement(subTotalText).Text.Substring(12);
        }

        public void ClickToBuyCupcakeByIdOrder(int order)
        {
            List<IWebElement> buyCupcakeButton = webDriver
                            .FindElements(buyButton)
                            .ToList();

            string onClick = buyCupcakeButton[order].GetAttribute("onclick");
            buyButtonId = By.XPath("//button[@onclick=\""+onClick+"\"]");
            webDriver.FindElement(buyButtonId).Click();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public void BuyCupcakesByOrderAndQuantity(int cupcakeOrderId, int amountOfTime)
        {
            for (int i = 0; i < amountOfTime; i++)
            {
                this.ClickToBuyCupcakeByIdOrder(cupcakeOrderId);
            }
       
        }

        public string CreateExpectedHeaderTextForSubtotalLessThan100(string subtotalPrice)
        {
            string subtotal = subtotalPrice.Substring(12); // command to clean the string and capture only the price text 
            double subtotalDouble = Convert.ToDouble(subtotal, new NumberFormatInfo() { NumberDecimalSeparator = "," }); // command to convert comma to dot
            double amount = 100 - subtotalDouble; // command to calculate the amount missing to get free shipping
            string amountText = amount.ToString("0.00"); // command to make sure that the value will always have two decimal houses
            amountText = amountText.Replace(".", ","); 
            string headerText = "FALTAM MAIS R$ "+ amountText +" PARA O FRETE SAIR DE GRAÇA!";
            return headerText;
        }

        public string GetActualHeaderTextForSubtotalEqualOrMoreThan100(string subtotalPrice)
        {
            string subtotal = subtotalPrice.Substring(12); // command to clean the string and capture only the price text 
            double subtotalDouble = Convert.ToDouble(subtotal, new NumberFormatInfo() { NumberDecimalSeparator = "," }); // command to convert the , to .

            if(subtotalDouble >= 100)
            { 
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                return webDriver.FindElement(headerText).Text;
            }

            return "The subtotal is less than 100";
        }

        public string GetHeaderText()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return webDriver.FindElement(headerText).Text;
        }

        public string GetShoppingCardEmptyText()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return webDriver.FindElement(shoppingEmptyText).Text;
        }

    }
}
