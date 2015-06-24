using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.UnitTestProject
{
    [TestClass]
    public class UnitTestDataInfoProvider
    {
		private Mock<IRequestManager> _mockRequestManager;
        private IDataInfoProvider _dataInfoProvider;

        [TestInitialize]
        public void Init()
        { 
            _mockRequestManager = new Mock<IRequestManager>();
            _dataInfoProvider = new DataInfoProvider(_mockRequestManager.Object);
        }

        private bool IsJsonValidate(string json)
        {
            JToken jtoken = null;

            try
            {
                jtoken = JContainer.Parse(json);
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return jtoken != null;
        }

        [TestMethod]
        public async Task Get_Planes_Success()
        {
            string json = @"[{
                    'code':'100',
                    'name':'Fokker 100'
                },
                {
                    'code':'141',
                    'name':'British Aerospace BAe 146-100'
                },
                {
                    'code':'142',
                    'name':'British Aerospace BAe 146-200'
                },
                {
                    'code':'143',
                    'name':'British Aerospace BAe 146-300'
                }]";

            Assert.IsTrue(IsJsonValidate(json));

            _mockRequestManager
                .Setup(r => r.Get(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(json);

            var planes = await _dataInfoProvider.GetAsync<Plane>(DataNames.Planes);

            Assert.IsNotNull(planes, "List of planes is null.");

			var listPlanes = JsonConvert.DeserializeObject<List<Plane>>(json);
			
			Assert.AreEqual(listPlanes.Count, planes.ToList().Count, "Counts are not equals.");
        }

        [TestMethod]
        public async Task Get_Cities_Success()
        {
            string json = @"[{
                            'code':'SCE',
                            'name':'State College',
                            'coordinates':{
                                'lon':-77.84823,
                                'lat':40.85372
                            },
                            'time_zone':'America/New_York',
                            'name_translations':{
                                'de':'State College',
                                'en':'State College',
                                'zh-CN':'大学城',
                                'tr':'State College',
                                'ru':'Стейт Колледж',
                                'it':'State College',
                                'es':'State College',
                                'fr':'State College',
                                'th':'สเตทคอลเลจ'
                            },
                            'country_code':'US'
                            },
                            {
                            'code':'AAB',
                            'name':'Arrabury',
                            'coordinates':{
                                'lon':141.04167,
                                'lat':-26.7
                            },
                            'time_zone':'Australia/Brisbane',
                            'name_translations':{
                                'de':'Arrabury',
                                'en':'Arrabury',
                                'zh-CN':'阿拉伯里',
                                'ru':'Аррабури'
                            },
                            'country_code':'AU'
                        }]";

            Assert.IsTrue(IsJsonValidate(json));

            _mockRequestManager
                .Setup(r => r.Get(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(json);

            var cities = await _dataInfoProvider.GetAsync<City>(DataNames.Cities);

            Assert.IsNotNull(cities, "List of planes is null.");

            var listCities = JsonConvert.DeserializeObject<List<City>>(json);

            Assert.AreEqual(listCities.Count, cities.ToList().Count, "Counts are not equals.");
        }
    }
}
