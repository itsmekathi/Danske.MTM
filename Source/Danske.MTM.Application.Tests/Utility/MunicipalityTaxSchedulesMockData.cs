using Danske.MTM.Common.Enums;
using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Collections.Generic;

namespace Danske.MTM.Application.Tests.Utility
{
    public static class MunicipalityTaxSchedulesMockData
    {
        public static IEnumerable<MunicipalityTaxSchedules> Get()
        {
            return new List<MunicipalityTaxSchedules>()
            {
                new MunicipalityTaxSchedules()
                {
                    Id = 1,
                    MunicipalityName = "Municipality-1",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Yearly,
                    FromDate = new DateTime(2016,01,01),
                    Todate = new DateTime(2016,12,31),
                    TaxAmount = 0.2M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 2,
                    MunicipalityName = "Municipality-1",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Monthly,
                    FromDate = new DateTime(2016,05,01),
                    Todate = new DateTime(2016,05,31),
                    TaxAmount = 0.4M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 3,
                    MunicipalityName = "Municipality-1",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Weekly,
                    FromDate = new DateTime(2016,05,01),
                    Todate = new DateTime(2016,05,06),
                    TaxAmount = 0.5M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 4,
                    MunicipalityName = "Municipality-1",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                    FromDate = new DateTime(2016,01,01),
                    Todate = null,
                    TaxAmount = 0.2M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 5,
                    MunicipalityName = "Municipality-1",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                    FromDate = new DateTime(2016,12,25),
                    Todate = null,
                    TaxAmount = 0.2M
                },

                new MunicipalityTaxSchedules()
                {
                    Id = 6,
                    MunicipalityName = "Municipality-2",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Yearly,
                    FromDate = new DateTime(2016,01,01),
                    Todate = new DateTime(2016,12,31),
                    TaxAmount = 0.2M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 7,
                    MunicipalityName = "Municipality-2",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Monthly,
                    FromDate = new DateTime(2016,05,01),
                    Todate = new DateTime(2016,05,31),
                    TaxAmount = 0.4M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 8,
                    MunicipalityName = "Municipality-2",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Weekly,
                    FromDate = new DateTime(2016,05,01),
                    Todate = new DateTime(2016,05,06),
                    TaxAmount = 0.5M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 9,
                    MunicipalityName = "Municipality-2",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                    FromDate = new DateTime(2016,01,01),
                    Todate = null,
                    TaxAmount = 0.2M
                },
                new MunicipalityTaxSchedules()
                {
                    Id = 10,
                    MunicipalityName = "Municipality-2",
                    TaxSheduleTypeId = (int)eTaxScheduleTypes.Daily,
                    FromDate = new DateTime(2016,12,25),
                    Todate = null,
                    TaxAmount = 0.2M
                }
            };
        }

    }
}
