using System;
using OpenQA.Selenium;

namespace AutomationHackaton
{
    public static class BrowserExtentions
    {
        public static IWebElement WaitForAndReturn(this IWebDriver driver, By selector, int timeout = 30, IWebElement parentElement = null)
        {
            IWebElement element = null;

            Wait.WaitFor(
                () =>
                {
                    try
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
                        element = parentElement == null
                            ? driver.FindElement(selector)
                            : parentElement.FindElement(selector);

                        return (true, null);
                    }
                    catch (Exception e)
                    {
                        return (false, e);
                    }
                    finally
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    }
                }, timeout);

            driver.ScrollIntoView(element);
            return element;
        }

        public static void ScrollIntoView(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", element);
        }

        public static void ScrollToTheBottom(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

    }
}