using Danske.MTM.Application.Dtos;
using Danske.MTM.Application.Helpers;
using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services.Interfaces;
using Danske.MTM.Common.Exceptions;
using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Services
{
    public class MunicipalityTaxScheduleService : IMunicipalityTaxScheduleService
    {
        private IMunicipalityTaxScheduleRepository _municipalityTaxScheduleRepository { get; }
        public MunicipalityTaxScheduleService(IMunicipalityTaxScheduleRepository municipalityTaxScheduleRepository)
        {
            _municipalityTaxScheduleRepository = municipalityTaxScheduleRepository;
        }
        public async Task<MunicipalityTaxScheduleDto> SearchMunicipalityTax(string municipalityName, DateTime date)
        {
            MunicipalityTaxSchedules taxSchedule = await _municipalityTaxScheduleRepository.GetByDay(municipalityName, date);
            if (taxSchedule == null)
            {
                taxSchedule = await _municipalityTaxScheduleRepository.GetByWeek(municipalityName, date);
            }
            if (taxSchedule == null)
            {
                taxSchedule = await _municipalityTaxScheduleRepository.GetByMonth(municipalityName, date);
            }
            if (taxSchedule == null)
            {
                taxSchedule = await _municipalityTaxScheduleRepository.GetByYear(municipalityName, date);
            }
            if (taxSchedule == null) return null;

            return MunicipalityTaxScheduleMapper.ToDto(taxSchedule);
        }

        public async Task<MunicipalityTaxScheduleDto> AddNewMunicipalityTax(MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            bool isValid = MunicipalityTaxScheduleValidator.IsValid(municipalityTaxScheduleDto);
            if (!isValid) throw new ValidationException();
            MunicipalityTaxSchedules taxSchedule = MunicipalityTaxScheduleMapper.ToEntity(municipalityTaxScheduleDto);
            await _municipalityTaxScheduleRepository.AddTaxSchedule(taxSchedule);
            return MunicipalityTaxScheduleMapper.ToDto(taxSchedule);
        }

        public async Task<MunicipalityTaxScheduleDto> UpdateExistingMunicipalityTax(int id, MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            MunicipalityTaxSchedules taxSchedule = await _municipalityTaxScheduleRepository.GetById(id);
            if (taxSchedule == null) throw new NotFoundException("MunicipalityTaxSchedules", id);
            taxSchedule.ModifiedOn = DateTime.Now;
            taxSchedule.MunicipalityName = municipalityTaxScheduleDto.MunicipalityName;
            taxSchedule.FromDate = municipalityTaxScheduleDto.FromDate;
            taxSchedule.Todate = municipalityTaxScheduleDto.Todate;
            taxSchedule.TaxAmount = municipalityTaxScheduleDto.TaxAmount;
            await _municipalityTaxScheduleRepository.UpdateTaxSchedule(taxSchedule);
            return MunicipalityTaxScheduleMapper.ToDto(taxSchedule);
        }

        public async Task<IEnumerable<MunicipalityTaxScheduleDto>> GetAllMunicipalityTax()
        {
            return (await _municipalityTaxScheduleRepository.GetAll()).Select(_ => MunicipalityTaxScheduleMapper.ToDto(_));
        }
    }
}
