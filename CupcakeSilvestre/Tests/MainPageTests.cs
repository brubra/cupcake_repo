using CupcakeSilvestre.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Tests;

namespace CupcakeSilvestre.Tests
{
    class MainPageTests : TestBase
    {
        [Test, Description("Verificar cabeçalho com sacola vazia")]
        public void TC1()
        {
            //Arrange
            string expectedHeaderText = "FALTAM MAIS R$ 100,00 PARA O FRETE SAIR DE GRAÇA!";
            string actualHeaderText = string.Empty;
            MainPage inMainPage = new MainPage(driver);

            //Act
            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            actualHeaderText = inMainPage.GetHeaderText();

            //Assert
            Assert.IsTrue(actualHeaderText == expectedHeaderText,
                "There was a problem with the header text: "
                + "Expected text: " + expectedHeaderText + "  " + "Actual Text: " + actualHeaderText);        }

        [Test, Description("Verificar texto de sacola vazia")]
        public void TC2()
        {
            string expectedShoppingEmptyText = "Nenhum cupcake adicionado na sua sacola.";
            string actualShoppingEmptyText = string.Empty;
            MainPage inMainPage = new MainPage(driver);

            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            actualShoppingEmptyText = inMainPage.GetShoppingCardEmptyText();

            Assert.IsTrue(actualShoppingEmptyText == expectedShoppingEmptyText,
                "There was a problem with the shopping cart text: "
                + "Expected Text" + expectedShoppingEmptyText + "  " + "Actual Text: " + actualShoppingEmptyText);
        }

        [Test, Description("Adicionar 20 unidades de Cupcake de chocolate com cobertura de ganache")]
        public void TC3()
        {
            string expectedHeaderText = "PARABÉNS! O FRETE É POR NOSSA CONTA.";
            string actualHeaderText = string.Empty;
            string subtotalText = string.Empty;
            MainPage inMainPage = new MainPage(driver);

            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            inMainPage.BuyCupcakesByOrderAndQuantity(0, 20);
            subtotalText = inMainPage.GetRawSubtotal();
            actualHeaderText = inMainPage.GetActualHeaderTextForSubtotalEqualOrMoreThan100(subtotalText);

            Assert.IsTrue(actualHeaderText == expectedHeaderText, "There was a problem with the header text: " + "Expected text: " + expectedHeaderText + "  " + "Actual Text: " + actualHeaderText);
        }

        [Test, Description("Adicionar 3 Cupcakes de baunilha com cobertura de buttercream")]
        public void TC4()
        {
            string expectedHeaderText = string.Empty;
            string actualHeaderText = string.Empty;
            string subtotalText = string.Empty;
            MainPage inMainPage = new MainPage(driver);

            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            inMainPage.BuyCupcakesByOrderAndQuantity(2, 3);
            subtotalText = inMainPage.GetRawSubtotal();
            expectedHeaderText = inMainPage.CreateExpectedHeaderTextForSubtotalLessThan100(subtotalText);
            actualHeaderText = inMainPage.GetHeaderText();

            Assert.IsTrue(actualHeaderText == expectedHeaderText, "There was a problem with the header text: " + "Expected text: " + expectedHeaderText + "  " + "Actual Text: " + actualHeaderText);
        }

        [Test, Description("Adicionar 1000 Cupcakes de chocolate com cobertura de ganache")]
        public void TC5()
        {
            string actualSubtotalText = string.Empty;
            string expectedSubtotalText = "5.000,00";
            MainPage inMainPage = new MainPage(driver);

            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            inMainPage.BuyCupcakesByOrderAndQuantity(0, 1000);
            actualSubtotalText = inMainPage.GetCleanSubtotal();

            Assert.IsTrue(actualSubtotalText == expectedSubtotalText, "The expected subtotal was: " + expectedSubtotalText + " but the actual subtotal was: " + actualSubtotalText);
        }

        [Test, Description("Adicionar 1000 Cupcakes vegano de chocolate")]
        public void TC6()
        {
            string actualSubtotalText = string.Empty;
            string expectedSubtotalText = "2.750,00";
            MainPage inMainPage = new MainPage(driver);

            driver.Navigate().GoToUrl("http://cro.agencia.pmweb.com.br/cupcakes-teste/");
            inMainPage.BuyCupcakesByOrderAndQuantity(7, 1000);
            actualSubtotalText = inMainPage.GetCleanSubtotal();

            Assert.IsTrue(actualSubtotalText == expectedSubtotalText, "The expected subtotal was: " + expectedSubtotalText + " but the actual subtotal was: " + actualSubtotalText);
        }
    }
}
