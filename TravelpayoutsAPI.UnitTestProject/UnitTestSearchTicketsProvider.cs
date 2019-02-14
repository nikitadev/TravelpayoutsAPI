using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.UnitTestProject
{
    [TestClass]
    public class UnitTestSearchTicketsProvider
    {
        private Mock<IRequestManager> _mockRequestManager;
        private ISearchTicketsProvider _searchTicketsProvider;

        [TestInitialize]
        public void Init()
        {
            _mockRequestManager = new Mock<IRequestManager>();
            _searchTicketsProvider = new SearchTicketsProvider(_mockRequestManager.Object);
        }

        [TestMethod]
        public async Task Get_SpecialOffers_Success()
        {
            string xml = @"
                 <offers>
                    <offer airline='Scandinavian Airlines' airline_code='SK' title='Из Москвы в Скандинавию от 3422 рублей! Специальное предложение от авиакомпании SAS' id='18191' href='http://www.aviasales.ru/offers/iz-moskvy-v-skandinaviu-ot-3422-rublei-spetsialnoe-predlozhenie-ot-aviakompanii-sas' sale_date_begin='1430784000' sale_date_end='1431388800' flight_date_begin='1430784000' flight_date_end='1433030400' link='http://www.flysas.com/ru/ru'>
                        <description/>
                        <conditions>
                            <ul><li>Цены на авиабилеты указаны с учетом топливных, аэропортовых и государственных сборов.</li><li>Количество авиабилетов на каждом рейсе по данному тарифу ограничено.</li></ul>
                        </conditions>
                        <route from_iata='MOW' to_iata='STO' from_name='Москва' to_name='Стокгольм' class='эконом' oneway_price='от 3442 рублей' roundtrip_price=''/>
                        <route from_iata='MOW' to_iata='CPH' from_name='Москва' to_name='Копенгаген' class='эконом' oneway_price='от 5512 рублей' roundtrip_price=''/>
                        <route from_iata='MOW' to_iata='OSL' from_name='Москва' to_name='Осло' class='эконом' oneway_price='от 7442 рублей' roundtrip_price=''/>
                    </offer>
                    <offer airline='Трансаэро' airline_code='UN' title='По России от 3796 рублей! Специальное предложение от авиакомпании Трансаэро' id='18190' href='http://www.aviasales.ru/offers/po-rossii-ot-3796-rublei-spetsialnoe-predlozhenie-ot-aviakompanii-transaero' sale_date_begin='1430697600' sale_date_end='1431302400' flight_date_begin='1430697600' flight_date_end='1431302400' link='http://transaero.ru/ru/premium-class'>
                        <description/>
                        <conditions>
                            <ul><li>Цены на авиабилеты указаны с учетом топливных, аэропортовых и государственных сборов.</li><li>Количество авиабилетов на каждом рейсе по данному тарифу ограничено.</li><li>Полные условия продажи авиабилетов на сайте авиакомпании.</li></ul>
                        </conditions>
                        <route from_iata='MOW' to_iata='RGK' from_name='Москва' to_name='Горно-Алтайск' class='эконом' oneway_price='от 3796 рублей' roundtrip_price=''/>
                        <route from_iata='MOW' to_iata='RGK' from_name='Москва' to_name='Горно-Алтайск' class='эконом' oneway_price='' roundtrip_price='от 7536 рублей'/>
                    </offer>
                </offers>";

            _mockRequestManager
                .Setup(r => r.Get(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(xml);

            var offers = await _searchTicketsProvider.GetSpecialOffers(It.IsAny<string>());

            Assert.IsNotNull(offers, "List of planes is null.");
        }
    }
}
