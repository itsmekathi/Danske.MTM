using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services;
using Danske.MTM.Application.Tests.Utility;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
namespace Danske.MTM.Application.Tests.Services.MunicipalityTaxScheduleServiceTests
{
    public class GetMunicipalityTaxByIdTests
    {
        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.GetById(It.IsAny<int>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().First());
            });
            _service = new MunicipalityTaxScheduleService(_repoMock.Object);
        }

        [Test]
        public async Task GetMunicipalityTaxById_WhenEntryForIdPassedIsNotPresent_ShouldRetunNull()
        {
            _repoMock.Setup(_ => _.GetById(It.IsAny<int>())).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get().Where(_ => 1 == 2).FirstOrDefault());
            });

            var res = await _service.GetMunicipalityTaxById(100);

            Assert.IsNull(res);
            _repoMock.Verify(_ => _.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetMunicipalityTaxById_WhenEntryPresentForIdIsPresent_ShouldRetrunEntity()
        {
            var res = await _service.GetMunicipalityTaxById(1);

            Assert.IsNotNull(res);
            _repoMock.Verify(_ => _.GetById(It.IsAny<int>()), Times.Once);
        }

    }
}
