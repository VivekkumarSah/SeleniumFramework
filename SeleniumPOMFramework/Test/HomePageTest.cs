using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumPOMFramework.Source.Pages;
using SeleniumPOMFramework.Source.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace SeleniumPOMFramework.Test
{
    [TestFixture]
    public class HomePageTest : BaseTest
    {

        [Test]
        public void SearchFlights() 
        {
            test = null;
            test = extent.CreateTest("T001").Info("Login Test");
            try
            {
                HomePage _homepage = new HomePage();
                test.Log(Status.Info, "Go to URL");
                Utility _utitlity = new Utility();
                SearchResultsPage _searchresultspage = new SearchResultsPage();
                _homepage.closePopUp();
                _homepage.SearchAndSelectSource("Mumbai");
                _homepage.SearchAndSelectDestination("New Delhi");
                _homepage.selectJourneyType();
                _homepage.selectPassenger(2, 1);
                _homepage.clickOnDatePicker();
                _homepage.selectDeparturedate();
                _homepage.selectReturnDate();
                _homepage.SearchFlights();
                _utitlity.WaitTillPageLoads();
                _searchresultspage.selectMorningFlight();
                _searchresultspage.sortByDeparture();
                test.Log(Status.Pass, "Test Pass");
            }catch (Exception ex)
            {
                test.Log(Status.Fail, "Test Fail");
                throw;
            }
            

        }
    }
}
