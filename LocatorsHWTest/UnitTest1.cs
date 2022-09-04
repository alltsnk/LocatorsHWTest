using NUnit.Framework;
using OpenQA.Selenium;
//using System.Net.Mail;
using System.Threading;

namespace LocatorsHWTest
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _loginField = By.XPath("/html/body/div/div/main/div[1]/form/div[1]/div/label/input");
        private readonly By _passwordField = By.Name("password");
        private readonly By _submit = By.XPath("/html/body/div/div/main/div[1]/form/button");

        private readonly By _writeMail = By.XPath("//*[@id='content']/aside/button");
        private readonly By _reciverField = By.Name("toFieldInput");
        private readonly By _mailText = By.XPath("//*[@id='screens']/div/div[2]/section[1]/div[4]/div[2]/input");
        private readonly By _smiles = By.CssSelector("#mceu_18");
        private readonly By _sendSmile = By.ClassName("smiles-2");
        private readonly By _sendMail = By.XPath("//*[@id='screens']/div/div[2]/div/button[1]");

        private readonly By _mailIsSent = By.LinkText("лист");

        private const string login = "testmail1ivanenko";
        private const string password = "ax56lKo_89OP8";
        private const string reciverEmail = "sovyshka77@gmail.com";
        private const string text = "Hello World!";

        private bool expectedResult = true;
        private bool actualResult;

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://accounts.ukr.net/login?client_id=9GLooZH9KjbBlWnuLkVX&drop_reason=logout");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void SendMailTest()
        {
            var enterLogin = driver.FindElement(_loginField);
            enterLogin.SendKeys(login);
            var enterPassword = driver.FindElement(_passwordField);
            enterPassword.SendKeys(password);

            Thread.Sleep(2000);

            var submitPassword = driver.FindElement(_submit);
            submitPassword.Click();

            Thread.Sleep(5000);
            var writeMail = driver.FindElement(_writeMail);
            writeMail.Click();

            var reciver = driver.FindElement(_reciverField);
            reciver.SendKeys(reciverEmail);
            var sendText = driver.FindElement(_mailText);
            sendText.SendKeys(text);

            var smiles = driver.FindElement(_smiles);
            smiles.Click();
            var sendSmile = driver.FindElement(_sendSmile);
            sendSmile.Click();

            var sendMail = driver.FindElement(_sendMail);
            sendMail.Click();

            Thread.Sleep(2000);
            var mailIsSent = driver.FindElements(_mailIsSent);

            foreach(var msg in mailIsSent)
            {
                if (msg != null)
                {
                    actualResult = true;
                    break;
                }
                else
                {
                    actualResult = false;
                }
            }

            Assert.AreEqual(expectedResult, actualResult, "Mail wasn't sent");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}