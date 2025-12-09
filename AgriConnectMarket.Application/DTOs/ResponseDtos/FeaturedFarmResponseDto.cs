using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record class FeaturedFarmResponseDto(Guid FarmId, string FarmName, string FarmDesc, string BannerUrl, string Area, string Phone, bool IsMall);
}
