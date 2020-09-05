using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;

namespace Admixer
{
    public class Tests
    {
        private string testPage = "https://rozetka.com.ua/";
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {

            driver.Url = testPage;
            var url = driver.Url;

            Assert.AreEqual(testPage, url);


            driver.FindElement(By.XPath(".//input[@name='search']")).SendKeys("смартфоны");
            driver.FindElement(By.XPath(".//input[@name='search']")).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[3]/div/div/div/div[1]/div/rz-filter-section-checkbox/ul[1]/li[1]/a/label"))
                .Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[3]/div/div/div/div[1]/div/rz-filter-section-checkbox/ul[1]/li[9]/a/label"))
                .Click();
            System.Threading.Thread.Sleep(4000);

            var value = driver
                .FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[2]"))
                .GetProperty("value").ToString().Length;
            ;

            for (int i = 0; i < value; i++)
            {
                driver.FindElement(By.XPath(
                        "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[2]"))
                    .SendKeys(Keys.Backspace);
            }

            driver.FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[1]"))
                .Click();
            driver.FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[2]"))
                .SendKeys("20000");
            driver.FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[2]"))
                .SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            var inputValue = driver
                .FindElement(By.XPath(
                    "/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[4]/div/div/div/div/div/rz-filter-slider/form/fieldset/div/input[2]"))
                .GetProperty("value");
            Assert.AreEqual("20000", inputValue);
        
            driver.FindElement(By.XPath("/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/aside/rz-filter-stack/div[13]/div/div/div/div/div/rz-filter-checkbox/ul[1]/li[1]/a/label")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[1]/div/rz-sort/select/option[3]")).Click();
            System.Threading.Thread.Sleep(2000);

            List<string> pricesList = new List<string>();
            //List of WebElements with the names of the Phones
             IList<IWebElement> elements = driver.FindElements(By.XPath("/html/body/app-root/div/div[1]/rz-category/div/main/rz-catalog/div/div[2]/section/rz-grid/ul/li/app-goods-tile-default/div/div[2]/a[2]/span/text()"));
             foreach (var element in elements)
             {
                 pricesList.Add(element.GetAttribute("data"));
             }
             //List with the names of the phones up to 20 000UAH
             string result = string.Join("\r\n", elements);
             string filePath = @".\\Phones.txt";
             var date = DateTime.Now;
             using (StreamWriter outputFile = new StreamWriter(filePath,true))
             {
                 outputFile.WriteLine("{0} - {1}", result,date);
             }
             





        }
        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}