using Application.ExchangeRates.Commands.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using WalletsAPI.Controllers;

namespace WalletsAPITests
{
    public class ExchangeRatesControllerTests
    {
        private AutoMocker _mocker;
        private ExchangeRatesController _sut;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
        }

        [Test]
        public void GetAllExchangeRates_Returns_ListOfRates()
        {
            //
            // ARRANGE
            //

            var expectedExchangeRates = new List<ExchangeRateModel>
            {
                new ExchangeRateModel
                {
                    Id = 1,
                    Currency = "USD",
                    Code = "USD123",
                    Mid = 1.0m,
                    TableName = "Table1",
                    TableNo = "TableNo1",
                    EffectiveDate = "2023-11-08",
                    CreatedDate = "2023-11-08T10:30:00"
                },
                new ExchangeRateModel
                {
                    Id = 2,
                    Currency = "EUR",
                    Code = "EUR123",
                    Mid = 0.85m,
                    TableName = "Table2",
                    TableNo = "TableNo2",
                    EffectiveDate = "2023-11-08",
                    CreatedDate = "2023-11-08T10:45:00"
                }
            };

            _mocker.GetMock<IGetExchangeRatesQuery>()
                .Setup(q => q.Execute())
                .Returns(expectedExchangeRates);

            _sut = _mocker.CreateInstance<ExchangeRatesController>();

            //
            // ACT
            //

            var result = _sut.GetAllExchangeRates();

            //
            // ASSERT
            //

            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<List<ExchangeRateModel>>());
            var actualExchangeRates = (List<ExchangeRateModel>)okResult.Value;

            Assert.That(actualExchangeRates.Count, Is.EqualTo(2));

            for (int i = 0; i < expectedExchangeRates.Count; i++)
            {
                var expectedRate = expectedExchangeRates[i];
                var actualRate = actualExchangeRates.ElementAt(i);

                Assert.That(actualRate.Id, Is.EqualTo(expectedRate.Id));
                Assert.That(actualRate.Currency, Is.EqualTo(expectedRate.Currency));
                Assert.That(actualRate.Code, Is.EqualTo(expectedRate.Code));
                Assert.That(actualRate.Mid, Is.EqualTo(expectedRate.Mid));
                Assert.That(actualRate.TableName, Is.EqualTo(expectedRate.TableName));
                Assert.That(actualRate.TableNo, Is.EqualTo(expectedRate.TableNo));
                Assert.That(actualRate.EffectiveDate, Is.EqualTo(expectedRate.EffectiveDate));
                Assert.That(actualRate.CreatedDate, Is.EqualTo(expectedRate.CreatedDate));
            }
        }

        [Test]
        public void GetAllExchangeRates_Returns_EmptyListOfRates()
        {
            //
            // ARRANGE
            //

            _mocker.GetMock<IGetExchangeRatesQuery>()
                .Setup(q => q.Execute())
                .Returns(new List<ExchangeRateModel> { });

            _sut = _mocker.CreateInstance<ExchangeRatesController>();

            //
            // ACT
            //

            var result = _sut.GetAllExchangeRates();

            //
            // ASSERT
            //

            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<List<ExchangeRateModel>>());
            var actualExchangeRates = (List<ExchangeRateModel>)okResult.Value;

            Assert.That(actualExchangeRates.Count, Is.EqualTo(0));
        }
    }
}
