//using AgriConnectMarket.Application.Interfaces;
//using AgriConnectMarket.Domain.Entities;
//using AgriConnectMarket.SharedKernel.Constants;
//using AgriConnectMarket.SharedKernel.Result;

//namespace AgriConnectMarket.Infrastructure.Services
//{
//    public class PreOrderService(IUnitOfWork _uow)
//    {
//        public async Task<Result<PreOrder>> CreatePreOrderAsync(Guid customerId, Guid farmId, Guid productId, decimal quantity, string note, CancellationToken ct = default)
//        {
//            // Verify farm and product
//            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);
//            if (farm is null) return Result<PreOrder>.Fail(MessageConstant.FARM_NOT_FOUND);

//            var product = await _uow.ProductRepository.GetByIdAsync(productId, ct);
//            if (product is null) return Result<PreOrder>.Fail(MessageConstant.PRODUCT_NOT_FOUND);

//            var preOrder = PreOrder.Create(customerId, farmId, productId, quantity, note);

//            await _uow.PreOrderRepository.AddAsync(preOrder, ct);
//            await _uow.SaveChangesAsync(ct);

//            return Result<PreOrder>.Success(preOrder);
//        }

//        public async Task<Result<IEnumerable<PreOrder>>> GetCustomerPreOrdersAsync(Guid customerId, CancellationToken ct = default)
//        {
//            var allPreOrders = await _uow.PreOrderRepository.ListAllAsync(ct);
//            var customerPreOrders = allPreOrders.Where(p => p.CustomerId == customerId);
//            return Result<IEnumerable<PreOrder>>.Success(customerPreOrders);
//        }

//        public async Task<Result<IEnumerable<PreOrder>>> GetFarmerPreOrdersAsync(Guid farmId, CancellationToken ct = default)
//        {
//            var allPreOrders = await _uow.PreOrderRepository.ListAllAsync(ct);
//            var farmPreOrders = allPreOrders.Where(p => p.FarmId == farmId);
//            return Result<IEnumerable<PreOrder>>.Success(farmPreOrders);
//        }

//        public async Task<Result<PreOrder>> UpdatePreOrderDateAsync(Guid preOrderId, DateTime releaseDate, CancellationToken ct = default)
//        {
//            var preOrder = await _uow.PreOrderRepository.GetByIdAsync(preOrderId, ct);
//            if (preOrder is null) return Result<PreOrder>.Fail("PreOrder not found");

//            preOrder.SetExpectedReleaseDate(releaseDate);
//            await _uow.PreOrderRepository.UpdateAsync(preOrder, ct);
//            await _uow.SaveChangesAsync(ct);

//            return Result<PreOrder>.Success(preOrder);
//        }

//        public async Task<Result<IEnumerable<Product>>> GetFarmProductSuggestionsAsync(Guid farmId, CancellationToken ct = default)
//        {
//            var batches = await _uow.ProductBatchRepository.GetByFarmAsync(farmId, true, ct);
//            var productIds = batches.Select(b => b.Season?.ProductId).Where(id => id.HasValue).Select(id => id.Value).Distinct();

//            var products = new List<Product>();
//            foreach (var pid in productIds)
//            {
//                var p = await _uow.ProductRepository.GetByIdAsync(pid, ct);
//                if (p != null) products.Add(p);
//            }

//            return Result<IEnumerable<Product>>.Success(products);
//        }
//    }
//}
