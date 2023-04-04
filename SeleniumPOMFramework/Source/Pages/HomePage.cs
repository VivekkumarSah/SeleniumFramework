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
    public class HomePage : BaseClass

    {

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'One way')])[1]")]
        private IWebElement _oneWayDropDown;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'One way')])[2]")]
        private IWebElement _oneWaySelection;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Round trip')])[1]")]
        private IWebElement _roundTripSelection;

        [FindsBy(How = How.XPath,Using = "//input[@placeholder ='Where from?']")]
        private IWebElement _whereFromSearchBox;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder ='Where to?']")]
        private IWebElement _whereToSearchBox;

        [FindsBy(How = How.XPath, Using = "//p[@class='to-ellipsis o-hidden ws-nowrap fs-3 fw-500'][1]")]
        private IWebElement _firstSearchResult;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Search flights')]")]
        private IWebElement _searchFlightsButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='px-1   flex flex-middle nmx-1 pb-1']")]
        private IWebElement popUpCloseButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='p-relative br-4']")]
        private IWebElement selectPassengersDropdown;

        [FindsBy(How = How.XPath, Using = "//span[@class='fs-4 fw-500 lh-24 c-inherit']/ancestor::div[@class='flex flex-column']/following-sibling::ul/li[3]")]
        private IWebElement plusButtonAdults;

        [FindsBy(How = How.XPath, Using = "//span[@class='fs-4 fw-500 lh-24 fs-3 c-inherit']/ancestor::div[@class='flex flex-column mt-2 mb-2']/following-sibling::ul/li[3]")]
        private IWebElement plusButtonChildren;

        [FindsBy(How = How.XPath, Using = "//button[@class='flex flex-middle  t-all fs-2 focus:bc-secondary-500 bg-transparent bc-neutral-100 c-pointer pr-2 pl-3 pt-2 pb-2 ba br-4 h-8 c-neutral-900'][1]")]
        private IWebElement datePicker;

        [FindsBy(How = How.XPath, Using = "//div[@class='fs-4 fw-500 c-inherit flex flex-nowrap ml-6']")]
        private IWebElement datePickerReturn;

        [FindsBy(How = How.CssSelector, Using = "svg[data-testid='rightArrow']")]
        private IWebElement changeMonthRightArrow;


        public HomePage()
        {
            PageFactory.InitElements(_driver, this);
        }

        public void SearchAndSelectSource(String searchText)
        {
            enterText(_whereFromSearchBox, searchText);
            Thread.Sleep(WAIT_TIME);
            _firstSearchResult.Click();
        }

        public void SearchAndSelectDestination(String searchText)
        {
            enterText(_whereToSearchBox, searchText);
            Thread.Sleep(WAIT_TIME);
            _firstSearchResult.Click();
        }

        public void closePopUp()
        { 

            Thread.Sleep(WAIT_TIME);
            popUpCloseButton.Click();
        }



        public void SearchFlights()
        {
            _searchFlightsButton.Click();
        }

        public void selectJourneyType()
        {
            _oneWayDropDown.Click();
            _roundTripSelection.Click();    
        }

        public void enterText(IWebElement element,String text)
        {
            element.Clear();
            element.SendKeys(text);

        }
        public void ClickButton(IWebElement button, int numberOfClicks)

        {
            for (int i = 0; i < numberOfClicks; i++)
            {
                Thread.Sleep(2);
                button.Click();
            }
        }
        public void selectPassenger(int Adults, int Child)

        {
            
            selectPassengersDropdown.Click();
            ClickButton(plusButtonAdults, Adults-1);
            ClickButton(plusButtonChildren, Child);
            selectPassengersDropdown.Click();

        }

       

        public void clickOnDatePicker()
        {
            datePicker.Click();
        }

        public void selectDate(IWebDriver _driver, string date)
        {
            try
            {
                string datePath = "div[aria-label='" + date + "']";
                IWebElement pickDate = _driver.FindElement(By.CssSelector(datePath));
                Thread.Sleep(2);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
                executor.ExecuteScript("arguments[0].click();", pickDate);
            }
            catch (NoSuchElementException ex) {
                ClickButton(changeMonthRightArrow, 1);
                string datePath = "div[aria-label='" + date + "']";
                IWebElement pickDate = _driver.FindElement(By.CssSelector(datePath));
                Thread.Sleep(2);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
                executor.ExecuteScript("arguments[0].click();", pickDate);
            }

        }

        public static(string,DateTime) depatDate()
        {
            DateTime today = DateTime.Today;
            DateTime depD = today.AddDays(90);
            string dayOfWeek = depD.ToString("dddd");
            string day = dayOfWeek.Substring(0, 3);
            string depatdate = day + " " + depD.ToString("MMM dd yyyy");
            return (depatdate, depD);
        }

        public static (string,DateTime) returnDate()
        {
            (string DepatDFormtted, DateTime DeprtureDate) = depatDate();
            DateTime rDate = DeprtureDate.AddDays(2);
            string RdayOfWeek = rDate.ToString("dddd");
            string Rday = RdayOfWeek.Substring(0, 3);
            string returnDate = Rday + " " + rDate.ToString("MMM dd yyyy");
            return (returnDate, rDate);

        }

        public void selectReturnDate()
        {
            (string RdateFormatted, DateTime Rdate) = returnDate();
            Thread.Sleep(2);

            //datePickerReturn.Click();

            selectDate(_driver, RdateFormatted);

        }

        public void changeMonth()
        {
            
            (string DepatDFormtted, DateTime DeprtureDate) = depatDate();
            DateTime today = DateTime.Today;
            TimeSpan difference = DeprtureDate - today;
            double noOfDays = difference.TotalDays;
            int n = Convert.ToInt32(noOfDays);
            int noOfClicks =  (n / 30) - 1;
            try
            {
                ClickButton(changeMonthRightArrow, noOfClicks);
            }
            catch(StaleElementReferenceException ex) {
                ClickButton(changeMonthRightArrow, noOfClicks-1);
            }
        }

        public void selectDeparturedate()
        {
           (string DepatDFormtted , DateTime DeprtureDate) = depatDate();
            DateTime today = DateTime.Today;
            TimeSpan difference = DeprtureDate - today;
            
            if (difference.TotalDays >= 60)
            {
                changeMonth();
                
                Thread.Sleep(2);
                selectDate(_driver, DepatDFormtted); 
            }
            else
            {
                
                selectDate(_driver, DepatDFormtted);
            }

        }


    }

        
  
}
