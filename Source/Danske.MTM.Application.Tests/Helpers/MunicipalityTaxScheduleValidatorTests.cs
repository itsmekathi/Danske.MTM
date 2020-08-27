using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Helpers;
using Danske.MTM.Common.Enums;
using NUnit.Framework;
using System;

namespace Danske.MTM.Application.Tests
{
    public class MunicipalityTaxScheduleValidatorTests
    {
        private MunicipalityTaxScheduleDto municipalityTaxScheduleDto;

        [SetUp]
        public void SetUp()
        {
            municipalityTaxScheduleDto =  new MunicipalityTaxScheduleDto()
            {
                MunicipalityName = "M1",
                FromDate = DateTime.Now,
                Todate = DateTime.Now.AddDays(28),
                TaxAmount = 0.2M,
                TaxSheduleTypeId = (int)eTaxScheduleTypes.Monthly
            };
        }

        [Test]
        public void IsValid_WhenInputIsValid_ShouldReturnTrue()
        {
            
            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_WhenTaxTypeIsDailyAndToDateIsSet_ShouldReturnFalse()
        {
            municipalityTaxScheduleDto.TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily;
            municipalityTaxScheduleDto.Todate = DateTime.Now.AddDays(1);
            
            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_WhenTaxTypeIsMonthlyAndToDateIsNotSet_ShouldReturnFalse()
        {
            municipalityTaxScheduleDto.TaxSheduleTypeId = (int)eTaxScheduleTypes.Monthly;
            municipalityTaxScheduleDto.Todate = null;

            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_WhenForAnyTypeWithPositiveTaxAmount_ShouldReturnTrue()
        {
            municipalityTaxScheduleDto.TaxAmount = 0.2M;

            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_WhenForAnyTypeWithZeroTaxAmount_ShouldReturnFalse()
        {
            municipalityTaxScheduleDto.TaxAmount = 0.0M;

            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_WhenForAnyTypeWithNegativeTaxAmount_ShouldReturnFalse()
        {
            municipalityTaxScheduleDto.TaxAmount = -0.0M;

            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_WhenFromDateIsGreaterThanToDate_ShouldReturnFalse()
        {
            municipalityTaxScheduleDto.FromDate = DateTime.Now;
            municipalityTaxScheduleDto.Todate = DateTime.Now.AddDays(-1);
            bool result = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);

            Assert.IsFalse(result);

        }
    }
}