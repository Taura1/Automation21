using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationHackaton
{
    public class Tests
    {
        ChromeDriver ChromeDriver { get; set; }

        [SetUp]
        public void Setup()
        {
            ChromeDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }

        [Test]
        public void Test1()
        {
            ChromeDriver.Navigate().GoToUrl("http://www.google.com");

            var acceptButton = ChromeDriver.FindElement(By.XPath(".//button[.//div[normalize-space()='Sutinku']]"));
            acceptButton.Click();

            var searchInput = ChromeDriver.FindElement(By.Name("q"));
            searchInput.SendKeys("devbridge");
            searchInput.SendKeys(Keys.Enter);

            var searchResults = ChromeDriver.FindElements(By.XPath(".//div[@class='g']"));

            var searchResult = searchResults[0];
            var link = searchResult.FindElement(By.CssSelector("a"));

            Assert.True(link.GetAttribute("href").Contains("devbridge.com"));
        }

        [Test]
        public void Test2()
        {
            ChromeDriver.Navigate().GoToUrl("http://www.google.com");

            var acceptButton = ChromeDriver.FindElement(By.XPath(".//button[.//div[normalize-space()='Sutinku']]"));
            acceptButton.Click();

            var searchInput = ChromeDriver.FindElement(By.Name("q"));
            searchInput.SendKeys("devbridge");
            searchInput.SendKeys(Keys.Enter);

            var searchResults = ChromeDriver.FindElements(By.XPath(".//div[@class='g']"));

            var searchResult = searchResults[1];
            var link = searchResult.FindElement(By.CssSelector("a"));

            Assert.True(link.GetAttribute("href").Contains("rekvizitai"));
        }
    }
}