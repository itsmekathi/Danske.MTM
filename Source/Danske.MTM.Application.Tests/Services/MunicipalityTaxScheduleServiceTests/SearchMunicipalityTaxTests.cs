using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services;
using Danske.MTM.Application.Tests.Utility;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Tests.Services
{
    /// <summary>
    /// We are mocking the repository methods, the data does not matter in this case as we are verifying
    /// the interaction between service and repo.
    /// </summary>
    /// 
    public class SearchMunicipalityTaxTests
    {
        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                    MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => _.Id == 1)
                );
            });
            _repoMock.Setup(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
           {
               return await Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => _.Id == 2)
               );
           });
            _repoMock.Setup(_ => _.GetByMonth(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                    MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => _.Id == 3)
                );
            });
            _repoMock.Setup(_ => _.GetByYear(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                    MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => _.Id == 4)
                );
            });
            _service = new MunicipalityTaxScheduleService(_repoMock.Object);
        }


        [Test]
        public async Task SearchMunicipalityTax_WhenValidRequestAndDataExistsAsDayTax_ShouldReturnTheSame()
        {
            var result = await _service.SearchMunicipalityTax("Municipality-1", new DateTime(2016, 01, 01));

            Assert.IsNotNull(result);
            _repoMock.Verify(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }
        [Test]
        public async Task SearchMunicipalityTax_WhenValidRequestAndDataExistsAsWeekTax_ShouldReturnTheSame()
        {
            _repoMock.Setup(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            var result = await _service.SearchMunicipalityTax("Municipality-1", new DateTime(2016, 01, 01));

            Assert.IsNotNull(result);
            _repoMock.Verify(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByMonth(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Test]
        public async Task SearchMunicipalityTax_WhenValidRequestAndDataExistsAsMonthTax_ShouldReturnTheSame()
        {
            _repoMock.Setup(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });
            _repoMock.Setup(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            var result = await _service.SearchMunicipalityTax("Municipality-1", new DateTime(2016, 01, 01));

            Assert.IsNotNull(result);
            _repoMock.Verify(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByMonth(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByYear(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

        }

        [Test]
        public async Task SearchMunicipalityTax_WhenValidRequestAndDataExistsAsYearTax_ShouldReturnTheSame()
        {
            _repoMock.Setup(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });
            _repoMock.Setup(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            _repoMock.Setup(_ => _.GetByMonth(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            var result = await _service.SearchMunicipalityTax("Municipality-1", new DateTime(2016, 01, 01));

            Assert.IsNotNull(result);
            _repoMock.Verify(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public async Task SearchMunicipalityTax_WhenValidRequestAndDataDoesNotExists_ShouldReturnNull()
        {
            _repoMock.Setup(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });
            _repoMock.Setup(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            _repoMock.Setup(_ => _.GetByMonth(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() =>
            {
                return Task.Factory.StartNew(() =>
                   MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });
            _repoMock.Setup(_ => _.GetByYear(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() =>
                    MunicipalityTaxSchedulesMockData.Get().FirstOrDefault(_ => 1 == 2)
                );
            });

            var result = await _service.SearchMunicipalityTax("Municipality-1", new DateTime(2016, 01, 01));

            Assert.IsNull(result);
            _repoMock.Verify(_ => _.GetByDay(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _repoMock.Verify(_ => _.GetByWeek(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
