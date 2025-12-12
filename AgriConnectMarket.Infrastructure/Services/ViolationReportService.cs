using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ViolationReportService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<AddViolationReportResponseDto>> CreateAsync(AddViolationReportRequestDto dto, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(dto.farmId);

            if (farm is null)
            {
                return Result<AddViolationReportResponseDto>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            if (_currentUserService.UserId is null)
                return Result<AddViolationReportResponseDto>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
                return Result<AddViolationReportResponseDto>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);

            var entity = ViolationReport.Create(dto.farmId, profile.Id, dto.violationType, dto.content, dto.evidenceUrl);

            await _uow.ViolationReportRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            var response = new AddViolationReportResponseDto(profile.Fullname, entity.ReportContent, entity.EvidenceUrl ?? "", entity.CreatedAt);

            return Result<AddViolationReportResponseDto>.Success(response);
        }

        public async Task<Result<IEnumerable<AddViolationReportResponseDto>>> GetAllReportsAsync(CancellationToken ct = default)
        {
            var entities = await _uow.ViolationReportRepository.ListAllAsync(ct);

            if (!entities.Any())
            {
                return Result<IEnumerable<AddViolationReportResponseDto>>.Success([]);
            }

            var response = entities.Select(e => new AddViolationReportResponseDto(e.Customer.Fullname, e.ReportContent, e.EvidenceUrl ?? "", e.CreatedAt));

            return Result<IEnumerable<AddViolationReportResponseDto>>.Success(response);
        }
    }
}
