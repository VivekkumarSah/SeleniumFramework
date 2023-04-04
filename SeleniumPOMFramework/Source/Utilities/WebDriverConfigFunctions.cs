using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.DevTools.V108.PerformanceTimeline;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using System.Configuration;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.DevTools;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;

namespace SeleniumPOMFramework.Source.Utilities
{
    public class WebDriverConfigFunctions : PropertyReader
    {
        public static IWebDriver _driver;
        public static readonly string appUrl = PropertyReader.GetPropertyValue("BaseUrl");
        public static readonly string browserName = PropertyReader.GetPropertyValue("Browser");

        public static ExtentTest test;
        public static ExtentReports extent;

        [SetUp]
        public void InitScript()
        {
            try
            {
                if (browserName.Equals("Chrome"))
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver();
                }
                else if (browserName.Equals("Edge"))
                {
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _driver = new EdgeDriver();
                }
                else
                {
                    Console.WriteLine("Browser name is not correct");

                }
                _driver.Manage().Window.Maximize();
                _driver.Navigate().GoToUrl(appUrl);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        [OneTimeSetUp]
        public void ExtentStart()
        {
            try
            {
                extent = new ExtentReports();
                var htmlreporter = new ExtentHtmlReporter(@"C:\Users\vivek\Report\Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
                extent.AttachReporter(htmlreporter);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            extent.Flush();
        }
    }   
}
