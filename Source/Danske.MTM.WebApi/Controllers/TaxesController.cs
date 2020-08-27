using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
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
        [Route("{id:int}")]
        public async Task<MunicipalityTaxScheduleDto> GetById([Required]int id)
        {
            return await _municipalityTaxScheduleService.GetMunicipalityTaxById(id);
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
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTax([Required]int id, [FromBody] MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var tax = await _municipalityTaxScheduleService.UpdateExistingMunicipalityTax(id, municipalityTaxScheduleDto);
            return Ok(tax);
        }

        [HttpGet]
        public async Task<MunicipalityTaxScheduleDto> Search([FromQuery][Required][MinLength(3)][MaxLength(200)]string municipalityName, [FromQuery][Required]DateTime date)
        {
            return await _municipalityTaxScheduleService.SearchMunicipalityTax(municipalityName, date);
        }

        [HttpPost]
        public async Task<IActionResult> BulkUploadTaxes()
        {
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var jsonFile = files[0];
                if(Path.GetExtension(jsonFile.FileName).ToLower() != ".json")
                {
                    return BadRequest("Invalid File Format");
                }
                try
                {
                    string fileContents;
                    using (var stream = jsonFile.OpenReadStream())
                    using (var reader = new StreamReader(stream))
                    {
                        fileContents = await reader.ReadToEndAsync();
                    }
                    var entities = JsonConvert.DeserializeObject<IEnumerable<MunicipalityTaxScheduleDto>>(fileContents);
                    int count = await _municipalityTaxScheduleService.BulkAddMunicipalityTax(entities);
                    return Ok(count);
                }
                catch(Exception ex)
                {
                    var sqlException = ex.InnerException
                                       as SqlException;

                    if (sqlException.Number == 2601 || sqlException.Number == 2627)
                    {
                        return BadRequest("Cannot insert duplicate values.");
                    }
                    else
                    {
                        return BadRequest("Error while saving data.");
                    }
                }
            }
            else
                return BadRequest();
        }
    }
}
