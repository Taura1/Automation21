using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace AutomationHackaton
{
    public class Tests
    {
        ChromeDriver ChromeDriver { get; set; }
        ChromeDriver ChromeDriver2 { get; set; }

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            ChromeDriver = new ChromeDriver(options);
            ChromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ChromeDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }

        public IWebElement getTableElement(string tableCoordinates)
        {
            var tableElementSelector = ChromeDriver.FindElement(By.CssSelector($"[data-square='{tableCoordinates}']"));

            return tableElementSelector;
        }
        public Boolean isAlertPresent()
        {
            try
            {
                ChromeDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        [Test]
        public void Task1CheckMate()
        {
            ChromeDriver.Navigate().GoToUrl("https://hackathon-chess.herokuapp.com/");

            while(true)
            {
                Actions actions = new Actions(ChromeDriver);

                actions
                    .ClickAndHold(getTableElement("e2"))
                    .MoveToElement(getTableElement("e4"))
                    .Release()
                    .ClickAndHold(getTableElement("d1"))
                    .MoveToElement(getTableElement("h5"))
                    .Release()
                    .ClickAndHold(getTableElement("f1"))
                    .MoveToElement(getTableElement("c4"))
                    .Release()
                    .ClickAndHold(getTableElement("h5"))
                    .MoveToElement(getTableElement("f7"))
                    .Release()
                    .Perform();

                Thread.Sleep(1000);

                if(isAlertPresent() == true)
                {
                    if (ChromeDriver.SwitchTo().Alert().Text.Contains("Checkmate!"))
                    {
                        break;
                    }
                }
                ChromeDriver.Navigate().Refresh();
            }

            Thread.Sleep(5000);
        }

        [Test]
        public void Task2()
        {
        }

        [Test]
        public void Task3MusicChallenge()
        {
            ChromeDriver2 = new ChromeDriver();
            // ChromeDriver2.Manage().Window.Maximize();
            var random = new Random();
            var defaultTimeout = 200;
            var pianoKeys = new String[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l", "w", "e", "t", "y", "u", "o", "p" };
            var drumKeys = new String[]{ "e", "r", "f", "g", "h", "v", "b", "j", "i", "k" };
            var seq = new[] { "s", "j", "h", "g", "s", "s", "s", "s", "j", "h", "g", "d", "d", "k", "j", "h", "t", "l", "l", "k", "h", "j", "s", "j", "h", "g", "s", "s", "j", "h", "g", "d", "d", "d", "k", "j",
                "h"
            };

            var seq2 = new[]{ defaultTimeout, defaultTimeout,defaultTimeout, defaultTimeout, defaultTimeout * 3, defaultTimeout / 2, defaultTimeout / 2, defaultTimeout, defaultTimeout, defaultTimeout,
                defaultTimeout, defaultTimeout * 4, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout * 4, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout,
                defaultTimeout * 4, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout * 4, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout * 3,
                defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout, defaultTimeout};

            var drum = "https://codepen.io/amdsouza92/full/xdooWa";
            var guitar = "https://codepen.io/sandovalgus/pen/rwBLwy";
            var piano = "https://codepen.io/gabrielcarol/full/rGeEbY";

            By section = By.XPath(".//body");
            ChromeDriver.Navigate().GoToUrl(drum);
            //ChromeDriver1.Navigate().GoToUrl(guitar);
            ChromeDriver2.Navigate().GoToUrl(piano);

            var drumElm = ChromeDriver.WaitForAndReturn(section);
            //var guitarElm = ChromeDriver1.FindElement(section);
            var pianoElm = ChromeDriver2.WaitForAndReturn(section);
            drumElm.Click();
            pianoElm.Click();

            var pianoActions = new Actions(ChromeDriver2);
            var drumActions = new Actions(ChromeDriver);

            var iteration = 0;
            var start = DateTime.Now;

            while ((DateTime.Now - start).TotalSeconds < 30 || iteration < 1)
            {
                var pianoKey = pianoKeys[random.Next(pianoKeys.Length)];
                var drumKey = drumKeys[random.Next(drumKeys.Length)];

                pianoActions.Click(pianoElm).KeyDown(seq[iteration]).KeyUp(seq[iteration]).Perform();
                drumActions.Click(drumElm).KeyDown(drumKey).KeyUp(drumKey).Perform();

                Thread.Sleep(seq2[iteration]);
                iteration++;
            }

            Thread.Sleep(5000);
            ChromeDriver2.Quit();
            ChromeDriver2.Dispose();
        }

        [Test]
        public void Task4()
        {
        }

        [Test]
        public void Task5Labyrinth()
        {
            ChromeDriver.Navigate().GoToUrl("http://hackathon-maze.herokuapp.com/static/");

            var arrowUp = OpenQA.Selenium.Keys.ArrowUp;
            var arrowDown = OpenQA.Selenium.Keys.ArrowDown;
            var arrowLeft = OpenQA.Selenium.Keys.ArrowLeft;
            var arrowRight = OpenQA.Selenium.Keys.ArrowRight;
            var bodyElement = ChromeDriver.FindElement(By.XPath(".//body"));

            void timesToTheUp(int number)
            {
                for (int i = 0; i < (number); i++)
                {
                    bodyElement.SendKeys(arrowUp);
                }
            }

            void timesToTheLeft(int number)
            {
                for (int i = 0; i < (number); i++)
                {
                    bodyElement.SendKeys(arrowLeft);
                }
            }

            void timesToTheRight(int number)
            {
                for (int i = 0; i < (number); i++)
                {
                    bodyElement.SendKeys(arrowRight);
                }
            }

            void timesToTheDown(int number)
            {
                for (int i = 0; i < (number); i++)
                {
                    bodyElement.SendKeys(arrowDown);
                }
            }

            timesToTheUp(4);
            timesToTheLeft(2);
            timesToTheUp(2);
            timesToTheLeft(3);
            timesToTheUp(2);
            timesToTheRight(3);
            timesToTheUp(2);
            timesToTheRight(4);
            timesToTheUp(4);
            timesToTheLeft(3);
            timesToTheDown(2);
            timesToTheLeft(7);
            timesToTheDown(2);
            timesToTheLeft(4);
            timesToTheDown(2);
            timesToTheRight(4);
            timesToTheDown(2);
            timesToTheLeft(4);
            timesToTheDown(2);
            timesToTheLeft(2);
            timesToTheUp(2);
            timesToTheLeft(4);
            timesToTheUp(2);
            timesToTheLeft(5);
            timesToTheDown(2);
            timesToTheLeft(1);
            timesToTheDown(4);
            timesToTheLeft(2);
            timesToTheUp(9);
            timesToTheRight(2);
            timesToTheUp(3);
            timesToTheLeft(2);
            timesToTheRight(2);
            timesToTheDown(4);
            timesToTheLeft(2);
            timesToTheDown(8);
            timesToTheRight(2);
            timesToTheUp(6);
            timesToTheRight(6);
            timesToTheDown(2);
            timesToTheRight(4);
            timesToTheDown(2);
            timesToTheRight(2);
            timesToTheUp(2);
            timesToTheRight(4);
            timesToTheUp(2);
            timesToTheLeft(4);
            timesToTheUp(2);
            timesToTheRight(4);
            timesToTheUp(2);
            timesToTheRight(8);
            timesToTheUp(6);
            timesToTheLeft(8);
            timesToTheUp(2);
            timesToTheLeft(2);
            timesToTheUp(2);
            timesToTheRight(4);
            timesToTheUp(4);
            timesToTheLeft(2);
            timesToTheUp(6);
            timesToTheLeft(3);
            timesToTheDown(1);
            timesToTheUp(1);
            timesToTheLeft(7);
            timesToTheDown(2);
            timesToTheLeft(4);
            timesToTheDown(6);
            timesToTheRight(2);
            timesToTheDown(3);
            timesToTheRight(1);
            timesToTheDown(1);
            timesToTheLeft(5);
            timesToTheDown(4);
            timesToTheLeft(2);
            timesToTheUp(6);
            timesToTheLeft(2);
            timesToTheUp(11);

            Thread.Sleep(5000);
        }

        [Test]
        public void Task6TheFlags()
        {
            ChromeDriver.Navigate().GoToUrl("https://www.gamesforthebrain.com/game/flag/");

            var answerSelector = "[name='answer']";
            var continueSelector = "[value='Continue'][id='continueButton']";

            for (var i = 0; i < 10; i++)
            {
                var answer = ChromeDriver.FindElement(By.CssSelector(answerSelector)).GetAttribute("value");
                var answerElement = ChromeDriver.FindElement(By.CssSelector($"[type='radio'][value='{answer}']"));
                answerElement.Click();
                var continueElement = ChromeDriver.FindElement(By.CssSelector(continueSelector));
                continueElement.Click();
                var goodAnswer = ChromeDriver.FindElement(By.CssSelector(".right")).Text;
                if (goodAnswer == "Your pick was right, congratulations! (+10 points)")
                {
                    continueElement = ChromeDriver.FindElement(By.CssSelector(continueSelector));
                    continueElement.Click();
                }
            }

            Thread.Sleep(5000);
        }

        [Test]
        public void Task7Memes()
        {
            var http = new HttpClient();
            var token = "";
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = "https://slack.com/api/files.upload";

            /*
            ChromeDriver.Navigate().GoToUrl("https://imgflip.com/memegenerator");
            var elm = ChromeDriver.WaitForAndReturn(By.XPath(".//textarea[@placeholder='Text #1']"));
            elm.SendKeys("LETS GO FEASTING");
            var elm2 = ChromeDriver.WaitForAndReturn(By.XPath(".//textarea[@placeholder='Text #2']"));
            elm2.SendKeys("Like rlly. Now.");
            var anotherElm = ChromeDriver.WaitForAndReturn(By.XPath(".//button[@class='mm-generate b but']"));
            anotherElm.Click();

            var linkElm = ChromeDriver.WaitForAndReturn(By.XPath(".//input[@class='img-code link']"));
            var theLink = linkElm.GetAttribute("value");*/



            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("channels", "db-automation-hackathon-2021"));
            postData.Add(new KeyValuePair<string, string>("content", "Hellou miau. Click the upper link ;)"));
            //postData.Add(new KeyValuePair<string, string>("file", file.ToString()));
            postData.Add(new KeyValuePair<string, string>("token", token));
            postData.Add(new KeyValuePair<string, string>("initial_comment", "https://imgflip.com/i/5whubo"));
            //postData.Add(new KeyValuePair<string, string>("content", "Miau :3"));

            using (var httpClient = new HttpClient())
            {
               // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    //content.Headers.Add("Content-Type", "multipart/form-data");
                    content.Headers.Add("Authentication", $"Bearer {token}");

                    var result = httpClient.PostAsync(uri, content).Result;

                }
            }

        }

    }
}