using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Danske.MTM.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TaxesController : BaseApiController
    {

        private IMunicipalityTaxScheduleService _municipalityTaxScheduleService;
        public TaxesController(IMunicipalityTaxScheduleService municipalityTaxScheduleService)
        {
            _municipalityTaxScheduleService = municipalityTaxScheduleService;
        }
        [HttpGet]
        public async Task<IEnumerable<MunicipalityTaxScheduleDto>> GetAll()
        {
            return await _municipalityTaxScheduleService.GetAllMunicipalityTax();

        }
        [HttpGet]
        public async Task<IEnumerable<MunicipalityTaxScheduleDto>> GetById([FromQuery][Required]int taxId)
        {
            return await _municipalityTaxScheduleService.GetAllMunicipalityTax();

        }

        [HttpGet]
        public async Task<MunicipalityTaxScheduleDto> Search([FromQuery][Required][MinLength(3)][MaxLength(200)]string municipalityName, [FromQuery][Required]DateTime date)
        {
            return await _municipalityTaxScheduleService.SearchMunicipalityTax(municipalityName, date);

        }

        [HttpPost]
        public async Task<IActionResult> AddTax([FromBody] MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var tax = await _municipalityTaxScheduleService.AddNewMunicipalityTax(municipalityTaxScheduleDto);
            return Ok(tax);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTax([FromQuery]int taxId, [FromBody] MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var tax = await _municipalityTaxScheduleService.UpdateExistingMunicipalityTax(taxId, municipalityTaxScheduleDto);
            return Ok(tax);
        }

        [HttpPost]
        public async Task<int> BulkUploadTaxes()
        {
            // To be added 
            return await Task.Factory.StartNew(() => 0);
        }
    }
}
