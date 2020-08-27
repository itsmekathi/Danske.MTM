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
    public class AddNewMunicipalityTaxTests
    {

        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;
        MunicipalityTaxScheduleDto requestObject;
        


        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.AddTaxSchedule(It.IsAny<MunicipalityTaxSchedules>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().First());
            });
            requestObject  = new MunicipalityTaxScheduleDto()
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
        public async Task AddNewMunicipalityTax_WhenValidDataIsPassed_ShouldReturnCreatedEntity()
        {
            
            var actual = await _service.AddNewMunicipalityTax(requestObject);
            Assert.IsNotNull(actual);

            _repoMock.Verify(_ => _.AddTaxSchedule(It.IsAny<MunicipalityTaxSchedules>()), Times.Once);

        }

        [Test]
        public async Task AddNewMunicipalityTax_WhenInValidDataIsPassed_ShouldThrowException()
        {
            requestObject.Todate = DateTime.Now;

            Assert.ThrowsAsync<ValidationException>(async () => await _service.AddNewMunicipalityTax(requestObject));

            _repoMock.Verify(_ => _.AddTaxSchedule(It.IsAny<MunicipalityTaxSchedules>()), Times.Never);

            await Task.Factory.StartNew(() => 0);  // workaround to avoid vs warnings
        }

    }
}
