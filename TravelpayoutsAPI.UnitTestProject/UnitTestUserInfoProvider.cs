using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using System.Threading.Tasks;

namespace TravelpayoutsAPI.UnitTestProject
{
    [TestClass]
    public class UnitTestUserInfoProvider
    {
        private Mock<IRequestManager> _mockRequestManager;
        private IUserInfoProvider _userInfoProvider;

        [TestInitialize]
        public void Init()
        {
            _mockRequestManager = new Mock<IRequestManager>();
            _userInfoProvider = new UserInfoProvider(_mockRequestManager.Object);
        }

        [TestMethod]
        public async Task Get_UserInfo_Success()
        {
            string ip = "40.0.0.1";
            string iata = "MOW";
            string callbackName = "useriata";
            string json = String.Concat(callbackName, "({'iata':'", iata, "','name':'Москва'})");

            _mockRequestManager
                .Setup(r => r.Get(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(json);

            var userInfo = await _userInfoProvider.GetUserLocationAsync(ip);

            Assert.IsNotNull(userInfo, "UserInfo is null.");
            Assert.AreEqual(userInfo.Code, iata, "IATA code is not equal.");
        }
    }
}
