using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
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
            ChromeDriver = new ChromeDriver();
            ChromeDriver.Manage().Window.Maximize();
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
        public void Task1()
        {
        }

        [Test]
        public void Task2()
        {
        }

        [Test]
        public void Task3MusicChallenge()
        {
            ChromeDriver2 = new ChromeDriver();
            ChromeDriver2.Manage().Window.Maximize();
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
        public void Task5()
        {
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
            var token = "xoxb-2256155121-2781463424375-nbVMXkKkzxKoFUmfCWoZiiRR";
            http.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
            var uri = "https://slack.com/api/files.upload";
            var message = new HttpRequestMessage(HttpMethod.Post, uri);



            var file = Convert.ToBase64String(File.ReadAllBytes("C:\\Users\\taura.frezdorfaite\\Pictures\\test.jpg"), Base64FormattingOptions.None);

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("channels", "db-automation-hackathon-2021"));
            postData.Add(new KeyValuePair<string, string>("content", file.ToString()));
            //postData.Add(new KeyValuePair<string, string>("file", file.ToString()));
            postData.Add(new KeyValuePair<string, string>("fileType", "auto"));
            postData.Add(new KeyValuePair<string, string>("token", token));
            postData.Add(new KeyValuePair<string, string>("initial_comment", "Miau :3"));
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