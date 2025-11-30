namespace AgriConnectMarket.Application.DTOs.QueryDtos
{
    public record ProductBatchQuery
    (
        string? searchTerm,
        Guid? categoryId,
        bool? isDesc,
        int? pageNumber,
        int? pageSize
    );
}
