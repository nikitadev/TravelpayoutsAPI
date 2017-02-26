using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Search;

namespace TravelpayoutsAPI.UnitTestProject
{
    /// <summary>
    /// Summary description for UnitTestPostQuery
    /// </summary>
    [TestClass]
    public class IntegrationTestFlightSearchProvider
    {
        private const string marker = "16886"; //"77022";
        private const string token = "321d6a221f8926b5ec41ae89a3b2ae7b"; //"9698fc7c86add3ece24f30a23b567fdf";

        private readonly IRealtimeSearchProvider _flightSearchProvider;
        private readonly IUserInfoProvider _userInfoProvider;

        public IntegrationTestFlightSearchProvider()
        {
            var apiFactory = new ApiFactory();

            _userInfoProvider = apiFactory.UserInfo;
            _flightSearchProvider = apiFactory.RealtimeSearch;
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task Get_Tikets_Success()
        {
            var segments = new List<Segment>
            {
                new Segment { Origin = "NYC", Destination = "LAX", Date = DateTime.Parse("2015-11-25") },
                new Segment { Origin = "LAX", Destination = "NYC", Date = DateTime.Parse("2015-12-18") }
            };

            //var segments = new List<Segment>();
            //segments.Add(new Segment { Origin = "MMK", Destination = "BAX", Date = DateTime.Parse("2015-08-25") });
            //segments.Add(new Segment { Origin = "BAX", Destination = "MMK", Date = DateTime.Parse("2015-09-18") });

            string userIP = await _userInfoProvider.UserIP;
            var tickets = await _flightSearchProvider.GetTickets(token, marker, "Integration Test TravelpayoutsAPI .NET", 1, 0, 0, "Y", userIP, segments.ToArray());

            Assert.IsNotNull(tickets);
        }
    }
}