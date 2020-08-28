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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Danske.MTM.Application.Tests.Services.MunicipalityTaxScheduleServiceTests
{
    public class BulkAddMunicipalityTaxTests
    {
        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;
        MunicipalityTaxScheduleDto requestObject;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.BulkInsertTaxSchedule(It.IsAny<IEnumerable<MunicipalityTaxSchedules>>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().Count());
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
        public async Task BulkAddMunicipalityTax_WhenAnyOfTheEntitiesPassedIsInvalid_ShouldThrowValidationException()
        {
            requestObject = new MunicipalityTaxScheduleDto()
            {
                MunicipalityName = "Municipality-3",
                TaxAmount = 0M,
                TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                FromDate = DateTime.Now,
                Todate = DateTime.Now
            };

            Assert.ThrowsAsync<ValidationException>(async () => await _service.BulkAddMunicipalityTax(new List<MunicipalityTaxScheduleDto>(){
                requestObject
            }));
            _repoMock.Verify(_ => _.BulkInsertTaxSchedule(It.IsAny<IEnumerable<MunicipalityTaxSchedules>>()), Times.Never);
        }


        [Test]
        public async Task BulkAddMunicipalityTax_WhenPassedEntitiesAreInvalid_ShouldReturnCountOfObjectsAdded()
        {
            _repoMock.Setup(_ => _.GetById(It.IsAny<int>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().Where(_ => 1 == 2).FirstOrDefault());
            });

            var actual = await _service.BulkAddMunicipalityTax(new List<MunicipalityTaxScheduleDto>(){
                requestObject
            });

            Assert.GreaterOrEqual(actual, 0);
            _repoMock.Verify(_ => _.BulkInsertTaxSchedule(It.IsAny<IEnumerable<MunicipalityTaxSchedules>>()), Times.Once);
        }
    }
}
