using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class CertificateService(IUnitOfWork _uow)
    {
        public async Task<Result<string>> UploadCertificate(UploadCertificateDto dto, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(dto.FarmId, ct);

            if (farm is null)
            {
                return Result<string>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.CertificateUrl = dto.CertificateUrl;

            await _uow.FarmRepository.UpdateAsync(farm, ct);
            await _uow.SaveChangesAsync();

            return Result<string>.Success(farm.CertificateUrl);
        }

        public async Task<Result<string>> UpdateCertificate(Guid farmId, string newUrl, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<string>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.CertificateUrl = newUrl;

            await _uow.FarmRepository.UpdateAsync(farm, ct);
            await _uow.SaveChangesAsync();

            return Result<string>.Success(farm.CertificateUrl);
        }

        public async Task<Result<Guid>> DeleteCertificate(Guid farmId, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<Guid>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.CertificateUrl = null;

            await _uow.FarmRepository.UpdateAsync(farm, ct);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(farmId);
        }
    }
}
