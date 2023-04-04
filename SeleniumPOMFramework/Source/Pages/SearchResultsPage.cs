using AngleSharp.Dom;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumPOMFramework.Source.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace SeleniumPOMFramework.Source.Pages
{
    public class SearchResultsPage : BaseClass
    {
        [FindsBy(How = How.XPath, Using = "(//p[contains(text(),'Morning')])[1]")]
        private IWebElement morningCheckbox;

        [FindsBy(How = How.XPath, Using = "//span[@class='fs-inherit c-inherit' and contains(text(),'Departure')]")]
        private IWebElement departure_link;


        //(//img[@alt='IndiGo']/ancestor::div[@class='flex flex-column ms-grid-column-1 flex-center mb-4'])[1]

        public SearchResultsPage()
        {
            PageFactory.InitElements(_driver, this);
        }

       

        public void selectMorningFlight()
        {

            Thread.Sleep(WAIT_TIME);
            morningCheckbox.Click();    
        }

        public void sortByDeparture()
        {

            Thread.Sleep(WAIT_TIME);
            departure_link.Click();
        }


    }
}
