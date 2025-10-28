namespace AgriConnectMarket.SharedKernel.Pagination
{
    public sealed class PagedRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public sealed class PagedResponse<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPage => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PagedResponse(IReadOnlyCollection<T> items, int totalItems, int currentPage, int pageSize)
        {
            this.Items = items;
            this.TotalItems = totalItems;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
        }
    }
}
