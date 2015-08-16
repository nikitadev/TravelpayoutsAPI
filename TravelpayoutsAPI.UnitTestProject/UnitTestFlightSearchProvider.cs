using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Search;

namespace TravelpayoutsAPI.UnitTestProject
{
    [TestClass]
    public class UnitTestFlightSearchProvider
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var apiFactory = new SearchTicketApiFactory();
        }

        [TestMethod]
        public void Set_Signature_Success()
        {
            var passengers = new Passengers { Adults = 1, Children = 0, Infants = 0 };
            var segments = new List<Segment>();
            segments.Add(new Segment { Origin = "NYC", Destination = "LAX", Date = DateTime.Parse("2015-11-25") });
            segments.Add(new Segment { Origin = "LAX", Destination = "NYC", Date = DateTime.Parse("2015-12-18") });

            var postQuery = new FlightSearchQuerySettings
            {
                Host = "beta.aviasales.ru",
                Locale = "ru",
                Marker = "16886",
                UserIP = "127.0.0.1",
                TripClass = "Y",
                Passengers = passengers,
                Segments = segments
            };
            postQuery.SetSignature("321d6a221f8926b5ec41ae89a3b2ae7b");

            Assert.AreEqual(postQuery.Signature, "deb5b02159898a6ab6f120624fa2f72c");
        }
    }
}
