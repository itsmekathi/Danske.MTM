using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services;
using Danske.MTM.Application.Tests.Utility;
using Danske.MTM.Common.Enums;
using Danske.MTM.Common.Exceptions;
using Danske.MTM.Persistence.Models.MTM;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Tests.Services.MunicipalityTaxScheduleServiceTests
{
    public class UpdateExistingMunicipalityTaxTests
    {
        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;
        MunicipalityTaxScheduleDto requestObject;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.GetById(It.IsAny<int>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().First());
            });
            requestObject = new MunicipalityTaxScheduleDto()
            {
                MunicipalityName = "Municipality-3",
                TaxAmount = 10.2M,
                TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                FromDate = DateTime.Now,
                Todate = null
            };
            _service = new MunicipalityTaxScheduleService(_repoMock.Object);
        }
        [Test]
        public async Task UpdateExistingMunicipalityTax_WhenPassedObjectIsInvalid_ShouldThrowValidationException()
        {
            requestObject = new MunicipalityTaxScheduleDto()
            {
                MunicipalityName = "Municipality-3",
                TaxAmount = 0M,
                TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                FromDate = DateTime.Now,
                Todate = DateTime.Now
            };

            Assert.ThrowsAsync<ValidationException>(async () => await _service.UpdateExistingMunicipalityTax(1, requestObject));

            _repoMock.Verify(_ => _.GetById(It.IsAny<int>()), Times.Never);
            _repoMock.Verify(_ => _.UpdateTaxSchedule(It.IsAny<MunicipalityTaxSchedules>()), Times.Never);

            await Task.Factory.StartNew(() => 0);  // workaround to avoid vs warnings
        }


        [Test]
        public async Task UpdateExistingMunicipalityTax_WhenEntryNotFoundForPassedId_ShouldThrowValidationException()
        {
            _repoMock.Setup(_ => _.GetById(It.IsAny<int>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().Where(_ => 1 == 2).FirstOrDefault());
            });

            Assert.ThrowsAsync<NotFoundException>(async () => await _service.UpdateExistingMunicipalityTax(1, requestObject));

            _repoMock.Verify(_ => _.GetById(It.IsAny<int>()), Times.Once);
            _repoMock.Verify(_ => _.UpdateTaxSchedule(It.IsAny<MunicipalityTaxSchedules>()), Times.Never);
            await Task.Factory.StartNew(() => 0);  // workaround to avoid vs warnings
        }

        [Test]
        public async Task UpdateExistingMunicipalityTax_WhenPassedIdAndEntityIsValid_ShouldReturnUpdatedEntity()
        {
            var res = await _service.UpdateExistingMunicipalityTax(1, requestObject);

            Assert.IsNotNull(res);
            _repoMock.Verify(_ => _.GetById(It.IsAny<int>()), Times.Once);
            _repoMock.Verify(_ => _.UpdateTaxSchedule(It.IsAny<MunicipalityTaxSchedules>()), Times.Once);
        }
    }
}
