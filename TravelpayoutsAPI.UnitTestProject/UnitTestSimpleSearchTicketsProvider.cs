using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelpayoutsAPI.Library.Models;
using Newtonsoft.Json.Linq;

namespace TravelpayoutsAPI.UnitTestProject
{
    /// <summary>
    /// Summary description for UnitTestSimpleSearchTicketsProvider
    /// </summary>
    [TestClass]
    public class UnitTestSimpleSearchTicketsProvider
    {
        private Mock<IRequestManager> _mockRequestManager;
        private ISimpleSearchTicketsProvider _simpleSearchTicketsProvider;

        [TestInitialize]
        public void Init()
        {
            _mockRequestManager = new Mock<IRequestManager>();
            _simpleSearchTicketsProvider = new SimpleSearchTicketsProvider(_mockRequestManager.Object);
        }

        [TestMethod]
        public async Task Get_Cheapest_Tickets_Success()
        {
            string json = @"{
                                'success': true, 
                                'data': {'HKT':{'0':{'price':41360,'airline':'SU','flight_number':274,'departure_at':'2015-06-27T22:05:00Z','return_at':'2015-06-29T10:45:00Z','expires_at':'2015-06-25T06:12:49Z'},'1':{'price':31056,'airline':'EY','flight_number':68,'departure_at':'2015-06-25T12:55:00Z','return_at':'2015-06-30T20:30:00Z','expires_at':'2015-06-25T12:01:50Z'},'2':{'price':28389,'airline':'EY','flight_number':68,'departure_at':'2015-06-25T12:55:00Z','return_at':'2015-06-30T10:20:00Z','expires_at':'2015-06-25T12:01:50Z'},'3':{'price':74984,'airline':'SU','flight_number':3122,'departure_at':'2015-06-25T13:30:00Z','return_at':'2015-06-30T21:10:00Z','expires_at':'2015-06-24T21:44:06Z'}}}
                            }";

            _mockRequestManager
                .Setup(r => r.GetJToken(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(JContainer.Parse(json));

            var tickets = await _simpleSearchTicketsProvider.GetCheapestTickets(It.IsAny<string>(), It.IsAny<string>());
            Assert.IsNotNull(tickets, "List of tickets is null.");

            Assert.IsFalse(tickets.ToList().Count == 0, "Count is not 0.");
        }
    }
}
