using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services;
using Danske.MTM.Application.Tests.Utility;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
namespace Danske.MTM.Application.Tests.Services.MunicipalityTaxScheduleServiceTests
{
    public class GetAllMunicipalityTaxTests
    {
        private MunicipalityTaxScheduleService _service;
        private Mock<IMunicipalityTaxScheduleRepository> _repoMock;
        MunicipalityTaxScheduleDto requestObject;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IMunicipalityTaxScheduleRepository>();
            _repoMock.Setup(_ => _.GetAll()).Returns(async () =>
            {
                return await Task.Factory.StartNew(() => MunicipalityTaxSchedulesMockData.Get());
            });
            _service = new MunicipalityTaxScheduleService(_repoMock.Object);
        }

        [Test]
        public async Task GetAllMunicipalityTax_WhenCalled_InvokesRepository()
        {
            var res = await _service.GetAllMunicipalityTax();

            _repoMock.Verify(_ => _.GetAll(), Times.Once);

        }
    }
}
