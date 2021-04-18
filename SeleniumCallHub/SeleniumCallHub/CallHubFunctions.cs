using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumCallHub
{
    class CallHubFunctions
    {
        IWebDriver driver = new ChromeDriver(); 

        public void GivenINavingateToTheSite()
        {
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["CallHubURL"]);
            driver.Manage().Window.Maximize();
            Thread.Sleep(3000);
        }

        public void WhenISelectSubscription(string subscription)
        {
            switch(subscription)
            {
                case "Monthly":
                    driver.FindElement(By.XPath("//li[starts-with(@class, 'plan-item')][1]")).Click();
                    break;

                case "Quarterly":
                    driver.FindElement(By.XPath("//li[starts-with(@class, 'plan-item')][2]")).Click();
                    break;

                case "Half-yearly":
                    driver.FindElement(By.XPath("//li[starts-with(@class, 'plan-item')][3]")).Click();
                    break;

                case "Yearly":
                    driver.FindElement(By.XPath("//li[starts-with(@class, 'plan-item')][4]")).Click();
                    break;

            }
                           
        }

        
        public void ThenIVerifyThePricing(string subscription)
        {
            string[] refData = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Test Data\\CallHubPricing.csv"));
            string[] header = refData[0].Split(',');
            string[] rowdata = new string[6];

            if(subscription.Equals("Monthly"))
            {
                rowdata = refData[1].Split(',');
            }
            else if(subscription.Equals("Quarterly"))
            {
                rowdata = refData[2].Split(',');
            }
            else if (subscription.Equals("Half-yearly"))
            {
                rowdata = refData[3].Split(',');
            }
            else if (subscription.Equals("Yearly"))
            {
                rowdata = refData[4].Split(',');
            }

            IDictionary<string, string> dataDiction = new Dictionary<string, string>();
            for (int i = 0; i < header.Length; i++)
            {
                dataDiction.Add(header[i].Trim('"'), rowdata[i]);
            }
            if (subscription.Equals("Monthly"))
                {
                string busPM = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//h3)[3]")).Text;
                string[] businessPM = busPM.Split(' ');
                Assert.AreEqual(dataDiction["Business"], businessPM[0]);

                string premPM = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//h3)[5]")).Text;
                string[] premiumPM = premPM.Split(' ');
                Assert.AreEqual(dataDiction["Premium"], premiumPM[0]);

                string busBilling = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//small)[2]")).Text;
                string[] businessBilling = busBilling.Split(' ');
                Assert.AreEqual(dataDiction["Business Billing"], businessBilling[1]);

                string premBilling = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//small)[3]")).Text;
                string[] premiumBilling = premBilling.Split(' ');
                Assert.AreEqual(dataDiction["Premium Billing"], premiumBilling[1]);               

            }
            else
            {
                string busPM = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//h3)[3]")).Text;
                string[] businessPM = busPM.Split(' ');
                Assert.AreEqual(dataDiction["Business"], businessPM[0]);

                string premPM = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//h3)[5]")).Text;
                string[] premiumPM = premPM.Split(' ');
                Assert.AreEqual(dataDiction["Premium"], premiumPM[0]);

                string busBilling = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//small)[2]")).Text;
                string[] businessBilling = busBilling.Split(' ');
                Assert.AreEqual(dataDiction["Business Billing"], businessBilling[2]);

                string premBilling = driver.FindElement(By.XPath("(//table[@class='plan-table plan-first-section']//td//small)[3]")).Text;
                string[] premiumBilling = premBilling.Split(' ');
                Assert.AreEqual(dataDiction["Premium Billing"], premiumBilling[2]);

                string busSaving = driver.FindElement(By.XPath("//td//div[@class='badge-content bg-green ']//span")).Text;
                busSaving = busSaving.Substring(5);
                Assert.AreEqual(dataDiction["Business Saving"], busSaving);

                string premSaving = driver.FindElement(By.XPath("//td//div[@class='badge-content bg-info ']//span")).Text;
                premSaving = premSaving.Substring(5);
                Assert.AreEqual(dataDiction["Premium Saving"], premSaving);
            }
                      
            
        }

        public void ThenICloseTheBrowser()
        {
            driver.Quit();
        }

    }
}
