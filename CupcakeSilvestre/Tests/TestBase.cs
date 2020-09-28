using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium.Support.Extensions;

namespace Tests
{
    [TestFixture]

    public class TestBase
    {
        protected IWebDriver driver;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void CreateDriver()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void QuitDriver()
        {
            if (driver != null)
                driver.Quit();
        }

    }
}